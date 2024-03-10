using CoreLayer.Enumerators;
using CoreLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ServiceLayer.Helpers.Generic.Image
{
    // This is our image helper logic ...
    public class ImageHelper : IImageHelper
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly string wwwRoot;

        private const string imageFolder = "images";
        private const string identityFolder = "user";
        private const string aboutFolder = "aboutUs";
        private const string portfolioFolder = "portfolios";
        private const string teamFolder = "team";
        private const string testimonalFolder = "testimonals";

        public ImageHelper(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            wwwRoot = _hostEnvironment.ContentRootPath + "/wwwroot/";
        }

        public async Task<ImageUploadModel> ImageUpload(IFormFile imageFile, ImageType imageType, string? folderName)
        {
            // First we are checking if we got a folderName becouse it is nullable parameter ...
            if(folderName == null)
            {
                switch(imageType)
                {
                    // Here we are figuring out what type of image we got and save it in proper folder ...
                    case ImageType.about:
                        folderName = aboutFolder;
                        break;

                    case ImageType.identity:
                        folderName = identityFolder;
                        break;

                    case ImageType.team:
                        folderName = teamFolder;
                        break;
                    
                    case ImageType.portfolio:
                        folderName = portfolioFolder;
                        break;
                    
                    case ImageType.testimonal:
                        folderName = testimonalFolder;
                        break;
                }
            }

            // Cheking directory exists or not, and if does not exist we are creating one ...
            if (!Directory.Exists($"{wwwRoot}/{imageFolder}/{folderName}")) 
            {
                Directory.CreateDirectory($"{wwwRoot}/{imageFolder}/{folderName}");
            }

            // We need only JPG or JPEG type of pictures so saveing extension in variable and then checking it ...
            string fileExtension = Path.GetExtension( imageFile.FileName ).ToLower();
            if( fileExtension != ".jpg" && fileExtension != ".jpeg")
            {
                return new ImageUploadModel { Error = "Please upload only JPG or JPEG files" };
            }

            // Now we are changing the image name and save it in new variable ...
            DateTime dateTime = DateTime.Now;
            var newFileName = folderName + "_" + dateTime.Microsecond.ToString() + fileExtension;

            // This is our image path where images will be saved ...
            string path = Path.Combine($"{wwwRoot}/{imageFolder}/{folderName}", newFileName);

            await using var stream = new FileStream (path, FileMode.Create, FileAccess.Write, FileShare.None, 1024*1024, useAsync:false);
            await imageFile.CopyToAsync (stream);
            await stream.FlushAsync();

            return new ImageUploadModel { FileName = $"{folderName}/{newFileName}", FileType = imageFile.ContentType };
        }

        public string DeleteImage(string imageName)
        {
            var fileToDelete = Path.Combine($"{wwwRoot}/{imageFolder}/{imageName}");
            if(File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
            }

            return "Image Is Deleted";
        }
    }
}
