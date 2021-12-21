using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using SETraining.Shared;

namespace SETraining.Server.Repositories;

// Credit to Rasmus Lystrøm / github.com/ondfisk/BDSA2021
public class UploadRepository : IUploadRepository
{
    private readonly BlobContainerClient _client;

    public UploadRepository(BlobContainerClient client)
    {
        _client = client;
    }

    public async Task<(Status status, Uri uri)> CreateUploadAsync(string name, string contentType, Stream stream)
    {
        var client = _client.GetBlockBlobClient(name);
        
        await client.UploadAsync(stream);
        
        await client.SetHttpHeadersAsync(new Azure.Storage.Blobs.Models.BlobHttpHeaders {  ContentType = contentType });
        
        return (Status.Created, client.Uri);
    }
}