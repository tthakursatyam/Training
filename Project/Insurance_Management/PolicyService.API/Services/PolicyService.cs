using Microsoft.EntityFrameworkCore;
using PolicyService.API.Data;
using PolicyService.API.DTOs;
using PolicyService.API.Entities;
using PolicyService.API.ExternalServices;
using PolicyService.API.Helpers;
using iText.Layout;

namespace PolicyService.API.Services
{
    public class PolicyServices
    {
        private readonly PolicyDbContext _context;
        private readonly PaymentClient _paymentClient;
        private readonly NotificationClient _notification;

        public PolicyServices(PolicyDbContext context, PaymentClient paymentClient, NotificationClient notification)
        {
            _context = context;
            _paymentClient = paymentClient;
            _notification = notification;
        }

        public async Task CreatePolicy(CreatePolicyRequest dto)
        {
            var policy = new Policy
            {
                Name = dto.Name,
                Type = dto.Type,
                Premium = dto.Premium,
                Coverage = dto.Coverage,
                Terms = dto.Terms
            };

            await _context.Policies.AddAsync(policy);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePolicy(int id, CreatePolicyRequest dto)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
                throw new Exception("Policy not found");

            policy.Name     = dto.Name;
            policy.Type     = dto.Type;
            policy.Premium  = dto.Premium;
            policy.Coverage = dto.Coverage;
            policy.Terms    = dto.Terms;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePolicy(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
                throw new Exception("Policy not found");

            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Policy>> GetAllPolicies()
        {
            return await _context.Policies.ToListAsync();
        }
        public async Task<object> PurchasePolicy(PurchasePolicyRequest dto, HttpContext context)
        {
            var userId = JwtHelper.GetUserId(context);

            var policy = await _context.Policies.FindAsync(dto.PolicyId);

            if (policy == null)
                throw new Exception("Policy not found");

            var email = JwtHelper.GetEmail(context);

            await _notification.SendEmail(
                email,
                "Policy Purchase Successful",
                "Your policy has been activated"
            );

            // ✅ Create Customer Policy (THIS WAS MISSING)
            var customerPolicy = new CustomerPolicy
            {
                UserId = userId,
                PolicyId = policy.Id,
                PolicyNumber = "POL" + DateTime.UtcNow.Ticks,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddYears(1),
                Status = "Active"
            };

            await _context.CustomerPolicies.AddAsync(customerPolicy);
            await _context.SaveChangesAsync();

            return new
            {
                message = "Policy purchased successfully",
                policyNumber = customerPolicy.PolicyNumber
            };
        }
        public async Task<List<MyPolicyResponse>> GetMyPolicies(HttpContext context)
        {
            var userId = JwtHelper.GetUserId(context);

            var result = await _context.CustomerPolicies
                .Where(x => x.UserId == userId)
                .Join(_context.Policies,
                    cp => cp.PolicyId,
                    p => p.Id,
                    (cp, p) => new MyPolicyResponse
                    {
                        Id = cp.Id,           // CustomerPolicy.Id — needed for PDF download
                        PolicyId = p.Id,
                        PolicyName = p.Name,
                        PolicyNumber = cp.PolicyNumber,
                        StartDate = cp.StartDate,
                        EndDate = cp.EndDate,
                        Status = cp.Status
                    })
                .ToListAsync();

            return result;
        }
        public async Task<byte[]> GeneratePolicyPdf(int customerPolicyId)
        {
            var data = await _context.CustomerPolicies
                .Where(cp => cp.Id == customerPolicyId)
                .Join(_context.Policies,
                    cp => cp.PolicyId,
                    p => p.Id,
                    (cp, p) => new
                    {
                        cp.PolicyNumber,
                        cp.StartDate,
                        cp.EndDate,
                        cp.Status,
                        p.Name,
                        p.Premium
                    })
                .FirstOrDefaultAsync();

            if (data == null)
                throw new Exception("Policy not found");

            using (var ms = new MemoryStream())
            {
                var writer = new iText.Kernel.Pdf.PdfWriter(ms);
                var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                var document = new iText.Layout.Document(pdf);
                var boldFont = iText.Kernel.Font.PdfFontFactory.CreateFont(
                        iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

                document.Add(new iText.Layout.Element.Paragraph("Policy Document").SetFont(boldFont));

                document.Add(new iText.Layout.Element.Paragraph($"Policy Name: {data.Name}"));
                document.Add(new iText.Layout.Element.Paragraph($"Policy Number: {data.PolicyNumber}"));
                document.Add(new iText.Layout.Element.Paragraph($"Premium: {data.Premium}"));
                document.Add(new iText.Layout.Element.Paragraph($"Start Date: {data.StartDate}"));
                document.Add(new iText.Layout.Element.Paragraph($"End Date: {data.EndDate}"));
                document.Add(new iText.Layout.Element.Paragraph($"Status: {data.Status}"));

                document.Close();

                return ms.ToArray();
            }
        }
        public async Task RenewPolicy(RenewPolicyRequest dto, HttpContext context)
        {
            var userId = JwtHelper.GetUserId(context);

            var policy = await _context.CustomerPolicies
                .FirstOrDefaultAsync(x => x.Id == dto.CustomerPolicyId);

            if (policy == null)
                throw new Exception("Policy not found");

            if (policy.UserId != userId)
                throw new Exception("Unauthorized access");

            if (policy.Status != "Active")
                throw new Exception("Only active policies can be renewed");

            policy.EndDate = policy.EndDate.AddYears(1);

            await _context.SaveChangesAsync();
        }
        public async Task ConfirmPayment(ConfirmPaymentRequest dto)
        {
            var userId = dto.UserId;

            var policy = await _context.Policies.FindAsync(dto.PolicyId);

            if (policy == null)
                throw new Exception("Policy not found");

            var customerPolicy = new CustomerPolicy
            {
                UserId = userId,
                PolicyId = policy.Id,
                PolicyNumber = "POL" + DateTime.UtcNow.Ticks,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddYears(1),
                Status = "Active"
            };

            await _context.CustomerPolicies.AddAsync(customerPolicy);
            await _context.SaveChangesAsync();

            // Send purchase confirmation email to the customer
            try
            {
                // Fetch user details (name + email) from AuthService
                using var http = new HttpClient();
                var userResponse = await http.GetAsync($"http://localhost:5257/api/internal/user/{userId}");

                if (userResponse.IsSuccessStatusCode)
                {
                    var userJson = await userResponse.Content.ReadAsStringAsync();
                    var user = System.Text.Json.JsonSerializer.Deserialize<UserDetailDto>(
                        userJson,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (user != null && !string.IsNullOrEmpty(user.Email))
                    {
                        await _notification.SendEmail(
                            user.Email,
                            $"Policy Purchase Confirmed — {policy.Name} 🛡️",
                            $"Hello {user.Name},\n\n" +
                            $"Your payment was successful and your policy is now active!\n\n" +
                            $"Policy Details:\n" +
                            $"  • Policy Name:   {policy.Name}\n" +
                            $"  • Policy Number: {customerPolicy.PolicyNumber}\n" +
                            $"  • Type:          {policy.Type}\n" +
                            $"  • Premium:       ₹{policy.Premium}/month\n" +
                            $"  • Coverage:      {policy.Coverage}\n" +
                            $"  • Start Date:    {customerPolicy.StartDate:dd MMM yyyy}\n" +
                            $"  • End Date:      {customerPolicy.EndDate:dd MMM yyyy}\n" +
                            $"  • Status:        Active\n\n" +
                            $"You can download your policy document anytime from your dashboard at http://localhost:4200\n\n" +
                            $"If you have any questions, raise a support query from your dashboard and our team will assist you.\n\n" +
                            $"Thank you for choosing SecureLife!\n\n" +
                            $"— The SecureLife Team"
                        );
                    }
                }
            }
            catch
            {
                // Email failure should not block policy activation
            }
        }

        public async Task<object> GetPolicyStats()
        {
            var availablePolicies = await _context.Policies.CountAsync();
            var purchasedPolicies = await _context.CustomerPolicies.ToListAsync();
            var revenue = await _context.CustomerPolicies
                .Join(_context.Policies, cp => cp.PolicyId, p => p.Id, (cp, p) => p.Premium)
                .SumAsync();

            return new
            {
                totalAvailablePolicies = availablePolicies,
                totalPurchasedPolicies = purchasedPolicies.Count,
                totalRevenue = revenue,
                activePolicies = purchasedPolicies.Count(p => p.Status == "Active"),
                expiredPolicies = purchasedPolicies.Count(p => p.Status == "Expired")
            };
        }
    }
}