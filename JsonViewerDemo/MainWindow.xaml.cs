using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

namespace JsonViewerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Switch between the two modes of loading the JSON

            try
            {
                var client = new WebClient {Proxy = null};
                client.DownloadStringCompleted += delegate(object sender, DownloadStringCompletedEventArgs args)
                {
                    JsonViewer.Load(args.Result);
                    TextBlock.Text = "Loading finished";
                };
                TextBlock.Text = "Loading...";

                // Choose 1
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/posts"));
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/comments"));
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/albums"));
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/photos"));
                //client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/todos"));
                client.DownloadStringAsync(new Uri("http://jsonplaceholder.typicode.com/users"));
                //client.DownloadStringAsync(new Uri("https://mocki.io/v1/fcd7c888-3300-48fe-87bc-c5cce6955690"));

                //Write To a file
                var responseData = "{\"one\": \"two\",\"key\": \"value\"}";
                string jsonData = JsonConvert.SerializeObject(responseData, Formatting.Indented);
                System.IO.File.WriteAllText(@".\MyJsonData.json", jsonData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                const string json = "{\"one\": \"two\",\"key\": \"value\"}";
                JsonViewer.Load(json);
            }
        }
    }
}
