using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using UniSyncService.DataContract;

namespace UniSyncService
{
    [ServiceContract]
    interface IUniSyncService
    {
        [OperationContract]
        void Initialize();

        [OperationContract]
        FileStream ReceiveFileStreamFromServer(string name);

        [OperationContract]
        void SendFileStreamToServer(FileStream file, string name);

        [OperationContract]
        List<FileInfoDataContract> FileListToSend(List<FileInfoDataContract> fileList);
    }
}
