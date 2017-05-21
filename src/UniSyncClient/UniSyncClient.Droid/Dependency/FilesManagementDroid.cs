using System;
using System.Threading.Tasks;
using UniSyncClient.Droid.Dependency;
using UniSyncClient.Model.Dependency;
using UniSyncClient.View;
using UniSyncClient.ViewModel;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FilesManagementDroid))]
namespace UniSyncClient.Droid.Dependency
{
    public class FilesManagementDroid : IFilesManagement
    {
        public string StartFolder { get; } = PCLStorage.FileSystem.Current.LocalStorage.Path;

        public async Task<string> GetFolderPath()
        {
            var navigation = Application.Current.MainPage.Navigation;
            var modalPage = new DirectorySelectPage();

            await navigation.PushModalAsync(modalPage);

            return (modalPage.BindingContext as DirectorySelectViewModel)?.Folder.Path;
        }
    }
}