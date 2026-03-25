namespace Repo_Pattern_With_TwoDB.Repository
{
    public interface IFileRepository
    {
        Task UploadFileAsync(IFormFile file);
        Task<(byte[] Content, string FileName)> DownloadFileAsync(int id);
    }
}
