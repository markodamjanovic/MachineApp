using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MachineApp.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.StaticFiles;

namespace MachineApp.Utility
{
    public class FileService : IFileService
    {
        const string FILES_FOLDER = "files";
        const string TEMP_FOLDER = "temp";

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MalfunctionController> _logger;

        public FileService(IWebHostEnvironment webHostEnvironment, ILogger<MalfunctionController> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public string ZipDirectory(string zipName) => GetFullFilePath(FILES_FOLDER, zipName);
        public bool FileExits(string fileName) => File.Exists(GetFullFilePath(FILES_FOLDER, fileName));

        public string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        public string CreateZip(string zipName, List<IFormFile> newFiles)
        {   
            bool result = true;

            string fileFolderPath = GetUploadsFolder(FILES_FOLDER);
            string tempFolder = GetUploadsFolder(TEMP_FOLDER);

            CheckDirectories(fileFolderPath, tempFolder);

            if(string.IsNullOrEmpty(zipName))
            {
               zipName = $"{Guid.NewGuid().ToString()}.zip";
            }
            else
            {
                result = ExtractZip(zipName, fileFolderPath, tempFolder);
            }

            if(result)
            {

                foreach (var formFile in newFiles)
                {
                    var fileName = Path.Combine(tempFolder, formFile.FileName);
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(fileName, FileMode.Create))
                        {
                            formFile.CopyToAsync(stream);
                        }
                    }
                }

                ZipFile.CreateFromDirectory(tempFolder, GetFullFilePath(fileFolderPath, zipName));
                DeleteTempFiles(tempFolder);
                
                return zipName;
            }
            else
            {
                return null;
            }
        }

        private bool ExtractZip(string zipName, string filesFolderPath, string tempFilePath)
        {   
            try
            {
                string fullPath = GetFullFilePath(filesFolderPath, zipName);
                
                DeleteTempFiles(tempFilePath);
                ZipFile.ExtractToDirectory(fullPath, tempFilePath, true);
                
                File.Delete(fullPath);

                return true;
            }
            catch (Exception)
            {
                _logger.LogError("Error while extracting files");
                return false;
            }
        }

        private void DeleteTempFiles(string filePath)
        {
            DirectoryInfo directory = new DirectoryInfo(filePath);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete(); 
            }
        }

        private void CheckDirectories( string filePath, string tempFilePath)
        {   
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!Directory.Exists(tempFilePath))
            {
                Directory.CreateDirectory(tempFilePath);
            }
        }

         private string GetUploadsFolder(string folder)
        {
            return Path.Combine(_webHostEnvironment.WebRootPath, folder);
        }

        private string GetFullFilePath(string folder, string fileName)
        {
            return Path.Combine(GetUploadsFolder(folder), fileName);
        }

        public void DeleteFile(string fileName)
        {   
            string directory = GetFullFilePath(FILES_FOLDER, fileName);
            if(File.Exists(directory))
            {
                File.Delete(directory);
            }
        }
    }
}
