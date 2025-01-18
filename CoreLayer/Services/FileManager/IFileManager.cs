using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.FileManager
{
   public interface IFileManager
    {
        string SaveFile(IFormFile file,string savePath);
    }

    public class FileManager : IFileManager
    {
        public string SaveFile(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("File Is Null");

            var fileName= $"{Guid.NewGuid()}{file.FileName}";

            var folderPath=Path.Combine(Directory.GetCurrentDirectory(),savePath.Replace("/","\\"));
            
            if(Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fullPath= Path.Combine(folderPath+fileName);

            using var stram = new FileStream(folderPath,FileMode.Create);
            file.CopyTo(stram);
            return fileName;

        }
    }
}
