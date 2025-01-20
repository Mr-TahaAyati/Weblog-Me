using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CoreLayer.Services.FileManager
{
    public interface IFileManager
    {
        string SaveFile(IFormFile file, string savePath);
    }

    public class FileManager : IFileManager
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" }; // افزودن پسوندهای مجاز

        public string SaveFile(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("File Is Null");

            // بررسی فرمت فایل
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (Array.IndexOf(_allowedExtensions, fileExtension) < 0)
            {
                throw new Exception("Invalid file format. Only jpg, jpeg, and png files are allowed.");
            }

            // Generating a unique file name
            var fileName = $"{Guid.NewGuid()}{fileExtension}"; // استفاده از پسوند اصلی فایل

            // Combining the directory path and save path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));

            // Check if directory exists, if not, create it
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Full file path to save
            var fullPath = Path.Combine(folderPath, fileName); // Correct way to combine paths

            // Saving the file to the desired location
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }
    }
}
