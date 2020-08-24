using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Interfaces
{
    public interface IImageHelper
    {       
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
        Task<string> UpdateImageAsync(string pathOldFile, IFormFile imageFile, string folder);
        string UploadImage(byte[] pictureArray, string folder);
    }
}
