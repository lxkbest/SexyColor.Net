using System.IO;
using Microsoft.AspNetCore.Http;


namespace SexyColor.CommonComponents
{
    public static class FormFileExtension
    {
        public static void SaveAs(this IFormFile file, string path)
        {
            if (file.Length > 0)
            {
                path = Path.GetExtension(path).IsNullOrWhiteSpace() ? Path.Combine(path, file.FileName) : path;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
        }

    }
}
