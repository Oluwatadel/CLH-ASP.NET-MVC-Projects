using DMSMVC.Repository.Interface;

namespace DMSMVC.Repository.Implementation
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment _environment;

        public FileRepository(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        public string Upload(IFormFile file)
        {
            var uploadedFile = file.ContentType.Split('/');
            var newFileName = $"{uploadedFile[0]}{Guid.NewGuid().ToString().Substring(1, 6)}{uploadedFile[1]}";
            var filePath = "";

            if (uploadedFile[1] == "doc" || uploadedFile[1] == "txt" || uploadedFile[1] == "xlsx" || uploadedFile[1] == "pdf")
            {
                filePath = Path.Combine("~/", "Documents");
            }
            else if(uploadedFile[1] == "jpg" || uploadedFile[1] == "jpeg" || uploadedFile[1] == "png")
            {
                filePath = Path.Combine("~/", "userImages");
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var newFilePath = Path.Combine(filePath, newFileName);
            using (var fileTobeUploaded = new FileStream(newFilePath, FileMode.Create))
            {
                file.CopyTo(fileTobeUploaded);
            }
            return newFilePath;
        }
    }
}
