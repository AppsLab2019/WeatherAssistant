using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public string LocationText { get; set; }
        public static MainPage Instance { get; private set; }

        readonly RestService _restService;
        private bool _hasAppeared = false;

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
            Instance = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_hasAppeared)
                return;

            _hasAppeared = true;
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location is null)
                return;

            var weatherData = await _restService.GetWeatherData(GenerateLocationRequestUri(Constants.OpenWeatherMapEndpoint, location));
            BindingContext = weatherData;
            RefreshFigure(weatherData);
        }

        void OnPositionButtonClicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new LocationPage());
        }

        public async Task RefreshWeatherData()
        {
            if (!string.IsNullOrWhiteSpace(LocationText))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;
                RefreshFigure(weatherData);
            }
        }

        private void RefreshFigure(WeatherData weatherData)
        {
            if (weatherData == null)
                return;

            string prefix, state;

            if (weatherData.Main.FeelsLike >= 26)
            {
                prefix = "vh";
            }
            else if (weatherData.Main.FeelsLike >= 19)
            {
                prefix = "h";
            }
            else if (weatherData.Main.FeelsLike >= 10)
            {
                prefix = "n";
            }
            else if (weatherData.Main.FeelsLike >= 0)
            {
                prefix = "c";
            }
            else
            {
                prefix = "vc";
            }

            if (weatherData.Snow != null && weatherData.Snow.OneHour > 0.01d)
            {
                state = "snowy";
            }
            else if (weatherData.Rain != null && weatherData.Rain.OneHour > 0.01d)
            {
                state = "rainy";
            }
            else if (weatherData.Clouds.All > 80)
            {
                state = "veryCloudy";
            } 
            else if (weatherData.Clouds.All > 10)
            {
                state = "cloudy";
            }
            else
            {
                state = "sunny";
            }

            var image = $"{prefix}_{state}.png";
            FigureImage.Source = image;
        }

        private string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={LocationText}";
            requestUri += "&units=metric";
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            requestUri += "&lang=sk";
            return requestUri;
        }

        private string GenerateLocationRequestUri(string endpoint, Location location)
        {
            string requestUri = endpoint;
            requestUri += $"?lat={location.Latitude}";
            requestUri += $"&lon={location.Longitude}";
            requestUri += "&units=metric";
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            requestUri += "&lang=sk";
            return requestUri;
        }
    }
}
