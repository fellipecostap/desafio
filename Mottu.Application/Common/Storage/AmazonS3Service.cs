using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using MimeKit;
using Desafio.Application.Common.Models;

namespace Desafio.Application.Common.Storage
{
    public class AmazonS3Service
    {

        public static async Task<GetObjectModel> GetObject(string name, AWS aws)
        {

            var client = new AmazonS3Client(aws.AccessKey, aws.AccessSecret, Amazon.RegionEndpoint.USEast2);

            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = aws.Bucket,
                Key = name
            };
            var responseObject = await client.GetObjectAsync(request);
            return new GetObjectModel
            {

                ContentType = responseObject.Headers.ContentType,
                Content = responseObject.ResponseStream
            };
        }

        public static async Task<UploadObjectModel> UploadObject(FileModel requestFile, AWS aws)
        {
            // connecting to the client
            var client = new AmazonS3Client(aws.AccessKey, aws.AccessSecret, Amazon.RegionEndpoint.USEast2);

            var file = Base64ToFile(requestFile);

            byte[] fileBytes = new byte[file.Length];

            file.OpenReadStream().Read(fileBytes, 0, int.Parse(file.Length.ToString()));

            var fileName = Guid.NewGuid() + "__" + file.FileName;

            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = aws.Bucket,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                response = await client.PutObjectAsync(request);
            };

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return new UploadObjectModel
                {
                    Success = true,
                    FileName = fileName
                };
            }
            else
            {
                return new UploadObjectModel
                {
                    Success = false,
                    FileName = fileName
                };
            }
        }


        private static IFormFile Base64ToFile(FileModel requestFile)
        {
            byte[] bytes = Convert.FromBase64String(requestFile.ContentFile);
            MemoryStream stream = new MemoryStream(bytes);

            IFormFile file = new FormFile(stream, 0, bytes.Length, requestFile.Name, requestFile.Name)
            {
                Headers = new HeaderDictionary(),
                ContentType = GetMimeType(requestFile.Name).MimeType
            };

            return file;
        }


        private static ContentType GetMimeType(string fileName)
        {
            var mimeType = MimeTypes.GetMimeType(fileName);

            return ContentType.Parse(mimeType);
        }


        public static bool IsValidImageFile(FileModel requestFile)
        {

            var file = Base64ToFile(requestFile);

            // Check file length
            if (file.Length <= 0)
            {
                return false;
            }

            // Check file extension to prevent security threats associated with unknown file types
            string[] permittedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains<string>(ext))
            {
                return false;
            }

            // Check if file size is greater than permitted limit
            if (file.Length > 10485760) // 10MB
            {
                return false;
            }

            return true;
        }

        public static bool IsValidAttachFile(FileModel requestFile)
        {

            var file = Base64ToFile(requestFile);

            // Check file length
            if (file.Length <= 0)
            {
                return false;
            }

            // Check file extension to prevent security threats associated with unknown file types
            string[] permittedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains<string>(ext))
            {
                return false;
            }

            // Check if file size is greater than permitted limit
            if (file.Length > 10485760) // 10MB
            {
                return false;
            }

            return true;
        }


        public static async Task<UploadObjectModel> RemoveObject(string fileName, AWS aws)
        {
            try
            {
                var client = new AmazonS3Client(aws.AccessKey, aws.AccessSecret, Amazon.RegionEndpoint.USEast2);

                var request = new DeleteObjectRequest
                {
                    BucketName = aws.Bucket,
                    Key = fileName
                };

                var response = await client.DeleteObjectAsync(request);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UploadObjectModel
                    {
                        Success = true,
                        FileName = fileName
                    };
                }
                else
                {
                    return new UploadObjectModel
                    {
                        Success = false,
                        FileName = fileName
                    };
                }
            }
            catch
            {
                return new UploadObjectModel
                {
                    Success = false,
                    FileName = fileName
                };
            }
        }

        public static string CreateLink(string fileName, AWS aws)
        {

            var link = "https://" + aws.Bucket + ".s3.us-east-2.amazonaws.com/" + fileName;
            return link;
        }

        public static string GetObjectByLink(string link)
        {
            var filename = link.Split("amazonaws.com/");
            return filename[1];
        }

    }
}