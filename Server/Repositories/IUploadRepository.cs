using SETraining.Shared;

namespace SETraining.Server.Repositories;

public interface IUploadRepository
{
    Task<(Status status, Uri uri)> CreateUploadAsync(string name, string contentType, Stream stream);
}