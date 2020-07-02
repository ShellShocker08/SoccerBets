using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Soccer.Web.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        private readonly IConfiguration _config;

        public ImageHelper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> UploadImageAsync(IFormFile imageFile, string folder)
        {
            // Obtener Fecha Actual YY/MM/DD
            string year = DateTime.Today.Year.ToString();
            string month = DateTime.Today.Month.ToString();
            string day = DateTime.Today.Day.ToString();

            // Crear PATH del Folder         
            string pathFolder = $"{year}\\{month}\\{day}";            

            // Checar que se haya mandado folder adicional para el PATH
            if (folder != null)
            {
                pathFolder += $"\\{folder}";
            }

            // Crear Guid
            string guid = Guid.NewGuid().ToString();
            string file = $"{guid}.jpg";

            string serverFolder = _config.GetValue<string>(
                "Static:ImageFolder");

            // Crear path
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"{serverFolder}\\{pathFolder}");

            try
            {
                // Checar que el PATH exista, si no crearlo
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                return $"Error - Directory Not Found: {ex.InnerException.Message}";
            }

            // Combinar path con archivo
            path = Path.Combine($"{path}",
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

        public async Task<string> UpdateImageAsync(string pathOldFile, IFormFile imageFile, string folder)
        {
            // Remover del pathOldFile el PATH relativo [ ~/ ]
            pathOldFile = pathOldFile.Remove(0, 2);

            // Recuperar PATH del Archivo Anterior
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\{pathOldFile}");
            
            try
            {
                // Borrar Archivo Anterior
                File.Delete(path);

                // Subir Nueva Imagen
                return await UploadImageAsync(imageFile, folder);
            }
            catch (Exception ex)
            {
                return $"Error - File Not Updated: {ex.InnerException.Message}";
            }
        }
    }
}
