using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GetData
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public string URL = "https://archi.ru/map/data";
        public MainPage()
        {
            InitializeComponent();
            GetData();
        }

        async void GetData()
        {
            using (var client = new HttpClient())
            {
                try
                {

                    var responce = await client.GetAsync(URL);

                    if (responce.IsSuccessStatusCode)
                    {
                        var payload = JsonConvert.SerializeObject(responce);
                        var content = new StringContent(payload, Encoding.UTF8, @"application/json");
                        var data = await content.ReadAsStringAsync();
                        testL.Text = data;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                catch
                {
                    await DisplayAlert("Error!", "err", "OK!");
                }
            }
        }
    }
}
