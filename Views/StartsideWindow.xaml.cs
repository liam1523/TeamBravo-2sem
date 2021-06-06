using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TeamBravo___2.Semester___Eksamensopgave
{

    public partial class StartsideWindow : Window //Liam
    {
        //Liam
        private int VirkID;
        private string Brugernavn;

        private Thread thread;

        //Liam
        public StartsideWindow(string username, int virksomhedid)
        {
            InitializeComponent();

            IDlabel.Content = virksomhedid;
            VirkID = virksomhedid;
            Brugernavn = username;
            velkLabel.Content = string.Format("Velkommen, {0}!", Brugernavn);

            if (!Directory.Exists("C:\\Dropzone"))
            {
                Directory.CreateDirectory("C:\\Dropzone");
            }

            FileReporter fileReporter = new FileReporter();
            FileTracker fileTracker = new FileTracker();
            fileReporter.Subscribe(fileTracker);

            FileSystemWatcher watcher = new FileSystemWatcher();
            try
            {
                watcher.Path = "C:\\Dropzone";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            watcher.Filter = "*.csv";

            watcher.Created += (s, e) =>
            {
                MessageBoxResult result = Dispatcher.Invoke(() => MessageBox.Show("Ny CSV fil tilføjet til mappe! " +
                    "Vil du åbne den nu?", "Meddelse", MessageBoxButton.YesNo, MessageBoxImage.Information));
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
                        fileReporter.affaldsposts.Clear();

                        fileTracker.TrackFile(e.FullPath);

                        if (fileReporter.affaldsposts.Count != 0)
                        {
                            Thread thread = new Thread(() => OpenFile(fileReporter.affaldsposts));
                            thread.IsBackground = true;
                            thread.SetApartmentState(ApartmentState.STA);
                            thread.Start();

                        }
                        else
                        {
                            MessageBox.Show("Filen er i et forkert format!");

                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Kunne ikke spore filen!");

                    }


                }

            };

            watcher.EnableRaisingEvents = true;

        }

        //Liam
        private void OpenFile(List<Affaldspost> affaldsposts)
        {
            NyFilWindow nyFilWindow = new NyFilWindow(affaldsposts, Brugernavn) ;
            nyFilWindow.ShowDialog();
            System.Windows.Threading.Dispatcher.Run();

        }

        //Liam
        private void OpenVirksomheder()
        {
            VirkWindow virkWindow = new VirkWindow(Brugernavn);
            virkWindow.ShowDialog();
            thread = null;
            System.Windows.Threading.Dispatcher.Run();

        }

        //Liam
        private void AffaldBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            AffaldWindow afw = new AffaldWindow(Brugernavn, VirkID);
            afw.ShowDialog();
            this.Visibility = Visibility.Visible;

        }

        //Liam
        private void StatistikBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            StatistikWindow stw = new StatistikWindow();
            stw.ShowDialog();
            this.Visibility = Visibility.Visible;

        }

        //Liam
        private void VirksomhederBtn_Click(object sender, RoutedEventArgs e)
        {
            if (thread == null)
            {
                ThreadStart ts = new ThreadStart(() => OpenVirksomheder());
                thread = new Thread(ts);
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }

        }

        //Liam
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }

        //Liam
        private void CloseKnap_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Vil du logge af nu?", "Information", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();

            }

        }

    }

}
