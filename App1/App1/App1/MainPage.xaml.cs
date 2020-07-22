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
    
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {   
        public MainPage()
        {
            InitializeComponent();
            //var res = await GeoAsync();
            var map = new Xamarin.Forms.Maps.Map(MapSpan.FromCenterAndRadius(new Position(55.755787, 37.617634), Distance.FromMiles(1))); //можно задать местоположение человека, допилим потом
            
            Content = map;
            //string jsonString = File.ReadAllText(@"D:\1\data.json");
            string jsonString = FileReader();
            string s = jsonString;
            var data = JsonConvert.DeserializeObject<Data>(jsonString);
            List<ConnectionWithDataBase> connection = data.Parse();
            double x = 55.702845;
            double y = 37.530651;
            string name = "Moscow State University";
            int numberOfBuidings = (int)data.count;
            foreach (ConnectionWithDataBase db in connection)
            {
                x = db.x;
                y = db.y;
                name = db.name;
                var pin = new Pin()
                {
                    Position = new Position(x, y),
                    Label = name 
                };
                map.IsShowingUser = true;       
                map.Pins.Add(pin);
            }
        }
        public string FileReader()
        {
            string jsonName = "Connection.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonName}");
            var reader = new System.IO.StreamReader(stream);
            var jsonString = reader.ReadToEnd();
            //Converting JSON Array Objects into generic list    
            //ObjContactList = JsonConvert.DeserializeObject<ContactList>(jsonString);
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
