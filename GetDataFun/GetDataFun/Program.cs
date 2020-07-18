using System;
using System.IO;
using System.Text.Json;

namespace GetDataFun
{
    public class Data
    {
        public string[][] d { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var jsonString = File.ReadAllText(@"D:\XamarinProjects\HSE msp\GetData\GetData\GetData\data.json");
            Data data = new Data();
            data = JsonSerializer.Deserialize<Data>(jsonString);

            Console.WriteLine("check");
            Console.WriteLine(data.d[0][0]);
            Console.WriteLine("check");
        }
    }
}
