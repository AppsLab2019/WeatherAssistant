using System;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        readonly RestService _restService;

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // 
        }

        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;

                

                // if (weatherData.Rain.OneHour == 0 && weatherData.Snow.OneHour == 0)
                if (weatherData.Rain == null && weatherData.Snow == null)
                {
                    if (weatherData.Clouds.All <= 40)
                    {
                        // 1. moznost
                        if (weatherData.Main.FeelsLike > 30)
                        {
                            FigureImage.Source = "panacik1.png";
                        }
                        if (weatherData.Main.FeelsLike > 20)
                        {
                            FigureImage.Source = "panacik2.png";
                        }
                        // ...
                    }
                    else
                    {
                        // 2. moznost
                    }
                }
                else
                {
                    // 3. moznost
                }
            }
        }

        private string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=metric"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            requestUri += "&lang=sk";
            return requestUri;
        }
    }
}
