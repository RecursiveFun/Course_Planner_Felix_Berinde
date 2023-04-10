using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_Felix_Berinde.Models;
using Course_Planner_Felix_Berinde.Services;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_Planner_Felix_Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        async void AddGadget_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GadgetAdd());
        }

        async void ViewGadgets_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GadgetList());
        }

        async void Settings_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppSettings());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var widgetList = await DatabaseService.GetWidgets();
            var notifyRandom = new Random();
            var notidyId = notifyRandom.Next(1000);

            foreach (Widget widgetRecord in widgetList)
            {
                if (widgetRecord.StartNotification == true)
                {
                    if (widgetRecord.CreationDate == DateTime.Today)
                    {
                        CrossLocalNotifications.Current.Show("Notice", $"{widgetRecord.Name} begins today!", notidyId);
                    }
                }
            }
        }

    }
}