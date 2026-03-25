namespace Repo_Pattern_With_TwoDB.Service
{
    public interface IFileService
    {
        Task UploadAsync(IFormFile file);
        Task<(byte[] Content, string FileName)> DownloadAsync(int id);
    }
}
