using LiveCharts;
using LiveCharts.Wpf;
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

    public partial class VirkWindow : Window
    {
        //Liam
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private List<Affaldspost> data = new List<Affaldspost>();
        private SqlRepository sql = new SqlRepository();

        private string Brugernavn;

        //Liam
        public VirkWindow(string brugernavn)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            InitializeComponent();
            DataContext = this;
            Brugernavn = brugernavn;
            SeriesCollection = new SeriesCollection();

            var files = Directory.GetFiles("C:\\Dropzone");
            foreach (var item in files)
            {
                string file = System.IO.Path.GetFileName(item);
                listResult.Items.Add(file);

            }

        }

        //Liam
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string filename = listResult.SelectedItem.ToString();
            double[] antalposter = new double[9];
            double[] AntalDbPoster = new double[9];
            List<Affaldspost> Affaldsposter = sql.GetAffaldsposts();
            string[] lines;

            string path = string.Format("C:\\Dropzone\\{0}", filename);
            try
            {
                lines = File.ReadAllLines(path);
                data.Clear();
                SeriesCollection.Clear();

                foreach (var item in lines)
                {
                    var split = item.Split(';');
                    Affaldspost affaldspost = new Affaldspost
                    {
                        ID = int.Parse(split[0]),
                        Maengde = decimal.Parse(split[1]),
                        Maaleenhed = int.Parse(split[2]),
                        Kategori = int.Parse(split[3]),
                        Beskrivelse = split[4],
                        Ansvarlig = split[5],
                        VirksomhedID = int.Parse(split[6]),
                        Dato = DateTime.Parse(split[7])

                    };

                    if (affaldspost.IsValid)
                    {
                        data.Add(affaldspost);

                    }

                }
                foreach (Affaldspost item in data)
                {
                    int kategori = item.Kategori;
                    int i = 1;
                    for (; i < 10; i++)
                    {
                        if (kategori == i)
                        {
                            antalposter[i - 1] += 1;
                            break;
                        }

                    }

                }
                foreach (Affaldspost item in Affaldsposter)
                {
                    int kategori = item.Kategori;
                    int i = 1;
                    for (; i < 10; i++)
                    {
                        if (kategori == i)
                        {
                            AntalDbPoster[i - 1] += 1;
                            break;
                        }

                    }

                }

                Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                Formatter = value => value.ToString("N");

                if (data.Count != 0)
                {
                    ImportKnap.IsEnabled = true;
                    ImportKnap.Foreground = Brushes.White;
                    FilLabel.Content = filename.Replace(".csv", "");

                    virksomhedGrid.ItemsSource = null;
                    virksomhedGrid.ItemsSource = data;

                    SeriesCollection.Add(new ColumnSeries
                    {
                        Title = filename.Replace(".csv", ""),
                        Values = new ChartValues<double>(antalposter),
                        Fill = Brushes.Red
                    });

                    SeriesCollection.Add(new ColumnSeries
                    {
                        Title = "Db affaldsposter",
                        Values = new ChartValues<double>(AntalDbPoster),
                        Fill = Brushes.Blue

                    });

                }
                else
                {
                    MessageBox.Show("Filen er i et forkert format!");
                    listResult.Items.Remove(filename);
                    virksomhedGrid.ItemsSource = null;
                    virksomhedGrid.ItemsSource = data;
                    FilLabel.Content = "";
                    ImportKnap.IsEnabled = false;
                    ImportKnap.Foreground = Brushes.Gray;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        //Liam
        private void ImportKnap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Affaldspost post in data)
                {
                    sql.Import(post.Maengde, post.Maaleenhed, post.Kategori, post.Beskrivelse, post.Ansvarlig, post.VirksomhedID, post.Dato);

                }
                MessageBox.Show("Affaldsposter importeret til databasen!");
                ErrorLogger.SaveMsg($"{Brugernavn} har importeret filen {FilLabel.Content} til databasen. {DateTime.Now}");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ikke muligt at importerer til databasen! : {ex.Message}");

            }
            finally
            {
                ImportKnap.IsEnabled = false;
                ImportKnap.Foreground = Brushes.Gray;

            }


        }

        //Liam
        private void GridKnap_Click(object sender, RoutedEventArgs e)
        {
            Content2.Visibility = Visibility.Hidden;
            Content1.Visibility = Visibility.Visible;
            GridKnap.IsEnabled = false;
            ChartKnap.IsEnabled = true;

        }

        //Liam
        private void ChartKnap_Click(object sender, RoutedEventArgs e)
        {
            Content2.Visibility = Visibility.Visible;
            Content1.Visibility = Visibility.Hidden;
            GridKnap.IsEnabled = true;
            ChartKnap.IsEnabled = false;

        }

    }

}
