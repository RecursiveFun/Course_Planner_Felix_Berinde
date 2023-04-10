using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_Felix_Berinde.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_Planner_Felix_Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GadgetAdd : ContentPage
    {
        public GadgetAdd()
        {
            InitializeComponent();
        }

        async void SaveGadget_OnClicked(object sender, EventArgs e)
        {
            decimal tossedDecimal;
            int tossedInt;

            if (string.IsNullOrWhiteSpace(GadgetName.Text))
            {
                await DisplayAlert("Missing Name", "Please enter a name.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(GadgetColorPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Missing Color", "Please enter a color.", "OK");
                return;
            }

            if (!Int32.TryParse(GadgetsInStock.Text, out tossedInt))
            {
                await DisplayAlert("Incorrect Inventory Value", "Please enter a whole number.", "OK");
                return;
            }

            if (!Decimal.TryParse(GadgetPrice.Text, out tossedDecimal))
            {
                await DisplayAlert("Incorrect Price Value", "Please enter a number.", "OK");
            }

            await DatabaseService.AddGadget(GadgetName.Text,
                GadgetColorPicker.SelectedItem.ToString(), Int32.Parse(GadgetsInStock.Text),
                Decimal.Parse(GadgetPrice.Text),CreationDatePicker.Date);
            await Navigation.PopAsync();
        }

        async void CancelGadget_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}