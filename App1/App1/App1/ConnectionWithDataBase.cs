using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;

namespace App1
{
    [JsonObject]
    public class Data
    {
        [JsonProperty("data")]
        public string[][] data { get; set; }
        [JsonProperty("count")]
        public double count { get; set; }
        [JsonProperty("totalCount")]
        public double totalCount { get; set; }
        /*
        public string[] AuthorAndName(string s)
        {
            return s.Split("\n\n");
        }
        */
        public List<ConnectionWithDataBase> Parse()
        {
            List<ConnectionWithDataBase> connect = new List<ConnectionWithDataBase>();
            foreach (string[] s in this.data)
            {
                ConnectionWithDataBase buf = new ConnectionWithDataBase(int.Parse(s[2]), double.Parse(s[0], CultureInfo.InvariantCulture), double.Parse(s[1], CultureInfo.InvariantCulture), s[5], s[3], s[4]);
                connect.Add(buf);
            }
            return connect;
        }
    }

    public class ConnectionWithDataBase
    {
        public string image { get; set; }
        public string type { get; set; }
        public int index { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public string name { get; set; }
        //public string author { get; set; }
        public ConnectionWithDataBase(int i, double X, double Y, string Name, string Type, string img/*, string auth*/)
        {
            index = i;
            x = X;
            y = Y;
            name = Name;
            type = Type;
            image = img;
            // author = auth;
        }
    }
}
