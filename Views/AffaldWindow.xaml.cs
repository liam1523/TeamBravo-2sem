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

    public partial class AffaldWindow : Window
    {
        public ObservableCollection<Affaldspost> affaldsposter = new ObservableCollection<Affaldspost>();
        public SqlRepository sql = new SqlRepository();

        public int VirksomhedID;
        public string Brugernavn;
        public AffaldWindow(string brugernavn, int virksomhedid)

        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            VirksomhedID = virksomhedid;
            Brugernavn = brugernavn;
            affaldsposter = sql.GetAffaldsposts();
            affaldGrid.ItemsSource = affaldsposter;
            
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            affaldGrid.ClipboardCopyMode = DataGridClipboardCopyMode.ExcludeHeader;
            ApplicationCommands.Copy.Execute(null, affaldGrid);
            affaldGrid.UnselectAllCells();
            string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

            StringBuilder sb = new StringBuilder();
            char[] charArray = result.ToCharArray();
            string substring;

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

        private void Opret_Click(object sender, RoutedEventArgs e)
        {
            OpretAWindow opretA = new OpretAWindow(Brugernavn, VirksomhedID);
            opretA.ShowDialog();

            affaldGrid.UnselectAll();

            affaldsposter.Clear();
            affaldsposter = sql.GetAffaldsposts();
            affaldGrid.ItemsSource = affaldsposter;
        }

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

        private void måleenhed_Changed(object sender, TextChangedEventArgs e)
        {
            SøgKnap.IsEnabled = true;
            SøgKnap.Foreground = Brushes.White;

        }

        private void kategori_Changed(object sender, TextChangedEventArgs e)
        {
            SøgKnap.IsEnabled = true;
            SøgKnap.Foreground = Brushes.White;

        }

        private void ansvarlig_Changed(object sender, TextChangedEventArgs e)
        {
            SøgKnap.IsEnabled = true;
            SøgKnap.Foreground = Brushes.White;

        }

        private void virkId_Changed(object sender, TextChangedEventArgs e)
        {
            SøgKnap.IsEnabled = true;
            SøgKnap.Foreground = Brushes.White;

        }

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

        private void affaldGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            Affaldspost affaldspost = (Affaldspost)row.Item;
            OpdaterAWindow opdaterAWindow = new OpdaterAWindow(affaldspost);
            opdaterAWindow.ShowDialog();

            affaldGrid.UnselectAll();

            affaldsposter.Clear();
            affaldsposter = sql.GetAffaldsposts();
            affaldGrid.ItemsSource = affaldsposter;

        }

    }

}
