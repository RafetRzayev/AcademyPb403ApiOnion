using Academy.Application.Constants;
using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Managers
{
    public class ImageManager : IImageService
    {
        public async Task<string> SaveImageAsync(IFormFile file, string basePath)
        {
            if (file != null)
            {
                if (!file.ContentType.StartsWith("image")) throw new Exception("Only image files are allowed.");

                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                var orijinalFileName = file.FileName;
                var fileExtension = Path.GetExtension(orijinalFileName);
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(orijinalFileName);
                var uniqueFileName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}{fileExtension}";

                var path = Path.Combine(basePath, uniqueFileName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                stream.Close();

                return uniqueFileName;
            }

            return string.Empty;
        }
    }
}
