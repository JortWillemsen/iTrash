using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iTrash
{
    public partial class App : Application
    {
        ContainerAgent containerAgent = new ContainerAgent();
        public App()
        {
            InitializeComponent();
            Application.Current.Properties["ContainerList"] = containerAgent.ContainerList;
            Application.Current.Properties["HasConnection"] = containerAgent.HasConnection;
            Application.Current.Properties["HostIP"] = containerAgent.HostIp;
            MainPage = new MainPage();
            
    }
        
        protected override void OnStart()
        {
            
            containerAgent.InitiateDataUpdater();
            
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
