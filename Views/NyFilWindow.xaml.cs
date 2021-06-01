using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for NyFilWindow.xaml
    /// </summary>
    public partial class NyFilWindow : Window
    {
        public List<Affaldspost> affaldsposter = new List<Affaldspost>();
        SqlRepository sql = new SqlRepository();

        public NyFilWindow(List<Affaldspost> affaldsposts)
        {
            InitializeComponent();
            foreach (Affaldspost post in affaldsposts)
            {
                if (post.IsValid)
                {
                    affaldsposter.Add(post);

                }

            }
            Dispatcher.Invoke(() => nyfilGrid.ItemsSource = affaldsposts);

        }

        private void ImportToDb_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Affaldspost post in affaldsposter)
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

    }

}
