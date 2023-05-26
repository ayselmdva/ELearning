using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace elearning.Utilites
{
    public static class FileExtension
    {
        public static bool CheckFileType(this IFormFile file, string fileType)
        {
            return file.ContentType.Contains(fileType + "/");
        }

        public static bool CheckFileSize(this IFormFile file, int fileSize)
        {
            return file.Length / 1024 > fileSize;
        }

        public async static Task<string> SaveFileAsync(this IFormFile file, string root, string folder)
        {
            string uniqueFile=Guid.NewGuid().ToString()+"_"+file.FileName;
            string path=Path.Combine(root, "manage","images", folder,uniqueFile);
            using(FileStream fileStream=new FileStream(path, FileMode.Create))
            {
              await file.CopyToAsync(fileStream);
            }

            return uniqueFile;
        }

        public static void DeleteFile(this IFormFile file, string root, string folder,string fileName)
        {
            string path=Path.Combine(root,"manage", "images", folder, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


    }
}
