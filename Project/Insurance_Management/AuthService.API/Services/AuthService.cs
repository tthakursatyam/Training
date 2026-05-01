using AuthService.API.Data;
using AuthService.API.DTOs;
using AuthService.API.Entities;
using AuthService.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.API.Services
{
    public class AuthServices
    {
        private readonly IUserRepository _repo;
        private readonly TokenService _tokenService;
        private readonly AuthDbContext _context;
        private readonly PasswordHasher<User> _hasher;
        private readonly HttpClient _http;


        public AuthServices(IUserRepository repo, TokenService tokenService, AuthDbContext context, HttpClient http)
        {
            _repo = repo;
            _tokenService = tokenService;
            _context = context;
            _hasher = new PasswordHasher<User>();
            _http = http;
        }

        public async Task Register(RegisterRequest dto)
        {
            var existingUser = await _repo.GetByEmail(dto.Email);
            var otp = new Random().Next(100000, 999999).ToString();

            if (existingUser != null)
            {
                if (existingUser.IsActive)
                    throw new Exception("User already registered");
                
                // Update inactive user with new OTP
                existingUser.Name = dto.Name;
                existingUser.PasswordHash = _hasher.HashPassword(existingUser, dto.Password);
                existingUser.Otp = otp;
                existingUser.OtpExpiry = DateTime.UtcNow.AddMinutes(10);
                await _repo.Save();
            }
            else
            {
                var role = await _context.Roles
                    .FirstOrDefaultAsync(x => x.Name == "Customer");
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Email = dto.Email,
                    RoleId = role.Id,
                    IsActive = false,
                    Otp = otp,
                    OtpExpiry = DateTime.UtcNow.AddMinutes(10)
                };

                user.PasswordHash = _hasher.HashPassword(user, dto.Password);

                await _repo.AddUser(user);
                await _repo.Save();
            }

            // Send OTP email
            try
            {
                await _http.PostAsJsonAsync(
                    "http://localhost:5286/api/notification/send-email",
                    new
                    {
                        toEmail = dto.Email,
                        subject = "Verify Your Registration",
                        body = $"Hello {dto.Name},\n\nYour OTP for registration is: {otp}\nIt will expire in 10 minutes."
                    });
            }
            catch { /* Ignore notification failures */ }
        }

        public async Task VerifyRegistrationOtp(string email, string otp)
        {
            var user = await _repo.GetByEmail(email);
            if (user == null)
                throw new Exception("User not found");
            if (user.IsActive)
                throw new Exception("User is already active");
            if (user.Otp != otp || user.OtpExpiry < DateTime.UtcNow)
                throw new Exception("Invalid or expired OTP");

            user.IsActive = true;
            user.Otp = null;
            user.OtpExpiry = null;
            await _repo.Save();

            // Send welcome email to the newly registered user
            try
            {
                await _http.PostAsJsonAsync(
                    "http://localhost:5286/api/notification/send-email",
                    new
                    {
                        toEmail = user.Email,
                        subject = "Welcome to SecureLife Insurance! 🛡️",
                        body = $"Hello {user.Name},\n\n" +
                               $"Your account has been successfully verified and activated.\n\n" +
                               $"You can now log in and explore our insurance plans at http://localhost:4200\n\n" +
                               $"Here's what you can do:\n" +
                               $"  • Browse and purchase insurance plans\n" +
                               $"  • File claims with document upload\n" +
                               $"  • Track your claims in real time\n" +
                               $"  • Raise support queries anytime\n\n" +
                               $"Welcome aboard!\n\n" +
                               $"— The SecureLife Team"
                    });
            }
            catch { /* Ignore notification failures */ }

            // Notify Admins about the new registration
            await NotifyAdmins("New User Registration", $"A new user has registered: {user.Name} ({user.Email})");
        }

        public async Task ForgotPassword(string email)
        {
            var user = await _repo.GetByEmail(email);
            if (user == null || !user.IsActive)
                throw new Exception("User not found or inactive");

            var otp = new Random().Next(100000, 999999).ToString();
            user.Otp = otp;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(10);
            await _repo.Save();

            try
            {
                await _http.PostAsJsonAsync(
                    "http://localhost:5286/api/notification/send-email",
                    new
                    {
                        toEmail = email,
                        subject = "Password Reset OTP",
                        body = $"Hello {user.Name},\n\nYour OTP for resetting your password is: {otp}\nIt will expire in 10 minutes."
                    });
            }
            catch { /* Ignore notification failures */ }
        }

        public async Task ResetPassword(string email, string otp, string newPassword)
        {
            var user = await _repo.GetByEmail(email);
            if (user == null || !user.IsActive)
                throw new Exception("User not found or inactive");
            
            if (user.Otp != otp || user.OtpExpiry < DateTime.UtcNow)
                throw new Exception("Invalid or expired OTP");

            user.PasswordHash = _hasher.HashPassword(user, newPassword);
            user.Otp = null;
            user.OtpExpiry = null;
            await _repo.Save();
        }

        public async Task<AuthResponse> Login(LoginRequest dto)
        {
            var user = await _repo.GetByEmail(dto.Email);
            if (user == null)
                throw new Exception("Invalid credentials");
            if (!user.IsActive)
                throw new Exception("User is deactivated");

            var result = _hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid credentials");

            var token = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Role = user.Role.Name,
                UserId = user.Id,
                Name = user.Name
            };
        }
        public async Task<AuthResponse> RefreshToken(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(x => x.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (token == null || token.IsRevoked || token.ExpiryDate < DateTime.UtcNow)
                throw new Exception("Invalid refresh token");

            // 🔥 STEP 6: Revoke old token
            token.IsRevoked = true;

            // 🔥 Generate new access token
            var newAccessToken = _tokenService.CreateToken(token.User);

            // 🔥 Generate new refresh token
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            await _context.RefreshTokens.AddAsync(new RefreshToken
            {
                Token = newRefreshToken,
                UserId = token.UserId,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });

            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Role = token.User.Role.Name,
                UserId = token.User.Id,
                Name = token.User.Name
            };
        }
        public async Task RevokeToken(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (token != null)
            {
                token.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateAgent(CreateAgentRequest dto)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "Agent");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                RoleId = role.Id,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Notify Admins
            await NotifyAdmins("New Agent Created", $"A new agent has been created: {dto.Name} ({dto.Email})");
        }
        public async Task<List<UserListDto>> GetAllUsers()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Where(x => x.Role.Name == "Customer")
                .Select(x => new UserListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }
        public async Task<List<UserListDto>> GetAllAgents()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Where(x => x.Role.Name == "Agent")
                .Select(x => new UserListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<object> GetUserStats()
        {
            var users = await _context.Users.Include(x => x.Role).ToListAsync();
            return new
            {
                totalUsers = users.Count(u => u.Role.Name == "Customer"),
                totalAgents = users.Count(u => u.Role.Name == "Agent"),
                totalAdjusters = users.Count(u => u.Role.Name == "ClaimAdjuster"),
                activeUsers = users.Count(u => u.IsActive && u.Role.Name == "Customer"),
                deactivatedUsers = users.Count(u => !u.IsActive && u.Role.Name == "Customer")
            };
        }
        public async Task CreateClaimAdjuster(CreateClaimAdjusterRequest dto)
        {
            var existingUser = await _repo.GetByEmail(dto.Email);
            if (existingUser != null)
                throw new Exception("User already exists");

            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.Name == "ClaimAdjuster");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                RoleId = role.Id,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserListDto>> GetAllClaimAdjusters()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Where(x => x.Role.Name == "ClaimAdjuster")
                .Select(x => new UserListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }


        public async Task ToggleUserStatus(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);

            user.IsActive = !user.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task<UserListDto?> GetUserById(Guid id)
        {
            return await _context.Users
                .Include(x => x.Role)
                .Where(x => x.Id == id)
                .Select(x => new UserListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role.Name,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<UserListDto>> GetAdmins()
        {
            return await _context.Users
                .Include(x => x.Role)
                .Where(x => x.Role.Name == "Admin")
                .Select(x => new UserListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role.Name,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        private async Task NotifyAdmins(string title, string message)
        {
            try
            {
                var admins = await GetAdmins();
                foreach (var admin in admins)
                {
                    // Create in-app notification
                    await _http.PostAsJsonAsync("http://localhost:5286/api/notification/create", new
                    {
                        userId = admin.Id.ToString(),
                        title = title,
                        message = message
                    });
                }
            }
            catch { /* Ignore notification failures */ }
        }
    }
}