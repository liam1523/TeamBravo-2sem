using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class AffaldWindow : Window //Liam
    {
        //Liam
        private List<Affaldspost> affaldsposter = new List<Affaldspost>();
        private SqlRepository sql = new SqlRepository();

        private int VirksomhedID;
        private string Brugernavn;

        //Liam
        public AffaldWindow(string brugernavn, int virksomhedid)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            VirksomhedID = virksomhedid;
            Brugernavn = brugernavn;

            //Ditte
            affaldsposter = sql.GetAffaldsposts();

            //Liam
            if (affaldsposter != null)
            {
                affaldGrid.ItemsSource = affaldsposter;

            }
            else
            {
                MessageBox.Show("Kunne ikke få fat på affaldsposterne!");

            }

            
        }

        //Liam
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            char[] charArray;
            string substring;
            StringBuilder sb = new StringBuilder();

            try
            {
                affaldGrid.ClipboardCopyMode = DataGridClipboardCopyMode.ExcludeHeader;
                ApplicationCommands.Copy.Execute(null, affaldGrid);
                affaldGrid.UnselectAllCells();
                string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

                charArray = result.ToCharArray();

                for (int i = 0; i < charArray.Length; i++)
                {
                    if (charArray[i] != ',')
                    {
                        sb.Append(charArray[i]);
                    }
                    else
                    {
                        if (charArray[i + 1] == ' ')
                        {
                            sb.Append(charArray[i]);
                        }
                        else
                        {
                            sb.Append(';');
                        }

                    }

                }

                substring = sb.ToString();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Fil (*.csv)|*.csv";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, substring, UnicodeEncoding.UTF8);

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Kunne ikke eksporterer de valgte affaldsposter!");

            }

        }

        //Liam
        private void Opret_Click(object sender, RoutedEventArgs e)
        {
            OpretAWindow opretA = new OpretAWindow(Brugernavn, VirksomhedID);
            opretA.ShowDialog();

            affaldGrid.UnselectAll();

            affaldsposter.Clear();
            affaldsposter = sql.GetAffaldsposts();
            affaldGrid.ItemsSource = affaldsposter;

        }

        //Liam
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            affaldGrid.UnselectAll();

            var måleenhed = måleTxt.Text.Trim();
            var kategori = kategoriTxt.Text.Trim();
            var ansvarlig = ansvarTxt.Text.Trim();
            var virksomhedid = virkTxt.Text.Trim();

            affaldsposter.Clear();

            affaldsposter = sql.Search(
                måleenhed, kategori, ansvarlig, virksomhedid);

            affaldGrid.ItemsSource = affaldsposter;

        }

        //Liam
        private void måleenhed_Changed(object sender, TextChangedEventArgs e)
        {
            SøgKnap.IsEnabled = true;
            SøgKnap.Foreground = Brushes.White;

        }

        //Liam
        private void kategori_Changed(object sender, TextChangedEventArgs e)
        {
            SøgKnap.IsEnabled = true;
            SøgKnap.Foreground = Brushes.White;

        }

        //Liam
        private void ansvarlig_Changed(object sender, TextChangedEventArgs e)
        {
            SøgKnap.IsEnabled = true;
            SøgKnap.Foreground = Brushes.White;

        }

        //Liam
        private void virkId_Changed(object sender, TextChangedEventArgs e)
        {
            SøgKnap.IsEnabled = true;
            SøgKnap.Foreground = Brushes.White;

        }

        //Liam
        private void ClearFields_Click(object sender, RoutedEventArgs e)
        {
            affaldGrid.UnselectAll();
            måleTxt.Clear();
            kategoriTxt.Clear();
            ansvarTxt.Clear();
            virkTxt.Clear();

            SøgKnap.IsEnabled = false;
            SøgKnap.Foreground = Brushes.Gray;

            affaldsposter.Clear();
            affaldsposter = sql.GetAffaldsposts();
            affaldGrid.ItemsSource = affaldsposter;

        }

        //Liam
        private void affaldGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (affaldGrid.SelectedCells.Count == 0)
            {
                ExportKnap.IsEnabled = false;
                ExportKnap.Foreground = Brushes.Gray;

            }
            else
            {
                ExportKnap.IsEnabled = true;
                ExportKnap.Foreground = Brushes.White;

            }

        }

        //Liam
        private void affaldGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Affaldspost affaldspost = (Affaldspost)row.Item;
            OpdaterAWindow opdaterAWindow = new OpdaterAWindow(affaldspost, Brugernavn);
            opdaterAWindow.ShowDialog();

            affaldGrid.UnselectAll();

            affaldsposter.Clear();
            affaldsposter = sql.GetAffaldsposts();
            affaldGrid.ItemsSource = affaldsposter;

        }

    }

}
