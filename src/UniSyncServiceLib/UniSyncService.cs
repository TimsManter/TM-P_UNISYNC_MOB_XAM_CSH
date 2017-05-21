using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using UniSyncService.DataContract;

namespace UniSyncService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class UniSyncService : IUniSyncService
    {
        private readonly string _path = AppDomain.CurrentDomain.BaseDirectory + @"\Files\";

        public List<FileInfoDataContract> FileListToSend(List<FileInfoDataContract> fileList)
        {
            var files = new List<FileInfoDataContract>();

            foreach (var fileInfo in fileList)
            {
                var file = new FileInfoDataContract {Name = fileInfo.Name};
                files.Add(file);

                // TODO: Add action to contract
            }

            return Directory.EnumerateFiles(_path).Select(fileName => new FileInfoDataContract { Name = fileName }).ToList();
        }

        public void Initialize()
        {
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
        }

        public FileStream ReceiveFileStreamFromServer(string name)
        {
            return File.OpenRead(_path + name);
        }

        public void SendFileStreamToServer(FileStream file, string name)
        {
            var newFile = File.Create(_path + name);
            file.CopyTo(newFile);
        }
    }
}
