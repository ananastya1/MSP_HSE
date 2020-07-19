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
            var map = new Map(MapSpan.FromCenterAndRadius(new Position(55.755787, 37.617634), Distance.FromMiles(10))); //можно задать местоположение человека, допилим потом
            int numberOfBuidings = 1;
            Content = map;
            double x = 55.702845;
            double y = 37.530651;
            string name = "Moscow State University";
            for (int i = 0; i < numberOfBuidings; i++)
            {
                var pin = new Pin()
                {
                    //x = - здесь ConnectionWithDataBase должен передать x, y и название места
                    //y = 
                    //name = 
                    Position = new Position(x, y),
                    Label = name 
                };
                //map.IsShowingUser = true;       
                map.Pins.Add(pin);
            }
        }
    }

}
