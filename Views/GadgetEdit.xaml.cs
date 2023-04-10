using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_Felix_Berinde.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Course_Planner_Felix_Berinde.Models;

namespace Course_Planner_Felix_Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GadgetEdit : ContentPage
    {
        public GadgetEdit()
        {
            InitializeComponent();
        }

        public GadgetEdit(Gadget gadget)
        {
            InitializeComponent();

            _selectedGadgetId = gadget.Id;

            GadgetId.Text = gadget.Id.ToString();
            GadgetName.Text = gadget.Name;
            GadgetColorPicker.SelectedItem = gadget.Color;
            GadgetsInStock.Text = gadget.InStock.ToString();
            GadgetPrice.Text = gadget.Price.ToString();
            CreationDatePicker.Date = gadget.CreationDate;
        }

        private readonly int _selectedGadgetId;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //TODO Return count from a table async
            //Note that the await unwraps the Task<T> to a T value (T may be a string, int ext. In this case an int)
            //See discussion at the link below by Jon Skeet.
            //https://stackoverflow.com/questions/13159080/how-does-taskint-become-an-int
            //https://stackoverflow.com/a/13159176

            int countWidgets = await DatabaseService.GetWidgetCountAsync(_selectedGadgetId);
            CountLabel.Text = countWidgets.ToString();
            WidgetCollectionView.ItemsSource = await DatabaseService.GetWidgets(_selectedGadgetId); //retrieve widgets for a specific Gadget based on the GadgetID
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

            await DatabaseService.UpdateGadget(Int32.Parse(GadgetId.Text), GadgetName.Text,
                GadgetColorPicker.SelectedItem.ToString(), Int32.Parse(GadgetsInStock.Text),
                Decimal.Parse(GadgetPrice.Text), DateTime.Parse(CreationDatePicker.Date.ToString()));
            await Navigation.PopAsync();
        }

        async void CancelGadget_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Delete_OnClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Gadget and Related Widgets?", "Delete this Gadget?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(GadgetId.Text);

                await DatabaseService.RemoveGadget(id);

                await DisplayAlert("Gadget Deleted", "Gadget Deleted", "OK");
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        async void AddWidget_OnClicked(object sender, EventArgs e)
        {
            var gadgetId = Int32.Parse(GadgetId.Text);

            await Navigation.PushAsync(new WidgetAdd(gadgetId));
        }

        async void WidgetCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var widget = (Widget)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync((new WidgetEdit(widget)));
            }
        }
    }
}