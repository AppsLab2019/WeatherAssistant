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



                // if (weatherData.Rain.ThreeHours == 0 && weatherData.Snow.ThreeHours == 0)
                if (weatherData.Rain == null && weatherData.Snow == null)
                {                                                   
                        if (weatherData.Main.FeelsLike >= 26)
                        {
                            FigureImage.Source = "panacik1.png";
                        }
                        if (weatherData.Main.FeelsLike >= 19)
                        {
                            FigureImage.Source = "panacik2.png";
                        }
                        if (weatherData.Main.FeelsLike >= 10)
                        {
                            FigureImage.Source = "panacik3.png";
                        }
                        if (weatherData.Main.FeelsLike >= 0)
                        {
                            FigureImage.Source = "panacik4.png";
                        }
                        if (weatherData.Main.FeelsLike <0)
                        {
                            FigureImage.Source = "panacik5.png";
                        }                                 
                }
                else
                {
                    if (weatherData.Main.FeelsLike >= 26)
                    {
                        FigureImage.Source = "panacik1+.png";
                    }
                    if (weatherData.Main.FeelsLike >= 19)
                    {
                        FigureImage.Source = "panacik2+.png";
                    }
                    if (weatherData.Main.FeelsLike >= 10)
                    {
                        FigureImage.Source = "panacik3+.png";
                    }
                    if (weatherData.Main.FeelsLike >= 0)
                    {
                        FigureImage.Source = "panacik4+.png";
                    }
                    if (weatherData.Main.FeelsLike < 0)
                    {
                        FigureImage.Source = "panacik5+.png";
                    }
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
