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
            string filename = listResult.SelectedItem.ToString();

            string path = string.Format("C:\\Dropzone\\{0}", filename);
            var lines = File.ReadAllLines(path);

            List<Affaldspost> data = new List<Affaldspost>();

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
                    Dato = (DateTime)DateTime.Parse(split[7])

                });

            }

            virksomhedGrid.ItemsSource = data;

        }

    }

}
