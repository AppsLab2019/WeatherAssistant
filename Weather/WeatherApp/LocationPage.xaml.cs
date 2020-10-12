using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : ContentPage
    {
        public LocationPage()
        {
            InitializeComponent();
        }

        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            var mainPage = MainPage.Instance;
            mainPage.LocationText = _cityEntry.Text;
            await mainPage.RefreshWeatherData();
            await Shell.Current.Navigation.PopAsync();
        }
    }
}