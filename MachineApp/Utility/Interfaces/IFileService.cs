using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MachineApp.Utility
{
    public interface IFileService
    {
        string CreateZip(string fileName, List<IFormFile> newFiles);
        string ZipDirectory(string zipName);
        string GetMimeType(string fileName);
        void DeleteFile(string fileName);
        bool FileExits(string fileName);
    }
}