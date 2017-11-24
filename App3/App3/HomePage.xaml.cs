using Java.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        
        public HomePage()
        {
            InitializeComponent();

            
            this.Loadrates();

            convertButton.Clicked += convertButton_Clicked;

        }

        private async void convertButton_Clicked(object sender, EventArgs e)
        {
            waitActivityIndicator.IsRunning = true;
            string url = "https://api.coinbase.com/v2/prices/spot?currency=USD";
            JsonValue json = await FetchWeatherAsync(url);
            ParseAndDisplay(json);
            waitActivityIndicator.IsRunning = false;
        }

                

        private async void Loadrates()
        {
            waitActivityIndicator.IsRunning = true;
            string url = "https://api.coinbase.com/v2/prices/spot?currency=USD";
            JsonValue json = await FetchWeatherAsync(url);
            ParseAndDisplay(json);
            waitActivityIndicator.IsRunning = false;
        }

        private async Task<JsonValue> FetchWeatherAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    //Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }

        private void ParseAndDisplay(JsonValue json)
        {
            
            //TextView temperature = FindViewById<TextView>(Resource.Id.tempText);
            
            JsonValue weatherResults = json["data"];

            
            double temp = weatherResults["amount"];

            priceLabel.Text = String.Format("{0:C2}", temp);
            
        }
    }
}