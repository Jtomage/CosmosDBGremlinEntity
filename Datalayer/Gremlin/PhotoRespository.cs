using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Datalayer.Structures;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Datalayer.Gremlin
{
	public class PhotoRepository : RepositoryBase
	{

		private CloudStorageAccount storageAccount;

		private CloudBlobContainer blobContainer;

		public PhotoRepository(string storageConnectionString)
		{
			if (!CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
				throw new ArgumentException("Connection string is invalid");
		}

		public async Task InsertPhoto(Photo p, User u)
		{
			//Insert Photo information and connect it to user
			await UploadPhoto(p);
		}

		private async Task UploadPhoto(Photo p)
		{
			try
			{
				CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
				CloudBlobContainer container = blobClient.GetContainerReference("photosdev");
				CloudBlockBlob blockBlob = container.GetBlockBlobReference(p.Id);
				using (MemoryStream stream = new MemoryStream(p.Image))
				{
					await blockBlob.UploadFromStreamAsync(stream);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
