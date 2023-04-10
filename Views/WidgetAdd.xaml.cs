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
    public partial class WidgetAdd : ContentPage
    {
        private readonly int _selectedWidgetId;
        public WidgetAdd()
        {
            InitializeComponent();
        }

        public WidgetAdd(int WidgetId)
        {
            _selectedWidgetId = WidgetId;
        }

        async void SaveWidget_OnClicked(object sender, EventArgs e)
        {
            decimal tossedDecimal;
            int tossedInt;

            if (string.IsNullOrWhiteSpace(WidgetName.Text))
            {
                await DisplayAlert("Missing Name", "Please enter a name.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(WidgetColorPicker.SelectedItem.ToString()))
            {
                await DisplayAlert("Missing Color", "Please enter a color.", "OK");
                return;
            }

            if (!Int32.TryParse(WidgetsInStock.Text, out tossedInt))
            {
                await DisplayAlert("Incorrect Inventory Value", "Please enter a whole number.", "OK");
                return;
            }

            if (!Decimal.TryParse(WidgetPrice.Text, out tossedDecimal))
            {
                await DisplayAlert("Incorrect Price Value", "Please enter a number.", "OK");
            }

            await DatabaseService.AddWidget(_selectedWidgetId, WidgetName.Text,
                WidgetColorPicker.SelectedItem.ToString(), Int32.Parse(WidgetsInStock.Text),
                Decimal.Parse(WidgetPrice.Text), CreationDatePicker.Date, Notification.IsToggled, NotesEditor.Text);
            await Navigation.PopAsync();
        }

        async void CancelWidget_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Home_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}