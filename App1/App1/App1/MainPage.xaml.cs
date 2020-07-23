using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Newtonsoft.Json;
using System.Reflection;
using Xamarin.Forms.Shapes;

namespace App1
{
    public class Obj
    {
        public Pin pin { get; set; }
        public string s { get; set; }

        public string image { get; set; }
        public Obj()
        {

        }
        public Obj(string S, Pin Pin, string img)
        {
            pin = Pin;
            s = S;
            image = img;
        }
    }
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public event EventHandler GetGeo;
        private double[] coordinate = new double[2];
        private List<Pin> pins = new List<Pin>();
        public MainPage()
        {
            GetGeo += MainPage_GetGeo;
            GetGeo?.Invoke(this, EventArgs.Empty);
            InitializeComponent();
            // var res = await GeoAsync();
            var map = new Xamarin.Forms.Maps.Map(MapSpan.FromCenterAndRadius(new Position(coordinate[0], coordinate[1]), Distance.FromMiles(1))); //можно задать местоположение человека, допилим потом
            Content = map;
            string jsonString = FileReader();
            var data = JsonConvert.DeserializeObject<Data>(jsonString);
            List<ConnectionWithDataBase> connection = data.Parse();
            double x = 55.702845;
            double y = 37.530651;
            string name = "Moscow State University";
            int numberOfBuidings = (int)data.count;
            map.IsShowingUser = true;
            foreach (ConnectionWithDataBase db in connection)
            {
                x = db.x;
                y = db.y;
                name = db.name;
                var pin = new Pin()
                {
                    Position = new Position(x, y),
                    Label = name,
                };
                pins.Add(pin);
                pin.MarkerClicked += Pin_MarkerClicked;
                map.Pins.Add(pin);
            }
        }
        public ConnectionWithDataBase Search(Pin pin)
        {
            string jsonString = FileReader(); 
            var data = JsonConvert.DeserializeObject<Data>(jsonString);
            List<ConnectionWithDataBase> connection = data.Parse();
            Position position = pin.Position;
            ConnectionWithDataBase result = new ConnectionWithDataBase();
            foreach (ConnectionWithDataBase db in connection)
            {
                if (db.x == position.Latitude)
                {
                    result = db;
                    break;
                }
            }
            return result;
        }

        private async void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            Pin buf = sender as Pin;
            ConnectionWithDataBase db = Search(buf);
            string s = $"https://archi.ru/projects/world/{db.index}";
            Obj obj1 = new Obj(s, buf, db.image);
            DetailPage detailPage = new DetailPage(obj1);
            //detailPage.BindingContext = obj1;
            await Navigation.PushModalAsync(detailPage);
        }

        private async void MainPage_GetGeo(object sender, EventArgs e)
        {
            coordinate = await GeoAsync();
        }

        public string FileReader()
        {
            string jsonName = "Connection.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonName}");
            var reader = new System.IO.StreamReader(stream);
            var jsonString = reader.ReadToEnd();
            return jsonString;
        }
        public async Task<double[]> GeoAsync()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            double[] res = new double[2];
            if (location != null)
            {
                res[0] = location.Latitude;
                res[1] = location.Longitude;
            }
            return res;
        }
    }

}
