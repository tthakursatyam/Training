namespace Repo_Pattern_With_TwoDB.Models
{
    public class FileMetadata
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string MongoId { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
