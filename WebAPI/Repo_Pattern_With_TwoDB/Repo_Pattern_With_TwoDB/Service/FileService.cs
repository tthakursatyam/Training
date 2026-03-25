using Repo_Pattern_With_TwoDB.Repository;

namespace Repo_Pattern_With_TwoDB.Service
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _repo;

        public FileService(IFileRepository repo)
        {
            _repo = repo;
        }

        public async Task UploadAsync(IFormFile file)
        {
            await _repo.UploadFileAsync(file);
        }

        public async Task<(byte[] Content, string FileName)> DownloadAsync(int id)
        {
            return await _repo.DownloadFileAsync(id);
        }
    }
}
