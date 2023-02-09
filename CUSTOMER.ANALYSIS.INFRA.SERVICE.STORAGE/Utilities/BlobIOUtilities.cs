using Azure;
using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.INFRA.SERVICE.STORAGE.Utilities
{
    internal sealed class BlobIOUtilities
    {
        private readonly string ConnectionString;

        internal BlobIOUtilities(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        internal async Task<(bool IsSucceed, string Message)> UpLoadFileAsync(string filePath, MemoryStream file, string containerName)
        {
            return await UpLoad(filePath, file, containerName);
        }

        internal async Task<(bool IsSucceed, string Message)> UpLoadFileAsync(string filePath, Stream file, string containerName)
        {
            return await UpLoad(filePath, file, containerName);
        }

        internal async Task<(bool IsSucceed, string Message)> UpLoad(string key, MemoryStream file, string containerName)
        {
            try
            {
                BlobContainerClient container = new BlobContainerClient(this.ConnectionString, containerName);
                if (!await container.ExistsAsync())
                    await container.CreateAsync();

                BlobClient blob = container.GetBlobClient(key);
                file.Position = 0;
                var response = await blob.UploadAsync(file);
                return (response.GetRawResponse().Status == 201, null);
            }
            catch (RequestFailedException ex)
            {
                return (false, string.Format("RequestFailedException: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (false, string.Format("Exception: {0}", ex.Message));
            }
        }
        
        internal async Task<(bool IsSucceed, string Message)> UpLoad(string key, Stream file, string containerName)
        {
            try
            {
                BlobContainerClient container = new BlobContainerClient(this.ConnectionString, containerName);
                if (!await container.ExistsAsync())
                    await container.CreateAsync();

                BlobClient blob = container.GetBlobClient(key);
                var response = await blob.UploadAsync(file);
                return (response.GetRawResponse().Status == 201, null);
            }
            catch (RequestFailedException ex)
            {
                return (false, string.Format("RequestFailedException: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (false, string.Format("Exception: {0}", ex.Message));
            }
        }

        internal async Task<(Stream Stream, string Message)> DownLoadFileAsync(string key, string containerName)
        {
            try
            {
                BlobContainerClient container = new BlobContainerClient(this.ConnectionString, containerName);
                if (!await container.ExistsAsync())
                    await container.CreateAsync();

                BlobClient blob = container.GetBlobClient(key);
                var response = await blob.DownloadAsync();
                return (response.Value.Content, null);
            }
            catch (RequestFailedException ex)
            {
                return (null, string.Format("RequestFailedException: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (null, string.Format("Exception: {0}", ex.Message));
            }
        }

        internal async Task<(bool IsSucceed, string Message)> DeleteFileAsync(string key, string containerName)
        {
            try
            {
                BlobContainerClient container = new BlobContainerClient(this.ConnectionString, containerName);
                if (!await container.ExistsAsync())
                    await container.CreateAsync();

                BlobClient blob = container.GetBlobClient(key);
                await blob.DeleteIfExistsAsync();
                return (true, null);
            }
            catch (RequestFailedException ex)
            {
                return (false, string.Format("RequestFailedException: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (false, string.Format("Exception: {0}", ex.Message));
            }
        }

        internal async Task<(bool IsSucceed, string Message)> FileExistsAsync(string key, string containerName)
        {
            try
            {
                BlobContainerClient container = new BlobContainerClient(this.ConnectionString, containerName);
                if (!await container.ExistsAsync())
                    await container.CreateAsync();

                BlobClient blob = container.GetBlobClient(key);
                var response = await blob.ExistsAsync();
                return (response.Value, null);
            }
            catch (RequestFailedException ex)
            {
                return (false, string.Format("RequestFailedException: {0}", ex.Message));
            }
            catch (Exception ex)
            {
                return (false, string.Format("Exception: {0}", ex.Message));
            }
        }
    }
}
