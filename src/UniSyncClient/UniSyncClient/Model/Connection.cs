using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using UniSyncClient.Annotations;
using UniSyncClient.UniSyncService;
using Xamarin.Forms;

namespace UniSyncClient.Model
{
    public sealed class Connection : INotifyPropertyChanged
    {
        #region Fields

        private const string Address = "http://localhost/unisync";
        private static readonly BasicHttpBinding Binding = new BasicHttpBinding {TransferMode = TransferMode.Streamed};
        private static readonly EndpointAddress Endpoint = new EndpointAddress(Address);

        #endregion

        #region Properties

        public readonly UniSyncServiceClient Service = new UniSyncServiceClient(Binding, Endpoint);

        public CommunicationState State => Service.State;

        #endregion

        #region Constructor
        public Connection()
        {
            
            Service.OpenCompleted += (sender, args) =>
            {
                Service.InnerChannel.Faulted += (s, a) =>
                {
                    OnPropertyChanged(nameof(State));
                    DebugService("Service faulted");
                };

                Initialize();

                OnPropertyChanged(nameof(State));
                DebugService("Service opened");
            };
            Service.CloseCompleted += (sender, args) =>
            {
                OnPropertyChanged(nameof(State));
                DebugService("Service closed");
            };
            Service.InitializeCompleted += (sender, args) =>
            {
                OnPropertyChanged(nameof(State));
                DebugService("Initialize completed");
            };
            Service.FileListToSendCompleted += (sender, args) =>
            {
                DebugService("List from server: " + args.Result.Count.ToString());
            };
        }
        #endregion

        #region Methods
        public bool Connect()
        {
            try
            {
                Service.OpenAsync();
                OnPropertyChanged(nameof(State));
                DebugService("Opening service");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                DebugService(e.Message);
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                Service.CloseAsync();
                OnPropertyChanged(nameof(State));
                DebugService("Closing service");
                return true;
            }
            catch (Exception e)
            {
                DebugService(e.Message);
                return false;
            }
        }

        private void Initialize()
        {
             Service.InitializeAsync();
        }

        #endregion

        #region Debug
        private static void DebugService(string message)
        {
            Device.BeginInvokeOnMainThread(() => Debug.WriteLine("Service: " + message));
        } 
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}