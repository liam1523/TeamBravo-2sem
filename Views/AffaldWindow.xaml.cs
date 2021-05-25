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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AffaldWindow : Window
    {
        public ObservableCollection<Affaldspost> affaldsposter = new ObservableCollection<Affaldspost>();

        public int VirksomhedID;
        public string Brugernavn;
        public AffaldWindow(string brugernavn, int virksomhedid)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            VirksomhedID = virksomhedid;
            Brugernavn = brugernavn;
            affaldGrid.ItemsSource = affaldsposter;

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            affaldGrid.SelectAllCells();
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

        }

    }

}
