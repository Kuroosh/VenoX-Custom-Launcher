using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace VenoX_V_Production_Launcher
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {

            InitializeComponent();
            MouseDown += Window_MouseDown;
            string directory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string filePath = System.IO.Path.Combine(directory, "Reallife.rar");

            if (!File.Exists(filePath))
            {
                Update_Label.Content = "Update Verfügbar! | VenoX - V.1.0.5 | ";
                Starten.Content = "Update Herunterladen";
            }
        }


        // Window Moveable machen 
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            string directory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string rarpath = System.IO.Path.Combine(directory, "Reallife.rar");



            /*RarArchive archive = RarArchive.Open("test.rar");
            foreach (RarArchiveEntry entry in archive.Entries)
            {
                try
                {
                    string fileName = Path.GetFileName(entry.FilePath);
                    string rootToFile = Path.GetFullPath(entry.FilePath).Replace(fileName, "");

                    if (!Directory.Exists(rootToFile))
                    {
                        Directory.CreateDirectory(rootToFile);
                    }

                    entry.WriteToFile(rootToFile + fileName, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                }
                catch (Exception ex)
                {
                    //handle your exception here..
                }
            }*/


            Starten.Content = "Spiel Starten";
            downloading = false;
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // In case you don't have a progressBar Log the value instead 
            // Console.WriteLine(e.ProgressPercentage);
            progressBar1.Value = e.ProgressPercentage;
            Update_Label.Content = "Downloading VenoX - V.1.0.5 | " + e.ProgressPercentage + " %";
        }



        bool downloading = false;
        //Start button Click
        private void Starten_Click(object sender, RoutedEventArgs e)
        {
            /*
            var process = Process.Start(new ProcessStartInfo(@"C:\RAGEMP\updater.exe")
            {
                Verb = "runAs",
                UseShellExecute = true,
                
            });
            Close();*/

            string directory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string filePathUpdate = System.IO.Path.Combine(directory, "Reallife.rar");
            string filePathRageMp = System.IO.Path.Combine(directory, "updater.exe");

            if (!File.Exists(filePathUpdate))
            {
                if (!downloading)
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                    webClient.DownloadFileAsync(new Uri("https://www.venox-reallife.com/Reallife.rar"), "Reallife.rar");
                    Starten.Content = "Bitte Warten...";
                    downloading = true;
                }
            }
            else
            {
                if (File.Exists(filePathRageMp))
                {
                    var process = Process.Start(new ProcessStartInfo(filePathRageMp)
                    {
                        Verb = "runAs",
                        UseShellExecute = true,

                    });
                    Close();
                }
            }
        }



        //ts3 Click
        private void Teamspeak3_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("ts3server://ts3.venox-reallife.com?port=9987");
        }

        private void Forum_Click(object sender, RoutedEventArgs e)
        {

        }



        //Progressbar für Updates
        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        //Close
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
