using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Documents;
using UniSyncClient.Model.Dependency;
using UniSyncClient.Uwp.Dependency;
using UniSyncClient.View;
using UniSyncClient.ViewModel;
using Xamarin.Forms;

namespace UniSyncClient.Uwp.Dependency
{
    public class FilesManagamentUwp : IFilesManagement
    {
        public string StartFolder { get; } = PCLStorage.FileSystem.Current.RoamingStorage.Path;

        public async Task<string> GetFolderPath()
        {
            var folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add("*");
            var result = await folderPicker.PickSingleFolderAsync();
            return result?.Path;
        }


    }
}