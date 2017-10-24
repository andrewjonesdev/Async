using System;
using System.Collections.Generic;
using System.IO;
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

namespace Async
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DownloadHtml("http://msdn.microsoft.com");
            var html = GetHtml("http://msdn.microsoft.com");
            MessageBox.Show(html.Substring(0, 10));
        }

        private async void Button_Click_Top(object sender, RoutedEventArgs e) //await keyword can only be used in async methods
        {
            DownloadHtmlAsync("http://msdn.microsoft.com");
            var html = await GetHtmlAsync("http://msdn.microsoft.com");
            MessageBox.Show(html.Substring(0, 10));
        }

        private async void Button_Click_Top_Alt(object sender, RoutedEventArgs e) //await keyword can only be used in async methods
        {
            DownloadHtmlAsync("http://msdn.microsoft.com");
            var getHtmlTask = GetHtmlAsync("http://msdn.microsoft.com");
            MessageBox.Show("Waiting for the task to complete.");//Can get things done with the thread before await to use it to its fullest potential, use await when the rest can't be completed until a process is ready
            var html = await getHtmlTask;
            MessageBox.Show(html.Substring(0, 10));
        }

        public async Task<string> GetHtmlAsync(string url)
        {
            var webClient = new WebClient();
            return await webClient.DownloadStringTaskAsync(url);
        }

        public string GetHtml(string url)
        {
            var webClient = new WebClient();
            return webClient.DownloadString(url);
        }


        public async Task DownloadHtmlAsync(string url)
        {
            var webClient = new WebClient();
            var html = await webClient.DownloadStringTaskAsync(url); //Used with Async, need to include await

            using (var streamWriter = new StreamWriter(@"c:\Users\AndrewCanDoAll\source\repos\Async\Async\result.html"))
            {
                await streamWriter.WriteAsync(html);
            }
        }

        public void DownloadHtml(string url)
        {
            var webClient = new WebClient();
            var html = webClient.DownloadString(url);

            using (var streamWriter = new StreamWriter(@"c:\Users\AndrewCanDoAll\source\repos\Async\Async\result.html"))
            {
                streamWriter.Write(html);
            }
        }

        
    }
}
