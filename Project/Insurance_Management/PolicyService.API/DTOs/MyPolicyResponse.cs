namespace PolicyService.API.DTOs
{
    public class MyPolicyResponse
    {
        public int Id { get; set; }          // CustomerPolicy.Id — used for PDF download
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}