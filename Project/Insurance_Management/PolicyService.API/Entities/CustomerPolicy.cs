using System.ComponentModel.DataAnnotations;

namespace PolicyService.API.Entities
{
    public class CustomerPolicy
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }
    }
}