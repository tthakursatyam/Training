using ClaimService.API.Data;
using ClaimService.API.DTOs;
using ClaimService.API.Helpers;
using ClaimService.API.Mongo;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Net.Http.Json;

namespace ClaimService.API.Services
{
    public class ClaimServices
    {
        private readonly ClaimDbContext _context;
        private readonly IMongoCollection<ClaimDocument> _mongo;
        private readonly IMongoCollection<QueryDocument> _queryMongo;
        private readonly HttpClient _httpClient;

        public ClaimServices(ClaimDbContext context, IMongoDatabase database, HttpClient httpClient)
        {
            _context = context;
            _mongo = database.GetCollection<ClaimService.API.Mongo.ClaimDocument>("ClaimDocuments");
            _queryMongo = database.GetCollection<ClaimService.API.Mongo.QueryDocument>("QueryDocuments");
            _httpClient = httpClient;
        }

        // ─── CLAIMS ──────────────────────────────────────────────────────────────

        public async Task<int> CreateClaim(CreateClaimRequest dto, HttpContext context)
        {
            var userId = JwtHelper.GetUserId(context);

            var claim = new ClaimService.API.Entities.Claim
            {
                UserId = userId,
                PolicyId = dto.PolicyId,
                Description = dto.Description,
                Status = "Pending"
            };

            await _context.Claims.AddAsync(claim);
            await _context.SaveChangesAsync();

            // Auto-assign to a free adjuster immediately
            await AssignToFreeAdjuster(claim.Id);

            return claim.Id;
        }

        public async Task UploadDocument(int claimId, IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var doc = new ClaimService.API.Mongo.ClaimDocument
            {
                ClaimId = claimId,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Content = ms.ToArray()
            };

            await _mongo.InsertOneAsync(doc);
        }

        /// <summary>
        /// Finds the first free Claim Adjuster (one with no "Assigned" claim)
        /// and assigns the given claim to them.
        /// </summary>
        public async Task AssignToFreeAdjuster(int claimId)
        {
            try
            {
                var adjusters = await _httpClient.GetFromJsonAsync<List<Guid>>("http://localhost:5257/api/internal/claim-adjusters");
                if (adjusters == null || !adjusters.Any()) return;

                var busyAdjusterIds = await _context.Claims
                    .Where(c => c.Status == "Assigned" && c.AssignedAdjusterId != null)
                    .Select(c => c.AssignedAdjusterId!.Value)
                    .ToListAsync();

                var freeAdjuster = adjusters.FirstOrDefault(a => !busyAdjusterIds.Contains(a));
                if (freeAdjuster == Guid.Empty) return;

                var claim = await _context.Claims.FindAsync(claimId);
                if (claim != null && claim.Status == "Pending")
                {
                    claim.Status = "Assigned";
                    claim.AssignedAdjusterId = freeAdjuster;
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                // If AuthService is unreachable, claim stays Pending
            }
        }

        /// <summary>
        /// When an adjuster finishes a claim, pull the oldest Pending claim and assign it to them.
        /// </summary>
        public async Task AssignNextClaim(Guid adjusterId)
        {
            var claim = await _context.Claims
                .Where(x => x.Status == "Pending")
                .OrderBy(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            if (claim == null) return;

            claim.Status = "Assigned";
            claim.AssignedAdjusterId = adjusterId;

            await _context.SaveChangesAsync();
        }

        public async Task<List<ClaimService.API.Entities.Claim>> GetAssignedClaims(HttpContext context)
        {
            var adjusterId = JwtHelper.GetUserId(context);

            return await _context.Claims
                .Where(x => x.AssignedAdjusterId == adjusterId && x.Status == "Assigned")
                .ToListAsync();
        }

        public async Task<List<ClaimService.API.Entities.Claim>> GetCustomerClaims(HttpContext context)
        {
            var customerId = JwtHelper.GetUserId(context);

            return await _context.Claims
                .Where(x => x.UserId == customerId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task ApproveReject(ApproveRejectDto dto, HttpContext context)
        {
            var adjusterId = JwtHelper.GetUserId(context);

            var claim = await _context.Claims.FindAsync(dto.ClaimId);

            if (claim == null) throw new Exception("Claim not found");
            if (claim.AssignedAdjusterId != adjusterId)
                throw new Exception("Unauthorized");

            claim.Status = dto.Status;

            await _context.SaveChangesAsync();

            // Auto-assign the next pending claim to this now-free adjuster
            await AssignNextClaim(adjusterId);
        }

        public async Task<List<ClaimService.API.Entities.Claim>> GetAllClaims()
        {
            return await _context.Claims
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<AdjusterStatsDto> GetAdjusterStats(HttpContext context)
        {
            var adjusterId = JwtHelper.GetUserId(context);
            var myClaims = await _context.Claims
                .Where(c => c.AssignedAdjusterId == adjusterId)
                .ToListAsync();

            var pendingInQueue = await _context.Claims
                .CountAsync(c => c.Status == "Pending");

            return new AdjusterStatsDto
            {
                AssignedClaims  = myClaims.Count(c => c.Status == "Assigned"),
                ApprovedClaims  = myClaims.Count(c => c.Status == "Approved"),
                RejectedClaims  = myClaims.Count(c => c.Status == "Rejected"),
                PendingInQueue  = pendingInQueue
            };
        }

        // ─── QUERIES ─────────────────────────────────────────────────────────────

        public async Task CreateQuery(CreateQueryRequest dto, HttpContext context)
        {
            var customerId = JwtHelper.GetUserId(context);
            var query = new ClaimService.API.Entities.Query
            {
                CustomerId = customerId,
                Title = dto.Title,
                Description = dto.Description,
                Status = "Pending"
            };
            await _context.Queries.AddAsync(query);
            await _context.SaveChangesAsync();

            // Auto-assign to a free agent immediately
            await AssignToFreeAgent(query.Id);
        }

        /// <summary>
        /// Finds the first free Agent (one with no "Assigned" query) and assigns this query.
        /// </summary>
        public async Task AssignToFreeAgent(int queryId)
        {
            try
            {
                var agents = await _httpClient.GetFromJsonAsync<List<Guid>>("http://localhost:5257/api/internal/agents");
                if (agents == null || !agents.Any()) return;

                var busyAgentIds = await _context.Queries
                    .Where(q => q.Status == "Assigned" && q.AgentId != null)
                    .Select(q => q.AgentId!.Value)
                    .ToListAsync();

                var freeAgent = agents.FirstOrDefault(a => !busyAgentIds.Contains(a));
                if (freeAgent == Guid.Empty) return;

                var query = await _context.Queries.FindAsync(queryId);
                if (query != null && (query.Status == "Pending" || query.Status == "Reopened"))
                {
                    query.Status = "Assigned";
                    query.AgentId = freeAgent;
                    await _context.SaveChangesAsync();

                    // Notify Agent
                    await NotifyUser(freeAgent, "New Ticket Assigned", $"You have been assigned a new ticket: {query.Title}", true);
                }
            }
            catch
            {
                // If AuthService is unreachable, query stays Pending
            }
        }

        /// <summary>
        /// When an agent finishes a query, pull the oldest Pending/Reopened query and assign it.
        /// </summary>
        public async Task AssignNextQuery(Guid agentId)
        {
            var query = await _context.Queries
                .Where(x => x.Status == "Pending" || x.Status == "Reopened")
                .OrderBy(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            if (query == null) return;

            query.Status = "Assigned";
            query.AgentId = agentId;

            await _context.SaveChangesAsync();

            // Notify Agent
            await NotifyUser(agentId, "New Ticket Assigned", $"You have been assigned a new ticket: {query.Title}", true);
        }

        public async Task<List<QueryResponseDto>> GetMyQueries(HttpContext context)
        {
            var customerId = JwtHelper.GetUserId(context);
            return await _context.Queries
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new QueryResponseDto
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Title = x.Title,
                    Description = x.Description,
                    Response = x.Response,
                    Status = x.Status,
                    AgentId = x.AgentId,
                    CreatedDate = x.CreatedDate
                }).ToListAsync();
        }

        public async Task<List<QueryResponseDto>> GetPendingQueries()
        {
            return await _context.Queries
                .Where(x => x.Status == "Pending" || x.Status == "Reopened")
                .OrderBy(x => x.CreatedDate)
                .Select(x => new QueryResponseDto
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Title = x.Title,
                    Description = x.Description,
                    Response = x.Response,
                    Status = x.Status,
                    AgentId = x.AgentId,
                    CreatedDate = x.CreatedDate
                }).ToListAsync();
        }

        /// <summary>
        /// Returns the single query currently assigned to the logged-in Agent.
        /// </summary>
        public async Task<QueryResponseDto?> GetMyAssignedQuery(HttpContext context)
        {
            var agentId = JwtHelper.GetUserId(context);
            return await _context.Queries
                .Where(x => x.AgentId == agentId && x.Status == "Assigned")
                .Select(x => new QueryResponseDto
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Title = x.Title,
                    Description = x.Description,
                    Response = x.Response,
                    Status = x.Status,
                    AgentId = x.AgentId,
                    CreatedDate = x.CreatedDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task ResolveQuery(ResolveQueryRequest dto, HttpContext context)
        {
            var agentId = JwtHelper.GetUserId(context);
            var query = await _context.Queries.FindAsync(dto.QueryId);
            if (query == null) throw new Exception("Query not found");
            if (query.AgentId != agentId) throw new Exception("Unauthorized");

            query.Response = dto.Response;
            query.Status = "Resolved";

            await _context.SaveChangesAsync();

            // Notify Customer
            await NotifyUser(query.CustomerId, "Ticket Resolved", $"Your ticket '{query.Title}' has been resolved. Response: {dto.Response}", true);

            // Auto-assign next pending query to this now-free agent
            await AssignNextQuery(agentId);
        }

        public async Task ReopenQuery(ReopenQueryRequest dto, HttpContext context)
        {
            var customerId = JwtHelper.GetUserId(context);
            var query = await _context.Queries.FindAsync(dto.QueryId);
            if (query == null) throw new Exception("Query not found");
            if (query.CustomerId != customerId) throw new Exception("Unauthorized");

            query.Description += $"\n\n[Reopened]: {dto.AdditionalComment}";
            query.Status = "Reopened";
            query.AgentId = null;

            await _context.SaveChangesAsync();

            // Notify Customer (optional confirmation)
            await NotifyUser(customerId, "Ticket Reopened", $"Your ticket '{query.Title}' has been successfully reopened.", false);

            // Try to immediately re-assign to a free agent
            await AssignToFreeAgent(query.Id);
        }

        public async Task<List<QueryResponseDto>> GetAllQueries()
        {
            return await _context.Queries
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new QueryResponseDto
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    Title = x.Title,
                    Description = x.Description,
                    Response = x.Response,
                    Status = x.Status,
                    AgentId = x.AgentId,
                    CreatedDate = x.CreatedDate
                }).ToListAsync();
        }

        // ─── DOCUMENTS ───────────────────────────────────────────────────────────

        public async Task UploadQueryDocument(int queryId, IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var doc = new ClaimService.API.Mongo.QueryDocument
            {
                QueryId = queryId,
                FileName = file.FileName,
                Content = ms.ToArray()
            };

            await _queryMongo.InsertOneAsync(doc);
        }

        public async Task<ClaimDocument?> GetClaimDocument(int claimId)
        {
            return await _mongo.Find(x => x.ClaimId == claimId).FirstOrDefaultAsync();
        }

        public async Task<QueryDocument> GetQueryDocument(int queryId)
        {
            return await _queryMongo.Find(x => x.QueryId == queryId).FirstOrDefaultAsync();
        }

        public async Task<ClaimStatsDto> GetClaimStats()
        {
            var claims = await _context.Claims.ToListAsync();
            var queries = await _context.Queries.ToListAsync();

            return new ClaimStatsDto
            {
                TotalClaims = claims.Count,
                PendingClaims = claims.Count(c => c.Status == "Pending" || c.Status == "Assigned"),
                ApprovedClaims = claims.Count(c => c.Status == "Approved"),
                RejectedClaims = claims.Count(c => c.Status == "Rejected"),
                TotalQueries = queries.Count,
                PendingQueries = queries.Count(q => q.Status == "Pending" || q.Status == "Assigned" || q.Status == "Reopened"),
                ResolvedQueries = queries.Count(q => q.Status == "Resolved")
            };
        }

        public async Task<AgentStatsDto> GetAgentStats(HttpContext context)
        {
            var agentId = JwtHelper.GetUserId(context);
            var queries = await _context.Queries.Where(q => q.AgentId == agentId).ToListAsync();
            var pendingInQueue = await _context.Queries.CountAsync(q => q.Status == "Pending" || q.Status == "Reopened");

            return new AgentStatsDto
            {
                AssignedQueries = queries.Count(q => q.Status == "Assigned"),
                ResolvedQueries = queries.Count(q => q.Status == "Resolved"),
                PendingInQueue = pendingInQueue
            };
        }

        private async Task NotifyUser(Guid userId, string title, string message, bool sendEmail)
        {
            try
            {
                // Create in-app notification
                await _httpClient.PostAsJsonAsync("http://localhost:5286/api/notification/create", new
                {
                    userId = userId.ToString(),
                    title = title,
                    message = message
                });

                if (sendEmail)
                {
                    var user = await GetUserDetails(userId);
                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        await _httpClient.PostAsJsonAsync("http://localhost:5286/api/notification/send-email", new
                        {
                            toEmail = user.Email,
                            subject = title,
                            body = $"Hello {user.Name},\n\n{message}"
                        });
                    }
                }
            }
            catch { /* Ignore notification failures */ }
        }

        private async Task<UserDetailDto?> GetUserDetails(Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserDetailDto>($"http://localhost:5257/api/internal/user/{userId}");
            }
            catch { return null; }
        }
    }

    public class UserDetailDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class ClaimStatsDto
    {
        public int TotalClaims { get; set; }
        public int PendingClaims { get; set; }
        public int ApprovedClaims { get; set; }
        public int RejectedClaims { get; set; }
        public int TotalQueries { get; set; }
        public int PendingQueries { get; set; }
        public int ResolvedQueries { get; set; }
    }

    public class AgentStatsDto
    {
        public int AssignedQueries { get; set; }
        public int ResolvedQueries { get; set; }
        public int PendingInQueue { get; set; }
    }

    public class AdjusterStatsDto
    {
        public int AssignedClaims { get; set; }
        public int ApprovedClaims { get; set; }
        public int RejectedClaims { get; set; }
        public int PendingInQueue { get; set; }
    }
}
