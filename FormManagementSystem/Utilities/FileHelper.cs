using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
namespace FormManagementSystem.Utilities
{
    public static class FileHelper
    {
        public static async Task<string?> SaveFileAsync(IFormFile file, string rootPath, string subFolder = "uploads")
        {
            if (file == null || file.Length == 0) return null;
            var dir = Path.Combine(rootPath, subFolder);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var unique = Guid.NewGuid().ToString();
            var ext = Path.GetExtension(file.FileName);
            var fileName = unique + ext;
            var full = Path.Combine(dir, fileName);

            using var stream = new FileStream(full, FileMode.Create);
            await file.CopyToAsync(stream);
            return Path.Combine(subFolder, fileName).Replace("\\", "/");
        }
    }
}
