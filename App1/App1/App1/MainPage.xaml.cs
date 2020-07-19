using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


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
            var map = new Map(MapSpan.FromCenterAndRadius(new Position(37, -122), Distance.FromMiles(10)));
            var pin = new Pin()
            {
                Position = new Position(37, -122),
                Label = "Some Pin!"
            };
            map.Pins.Add(pin);
            Content = map;
        }
            
        
    }

}
