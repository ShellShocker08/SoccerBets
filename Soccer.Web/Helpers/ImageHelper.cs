using Microsoft.AspNetCore.Http;
using Soccer.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder = null)
        {            
            // Obtener Fecha Actual YY/MM/DD
            string year = DateTime.Today.Year.ToString();
            string month = DateTime.Today.Month.ToString();
            string day = DateTime.Today.Day.ToString();

            // Crear PATH del Folder            
            string pathFolder = $"{year}\\{month}\\{day}";

            // Checar que se haya mandado folder adicional para el PATH
            if (folder != null) pathFolder += $"\\{folder}";

            try
            {
                // Checar que el PATH exista, si no crearlo
                if (!Directory.Exists(pathFolder))
                    Directory.CreateDirectory(pathFolder);
            }
            catch (Exception ex)
            {
                return $"Error - Directory Not Found: {ex.InnerException.Message}";
            }

            // Crear Guid
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.jpg";

            // Combinar pathFolder con Guid
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\img\\{pathFolder}",
                file);

            try
            {
                // Crear Archivo dentro de la carpeta
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return $"Error - File Not Created: {ex.InnerException.Message}";
            }

            return $"~/img/{pathFolder}/{file}";
        }
    }
}
