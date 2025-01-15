using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System.Web;
using microStore.Services.ProductApi.Controllers;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Models.DTO;

namespace microStore.Services.ProductApi.Helpers
{
    public class UploadImages
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ProductApiController> _logger;

        public UploadImages(ILogger<ProductApiController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<List<ProductImages>> UploadFile(IFormCollection form)
        {
            var azureConnectionString = _config["AzureUploadSettings:AzureStorageConnectionString"];
            var azureContainersAllowed = _config["AzureUploadSettings:AzureContainersAllowed"];
            var permittedFileExtensions = _config["AzureUploadSettings:FileUploadTypesAllowed"];
            var fileSizeLimit = _config.GetValue<long>("AzureUploadSettings:MaxFileUploadSize");

            // check that the Azure Storage connection string is available
            if (string.IsNullOrEmpty(azureConnectionString))
            {
                _logger.LogError("Azure Storage connection string is empty or has not been set in config/env variables");

            }

            // check that the Azure Storage container names is available
            if (string.IsNullOrEmpty(azureContainersAllowed))
            {
                _logger.LogError("Azure Storage container names was empty or has not been set in config/env variables");

            }



            var requestedContainer = form["container"].ToString();

            // check container name isn't empty
            if (requestedContainer.Length == 0)
            {
                _logger.LogError("Azure container name is empty");

            }

            // check requested container name is in an allowed set of names
            if (!azureContainersAllowed.Contains(requestedContainer.ToLowerInvariant()))
            {
                _logger.LogError("Invalid Azure container name supplied: {requestedContainer}", requestedContainer);

            }

            var files = form.Files;
            var urls = new List<ProductImages>();
            foreach (var file in files)
            {

                var fileExtension = Path.GetExtension(file.FileName.ToLowerInvariant());
                var fileNameLengthLimit = 75;

                List<string> basicFileChecks = BasicFileChecks(file, permittedFileExtensions, fileSizeLimit, fileNameLengthLimit, fileExtension);

                if (basicFileChecks is null)
                {
                    try
                    {
                        var azureContainer = new BlobContainerClient(azureConnectionString, requestedContainer);
                        var createResponse = await azureContainer.CreateIfNotExistsAsync();

                        // in case the container doesn't exist
                        if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        {
                            await azureContainer.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                        }

                        // generate a unique upload file name
                        // [original_filename_without_extension]_[8_random_chars].[original_filename_extension]
                        // eg. filename_xgh38tye.jpg
                        var fileName = HttpUtility.HtmlEncode(Path.GetFileNameWithoutExtension(file.FileName)) +
                            "_" + Path.GetRandomFileName().Substring(0, 8) + Path.GetExtension(file.FileName);

                        var blob = azureContainer.GetBlobClient(fileName);
                        //await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                        // set the content type (which may or may not have been provided by the client)
                        var blobHttpHeader = new BlobHttpHeaders();

                        if (file.ContentType != null)
                        {
                            blobHttpHeader.ContentType = file.ContentType;
                        }
                        else
                        {
                            blobHttpHeader.ContentType = fileExtension switch
                            {
                                ".jpg" => "image/jpeg",
                                ".jpeg" => "image/jpeg",
                                ".png" => "image/png",
                                _ => null
                            };
                        }

                        using (var fileStream = file.OpenReadStream())
                        {
                            await blob.UploadAsync(fileStream, blobHttpHeader);
                            urls.Add(new ProductImages
                            {
                                ImageLabel = file.Name,
                                ImageUrl = blob.Uri.AbsoluteUri
                            });
                        }
                        _logger.LogError("The file '{fileName}' was uploaded", file.FileName);
                    }

                    catch (Exception ex)
                    {
                        _logger.LogError("The file '{fileName}' could not be uploaded: {message}", file.FileName, ex.Message);
                        throw new AppException($"The file '{file.FileName}' could not be uploaded: {ex.Message}");
                    }
                }
                else
                {
                    _logger.LogError("The file '{fileName}' failed basic file checks and could not be uploaded", file.FileName);

                }
            }
            return urls;
        }
        private List<string> BasicFileChecks(IFormFile file, string permittedFileExtensions, long fileSizeLimit, int fileNameLengthLimit = 75, string fileExtension = "unknown")
        {
            var filecheckErrors = new List<string>();

            // check the file has an extension
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                _logger.LogError("'{fileName}' does not appear to have a file extension", file.FileName);
                filecheckErrors.Add($"'{file.FileName}' does not appear to have a file extension");
            }

            // check the file type is allowed
            if (!permittedFileExtensions.Contains(fileExtension))
            {
                _logger.LogError("Upload of '{fileName}' with file type '{fileExtension}' was not allowed", file.FileName, fileExtension);
                filecheckErrors.Add($"Upload of '{file.FileName}' with file type '{fileExtension}' is not allowed");
            }

            // check file isn't 0 bytes
            if (file.Length < 1)
            {
                _logger.LogError("The file '{fileName}' had a file size of 0 bytes", file.FileName);
                filecheckErrors.Add($"'{file.FileName}' has a file size of 0 bytes");
            }

            // check the file size (in bytes) isn't above the limit
            if (file.Length > fileSizeLimit)
            {
                _logger.LogError("The size of '{fileName}' ({fileSize} bytes) was larger than the current file size limit ({sizeLimit} bytes)", file.FileName, file.Length, fileSizeLimit);
                filecheckErrors.Add($"The size of '{file.FileName}' ({file.Length} bytes) is larger than the current file size limit");
            }

            // check the file name length isn't above the limit
            if (file.FileName.Length > fileNameLengthLimit)
            {
                _logger.LogError("The name of '{fileName}' was too long at {fileNameLength} characters. The current limit is {fileNameLengthLimit} characters", file.FileName, file.FileName.Length, fileNameLengthLimit);
                filecheckErrors.Add($"The name of '{file.FileName}' was too long: {file.FileName.Length}  characters");
            }

            if (filecheckErrors.Count == 0) return null!;

            return filecheckErrors;
        }
        public async void DeleteImages(String containerName, List<ProductImages> images)
        {
            var azureConnectionString = _config["AzureUploadSettings:AzureStorageConnectionString"];
            var azureContainersAllowed = _config["AzureUploadSettings:AzureContainersAllowed"];

            var azureContainer = new BlobContainerClient(azureConnectionString, containerName);
            foreach (var image in images)
            {
                await azureContainer.GetBlobClient(image.ImageUrl.Split("/")[0]).DeleteIfExistsAsync();

            }


        }
    }
}
