using System;
using System.Runtime.Serialization;

namespace UniSyncService.DataContract
{
    [DataContract]
    public enum FileAction
    {
        [EnumMember]
        None,

        [EnumMember]
        SendToClient,

        [EnumMember]
        SendToServer
    }
}