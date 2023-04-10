using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_Felix_Berinde.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_Planner_Felix_Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppSettings : ContentPage
    {
        public AppSettings()
        {
            InitializeComponent();
        }

        private void ClearPreferences_OnClicked(object sender, EventArgs e)
        {
            Preferences.Clear();
        }

        async void LoadSampleData_OnClicked(object sender, EventArgs e)
        {
            if (Settings.FirstRun)
            {
                DatabaseService.LoadSampleData();
                Settings.FirstRun = false;

                await Navigation.PopToRootAsync();
            }
        }

        async void ClearSampleData_OnClicked(object sender, EventArgs e)
        {
            await DatabaseService.ClearSampleData();
        }
    }
}