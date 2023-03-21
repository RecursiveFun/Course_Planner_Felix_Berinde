using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}