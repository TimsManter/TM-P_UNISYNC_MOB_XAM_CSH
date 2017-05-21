using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using Xamarin.Forms;
using UniSyncClient.Annotations;

namespace UniSyncClient.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Commands
        public Command Connect { get; } = new Command(
            () => App.Connection.Connect(),
            () => App.Connection.State == CommunicationState.Created || App.Connection.State == CommunicationState.Faulted);

        public Command Disconnect { get; } = new Command(
            () => App.Connection.Disconnect(),
            () => App.Connection.State == CommunicationState.Opened);
        #endregion

        public MainViewModel()
        {

            App.Connection.PropertyChanged += (sender, args) => Device.BeginInvokeOnMainThread(() =>
            {
                Connect.ChangeCanExecute();
                Disconnect.ChangeCanExecute();
                Debug.WriteLine(App.Connection.State);
            });
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