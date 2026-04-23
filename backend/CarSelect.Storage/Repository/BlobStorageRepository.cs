using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;

public class BlobStorageRepository : IBlobStorageRepository
{
    private readonly BlobContainerClient _container;
    public BlobStorageRepository(IOptions<BlobStorageRepositoryOptions> options)
    {
        var client = new BlobServiceClient(options.Value.Connectionstring);
        _container = client.GetBlobContainerClient(options.Value.ContainerName);
    }
    public async Task DeleteImageAsync(string imageUrl)
    {
        // we extract the blobsName AKA the Id
        var blobName = Path.GetFileName(new Uri(imageUrl).LocalPath);
        // This just creates a reference to the specific Blob you wish to target
        var blobClient = _container.GetBlobClient(blobName);
        // This one performs the operation of deleting only if it exists
        await blobClient.DeleteIfExistsAsync();
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName, string contentType)
    {
        var blobName = $"{Guid.NewGuid()}_{fileName}";
        var blobClient = _container.GetBlobClient(blobName);

        await blobClient.UploadAsync(imageStream, new BlobHttpHeaders
        {
            ContentType = contentType // the type of image it is: JPEG, PNG, etc...
        });

        // return the full URL to the stored image
        return blobClient.Uri.ToString();
    }
}