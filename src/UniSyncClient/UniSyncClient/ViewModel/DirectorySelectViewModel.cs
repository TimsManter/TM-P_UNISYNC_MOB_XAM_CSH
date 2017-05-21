using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PCLStorage;
using UniSyncClient.Annotations;
using UniSyncClient.Model.Dependency;
using Xamarin.Forms;

namespace UniSyncClient.ViewModel
{
    public class DirectorySelectViewModel : INotifyPropertyChanged
    {
        public IFolder Folder { get; private set; }
        public IList<IFolder> Folders { get; private set; }

        public Command SelectFolderCommand { get; private set; }

        public DirectorySelectViewModel()
        {
            SelectFolderCommand = new Command(folder => Folder = folder as IFolder);

            UpdateFolder();
        }

        private async void UpdateFolder(IFolder folder = null)
        {
            if (folder == null)
            {
                Debug.WriteLine("Start folder: " + DependencyService.Get<IFilesManagement>().StartFolder);
                Folder = await FileSystem.Current.GetFolderFromPathAsync(DependencyService.Get<IFilesManagement>().StartFolder);
            }
            else
            {
                Folder = folder;
            }
            OnPropertyChanged(nameof(Folder));

            Folders = await Folder.GetFoldersAsync();
            OnPropertyChanged(nameof(Folders));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}