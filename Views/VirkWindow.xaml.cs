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
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class VirkWindow : Window
    {
        public List<Affaldspost> data = new List<Affaldspost>();
        public SqlRepository sql = new SqlRepository();

        public VirkWindow()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            InitializeComponent();

            var files = Directory.GetFiles("C:\\Dropzone");
            foreach (var item in files)
            {
                string file = System.IO.Path.GetFileName(item);
                listResult.Items.Add(file);

            }

        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ImportKnap.IsEnabled = true;
            ImportKnap.Foreground = Brushes.White;

            string filename = listResult.SelectedItem.ToString();
            FilLabel.Content = filename;

            string path = string.Format("C:\\Dropzone\\{0}", filename);
            var lines = File.ReadAllLines(path);

            data.Clear();

            foreach (var item in lines)
            {
                var split = item.Split(';');
                data.Add(new Affaldspost
                {
                    ID = int.Parse(split[0]),
                    Maengde = decimal.Parse(split[1]),
                    Maaleenhed = int.Parse(split[2]),
                    Kategori = int.Parse(split[3]),
                    Beskrivelse = split[4],
                    Ansvarlig = split[5],
                    VirksomhedID = int.Parse(split[6]),
                    Dato = DateTime.Parse(split[7])

                });

            }

            virksomhedGrid.ItemsSource = null;
            virksomhedGrid.ItemsSource = data;

        }

        private void ImportKnap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Affaldspost post in data)
                {
                    sql.Import(post.Maengde, post.Maaleenhed, post.Kategori, post.Beskrivelse, post.Ansvarlig, post.VirksomhedID, post.Dato);

                }
                MessageBox.Show("Affaldsposter importeret til databasen!");
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

        private void GridKnap_Click(object sender, RoutedEventArgs e)
        {
            Content2.Visibility = Visibility.Hidden;
            Content1.Visibility = Visibility.Visible;
            GridKnap.IsEnabled = false;
            ChartKnap.IsEnabled = true;

        }

        private void ChartKnap_Click(object sender, RoutedEventArgs e)
        {
            Content2.Visibility = Visibility.Visible;
            Content1.Visibility = Visibility.Hidden;
            GridKnap.IsEnabled = true;
            ChartKnap.IsEnabled = false;

        }

    }

}
