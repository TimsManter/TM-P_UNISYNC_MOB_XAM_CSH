using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniSyncClient.Model;
using UniSyncClient.View;
using Xamarin.Forms;

namespace UniSyncClient
{
    public partial class App : Application
    {
        public static Connection Connection { get; } = new Connection();
        public static Storage Storage { get; } = new Storage();

        public App()
        {
            InitializeComponent();

            MainPage = new ContainerPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
