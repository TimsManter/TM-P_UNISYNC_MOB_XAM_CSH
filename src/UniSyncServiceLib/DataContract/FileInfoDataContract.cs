using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using UniSyncService.Annotations;

namespace UniSyncService.DataContract
{
    [DataContract]
    public class FileInfoDataContract
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime ModifyDateTime { get; set; }

        [DataMember]
        public FileAction Action { get; set; }
    }
}