using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PCLStorage;
using UniSyncClient.Annotations;
using UniSyncClient.Model.Dependency;
using UniSyncClient.UniSyncService;
using Xamarin.Forms;

namespace UniSyncClient.Model
{
    public class Storage : INotifyPropertyChanged
    {
        public readonly string DefaultPath = DependencyService.Get<IFilesManagement>().StartFolder;

        private static string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged();
                SetFolder(_path);
            }
        }

        private IFolder _synchronizationFolder;
        public IFolder SynchronizationFolder
        {
            get { return _synchronizationFolder; }
            set
            {
                _synchronizationFolder = value;
                OnPropertyChanged();
            }
        }

        private IList<IFile> _synchronizationFiles;
        public IList<IFile> SynchronizationFiles
        {
            get { return _synchronizationFiles; }
            set
            {
                _synchronizationFiles = value;
                OnPropertyChanged();
            }
        }

        public Storage()
        {
            SetDefaultFolder();
        }

        public async Task UpdateFiles()
        {
            SynchronizationFiles = await SynchronizationFolder.GetFilesAsync();
            Debug.WriteLine("Storage: Files updated");
        }

        public async void SetFolder(string path)
        {
            try
            {
                SynchronizationFolder = await FileSystem.Current.GetFolderFromPathAsync(path);
                Debug.WriteLine("Storage: Folder changed");
                await UpdateFiles();
            }
            catch (UnauthorizedAccessException)
            {
                await
                    Application.Current.MainPage.DisplayAlert("Błąd",
                        "Nie można ustawić ścieżki z powodu ograniczonego dostępu.\n\nPrzywracam ścieżkę domyślną.",
                        "OK");
                SetDefaultFolder();
                Debug.WriteLine("Storage: Access denied");
            }
            catch (ArgumentNullException)
            {
                Debug.WriteLine("Storage: Folder picker abort");
            }
        }

        public void SendFileListToServer()
        {
            var list = SynchronizationFiles.Select(file => new FileInfoDataContract {Name = file.Name});
            App.Connection.Service.FileListToSendAsync(new ObservableCollection<FileInfoDataContract>(list));
        }

        public void SetDefaultFolder()
        {
            Path = DefaultPath;
            SetFolder(Path);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}