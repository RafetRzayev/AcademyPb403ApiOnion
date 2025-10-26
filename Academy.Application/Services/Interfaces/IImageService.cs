﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file, string basePath);
    }
}
