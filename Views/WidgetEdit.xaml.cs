using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_Felix_Berinde.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Course_Planner_Felix_Berinde.Services;
using Xamarin.Essentials;

namespace Course_Planner_Felix_Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WidgetEdit : ContentPage
    {
        public WidgetEdit()
        {
            InitializeComponent();
        }

        public WidgetEdit(Widget selectedWidget)
        {
            InitializeComponent();

            //Populate controls next
            WidgetId.Text = selectedWidget.Id.ToString();
            WidgetName.Text = selectedWidget.Name;
            WidgetColorPicker.SelectedItem = selectedWidget.Color;
            WidgetsInStock.Text = selectedWidget.InStock.ToString();
            WidgetPrice.Text = selectedWidget.Price.ToString();
            NotesEditor.Text = selectedWidget.Notes;
            CreationDatePicker.Date = selectedWidget.CreationDate;
            Notification.IsToggled = selectedWidget.StartNotification;
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

            await DatabaseService.UpdateWidget(Int32.Parse(WidgetId.Text), WidgetName.Text,
                WidgetColorPicker.SelectedItem.ToString(), Int32.Parse(WidgetsInStock.Text),
                Decimal.Parse(WidgetPrice.Text), CreationDatePicker.Date, Notification.IsToggled, NotesEditor.Text);
            await Navigation.PopAsync();
        }

        async void CancelWidget_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void DeleteWidget_OnClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete this Widget?", "Delete this Widget?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(WidgetId.Text);

                await DatabaseService.RemoveWidget(id);

                await DisplayAlert("Widget Deleted", "Widget Deleted", "OK");
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        async void ShareButton_OnClicked(object sender, EventArgs e)
        {
            var text = WidgetName.Text;

            await Share.RequestAsync(new ShareTextRequest()
            {
                Text = text,
                Title = "Share Text"
            });
        }

        async void ShareURI_OnClicked(object sender, EventArgs e)
        {
            string uri = "https://docs.microsoft.com/en-us/xamarin/essentials/share?tabs=android";
            await Share.RequestAsync(new ShareTextRequest()
            {
                Uri = uri,
                Title = "Share Web Link"
            });
        }
    }
}