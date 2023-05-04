using System.Runtime.CompilerServices;

namespace ClassRoom.MVCP.Helper
{
    public class FileHelper
    {
        private const string Wwwroot = "wwwroot";
        private static void CheckDirectory(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }


        public static async Task<string> SaveUserImage(IFormFile file)
        {
            return await SaveFile(file, "UserImages");
        }

        public static async Task<string> SaveSchoolImage(IFormFile file)
        {
            return await SaveFile(file, "SchoolImages");
        }

        public static async Task<string> SaveFile(IFormFile formFile,string folder)
        { 
            CheckDirectory(Path.Combine(Wwwroot,folder));
            var fileName =  Guid.NewGuid()+ Path.GetExtension(formFile.FileName);
            var ms  = new MemoryStream();
            await formFile.CopyToAsync(ms);

            await File.WriteAllBytesAsync(Path.Combine(Wwwroot, folder, fileName),ms.ToArray());


            return $"/{folder}/{fileName}";

        }
    }
}
