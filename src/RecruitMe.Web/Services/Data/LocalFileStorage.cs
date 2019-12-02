using Microsoft.AspNetCore.Http;
using RecruitMe.Logic.Data;
using RecruitMe.Web.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Services.Data
{
    public class LocalFileStorage : IFileSaver, IPictureSaver, IFileRepository
    {
        public void Delete(string url)
        {
            File.Delete(url);
        }
        
        public Stream Get(string url)
        {
            return new FileStream(url, FileMode.Open);
        }
        
        async Task<string> IFileSaver.SaveAsync(Stream stream, string name)
        {
            var url = FileStorageConfiguration.PrivateFilesRoot + Guid.NewGuid().ToString() + name;

            using (var file = new FileStream(url, FileMode.Create))
            {
                await stream.CopyToAsync(file);
            }

            return url;
        }

        async Task<string> IPictureSaver.SaveAsync(Stream stream, string name)
        {
            var url = FileStorageConfiguration.ProfilePictures + Guid.NewGuid().ToString() + name;

            using (var file = new FileStream(url, FileMode.Create))
            {
                await stream.CopyToAsync(file);
            }

            return url;
        }
    }
}
