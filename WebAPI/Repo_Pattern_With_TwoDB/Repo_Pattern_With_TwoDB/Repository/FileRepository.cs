using MongoDB.Driver;
using Repo_Pattern_With_TwoDB.Data;
using Repo_Pattern_With_TwoDB.Models;

namespace Repo_Pattern_With_TwoDB.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly IMongoCollection<MongoFile> _mongoCollection;
        private readonly AppDbContext _context;

        public FileRepository(IMongoClient mongoClient, AppDbContext context)
        {
            var database = mongoClient.GetDatabase("FileDb");
            _mongoCollection = database.GetCollection<MongoFile>("Files");
            _context = context;
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var mongoFile = new MongoFile
            {
                Content = ms.ToArray()
            };

            await _mongoCollection.InsertOneAsync(mongoFile);

            var metadata = new FileMetadata
            {
                FileName = file.FileName,
                MongoId = mongoFile.Id,
                UploadDate = DateTime.UtcNow
            };

            _context.Files.Add(metadata);
            await _context.SaveChangesAsync();
        }
        public async Task<(byte[] Content, string FileName)> DownloadFileAsync(int id)
        {
            // STEP 1 → Get metadata from SQL
            var metadata = await _context.Files.FindAsync(id);

            if (metadata == null)
                throw new Exception("File not found");

            // STEP 2 → Get file from MongoDB using MongoId
            var filter = Builders<MongoFile>.Filter.Eq(x => x.Id, metadata.MongoId);

            var mongoFile = await _mongoCollection.Find(filter).FirstOrDefaultAsync();

            if (mongoFile == null)
                throw new Exception("File not found in MongoDB");

            return (mongoFile.Content, metadata.FileName);
        }
    }
}
