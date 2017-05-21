using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using PCLStorage;
using UniSyncClient.Annotations;
using UniSyncClient.Model;
using Xamarin.Forms;
using UniSyncClient.Model.Dependency;

namespace UniSyncClient.ViewModel
{
    public class FilesViewModel : INotifyPropertyChanged
    {
        public Command UpdateFileListCommand { get; private set; } = new Command(async () => await App.Storage.UpdateFiles());
        public Command SelectFolder { get; } = new Command(async () => App.Storage.Path = await DependencyService.Get<IFilesManagement>().GetFolderPath());
        public Command ResetFolder { get; } = new Command(
                () => App.Storage.SetDefaultFolder(),
                () => App.Storage.Path != App.Storage.DefaultPath);
        public Command SendFilesCommand { get; } = new Command(() => App.Storage.SendFileListToServer());

        public FilesViewModel()
        {
            App.Storage.PropertyChanged += (sender, args) => ResetFolder.ChangeCanExecute();
        }

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}