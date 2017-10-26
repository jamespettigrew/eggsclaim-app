using System;

using Xamarin.Forms;

namespace Eggsclaim
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<LogDataStore>();
            MainPage = new NavigationPage(new LogPage());
        }
    }
}
