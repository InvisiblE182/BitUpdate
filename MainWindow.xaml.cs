using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;


namespace BitUpdate
{
    public class UnformatedBitDetail
    {
        public Time time { get; set; }
        public string disclaimer { get; set; }
        public string chartName { get; set; }
        public Bpi bpi { get; set; }
    }

    public partial  class MainWindow : Window
    {
        

        public MainWindow()
        {
            Task.Run(async () =>
            {
                UnformatedBitDetail ubd = new UnformatedBitDetail();
                ubd = await GetBitAsync();
                string BitPrice = ubd.bpi.EUR.rate;
                double Bitcoins = 0.0003;
                double Portfolio = Convert.ToDouble(BitPrice) * Bitcoins;
                double Investment = 270;
                double Balance = Portfolio - Investment;
                tb.Text = "Current price of bitcoin is " + BitPrice;
                /*
                Console.WriteLine("Current price of bitcoin is {0}", BitPrice);
                Console.WriteLine("Your portfolio is {0}", Portfolio);
                Console.WriteLine("Your balance is {0} EUR {1} CZK", Balance, Balance * 27);*/
            }).GetAwaiter().GetResult();


            
            
            InitializeComponent();
        }

        public static async Task<UnformatedBitDetail> GetBitAsync()
        {

            try
            {
                var http = new HttpClient();
                var response = await http.GetAsync("http://api.coindesk.com/v1/bpi/currentprice.json");
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<UnformatedBitDetail>(result);
                return data;
            }
            catch (Exception e)
            {
                // Throw an HttpException with customized message.
                throw new Exception("No connection");
            }

        }
    }
}

