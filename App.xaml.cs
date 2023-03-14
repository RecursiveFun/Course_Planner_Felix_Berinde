using System;
using Course_Planner_Felix_Berinde.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_Planner_Felix_Berinde
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var dashBoard = new Dashboard();
            var navPage = new NavigationPage(dashBoard);
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
