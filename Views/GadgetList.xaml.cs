using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Course_Planner_Felix_Berinde.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Course_Planner_Felix_Berinde.Services;

namespace Course_Planner_Felix_Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GadgetList : ContentPage
    {
        public GadgetList()
        {
            InitializeComponent();
        }

        private async void GadgetCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Gadget gadget = (Gadget)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new GadgetEdit(gadget));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GadgetCollectionView.ItemsSource = await DatabaseService.GetGadgets();
        }

    }
}