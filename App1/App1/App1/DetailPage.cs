using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Xamarin.Forms;

namespace App1
{
    public class DetailPage : ContentPage
    {
        public DetailPage(Obj obj)
        {
            Label label = new Label();
            label.HorizontalOptions = LayoutOptions.Center;
            label.FontSize = 20;
            string x = obj.pin.Label;
            label.HorizontalTextAlignment = TextAlignment.Center;
            label.VerticalTextAlignment = TextAlignment.Center;
            x = x.Replace("\n\n", "\n\n Архитектор/студия:\n ");
            label.Text = "\n\nНазвание здания:\n " +x + "\n\n" + "Ссылка на статью:\n " + obj.s;
            string img = $"https://i.archi.ru/i/{obj.image}";
            Image image = new Image();
            image.Source = new UriImageSource
            {
                CachingEnabled = false,
                Uri = new System.Uri(img)
            };

            Content = new StackLayout
            {
                Children = {
                    label, image
                }
            }; 
        }
    }
}