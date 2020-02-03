using System;
using System.Threading.Tasks;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Datalayer.Gremlin
{

	/// <summary>
	/// There is a SIZE Limit when upload to Azure storage General storage account type. Storage clients default to a 128 MB maximum single blob upload
	/// https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet#create-a-container
	/// https://github.com/Azure/azure-storage-net/issues/732
	/// https://github.com/Azure/azure-sdk-for-net/issues/8941
	/// https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images?tabs=dotnet
	/// </summary>
	public class PhotoRepository : RepositoryBase
	{
		private readonly BlobServiceClient serviceClient;

		public PhotoRepository(string connectionString)
		{
			if (String.IsNullOrWhiteSpace(connectionString))
				throw new ArgumentNullException("Connectiong String is null or empty");

			serviceClient = new BlobServiceClient(connectionString);
		}

		public async Task CreateContainer(string containerName, string localPath)
		{
			BlobContainerClient containerClient = await serviceClient.CreateBlobContainerAsync(containerName);
			string fileName = Path.GetFileName(localPath);
			BlobClient blobClient = containerClient.GetBlobClient(fileName);
			using (FileStream uploadFileStream = File.OpenRead(localPath))
			{
				var uploadInfo = await blobClient.UploadAsync(uploadFileStream);
			}
		}

		public async Task<BlobContentInfo> UploadPhoto(string containerName, string localPath)
		{
			//https://docs.microsoft.com/en-us/rest/api/storageservices/understanding-block-blobs--append-blobs--and-page-blobs
			//Storage clients default to a 128 MB maximum single blob upload

			BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
			string fileName = "test1.jpg";
			//Create the blob that uploading to
			BlobClient blobClient = containerClient.GetBlobClient(fileName);

			using (FileStream uploadFileStream = File.OpenRead(localPath))
			{
				var uploadInfo = await blobClient.UploadAsync(uploadFileStream);
				return uploadInfo;
			}
		}

		public async Task DownloadPhoto(string containerName, string localPath)
		{
			BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
			BlobClient blobClient = containerClient.GetBlobClient("test1.jpg");
			BlobDownloadInfo download = await blobClient.DownloadAsync();

			string path = Path.Combine(Path.GetDirectoryName(localPath), "download.jpg");

			using (FileStream fs = File.OpenWrite(path))
			{
				await download.Content.CopyToAsync(fs);
			}
		}

	}
}
