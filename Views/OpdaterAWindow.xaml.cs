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
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class OpdaterAWindow : Window
    {
        public enum MaaleEnhed
        {
            Colli = 1,
            Stk = 2,
            Ton = 3,
            Kilogram = 4,
            Gram = 5,
            M3 = 6,
            Liter = 7,
            Hektoliter = 8

        }

        public enum Kategori
        {
            Batterier = 1,
            Biler = 2,
            Elektronikaffald = 3,
            ImprægneretTræ = 4,
            Inventar = 5,
            OrganiskAffald = 6,
            PapOgPapir = 7,
            Plastemballager = 8,
            PVC = 9

        }

        private Affaldspost Affaldspost { get; set; }

        private SqlRepository sql = new SqlRepository();
        private string Brugernavn;

        public OpdaterAWindow(Affaldspost affaldspost, string brugernavn)
        {
            InitializeComponent();
            Affaldspost = affaldspost;
            Brugernavn = brugernavn;
            enhedBox.ItemsSource = Enum.GetValues(typeof(MaaleEnhed));
            kategoribox.ItemsSource = Enum.GetValues(typeof(Kategori));
            maengdeTxt.Text = affaldspost.Maengde.ToString();
            switch (affaldspost.Maaleenhed)
            {
                case 1:
                    enhedBox.SelectedItem = MaaleEnhed.Colli;
                    break;
                case 2:
                    enhedBox.SelectedItem = MaaleEnhed.Stk;
                    break;
                case 3:
                    enhedBox.SelectedItem = MaaleEnhed.Ton;
                    break;
                case 4:
                    enhedBox.SelectedItem = MaaleEnhed.Kilogram;
                    break;
                case 5:
                    enhedBox.SelectedItem = MaaleEnhed.Gram;
                    break;
                case 6:
                    enhedBox.SelectedItem = MaaleEnhed.M3;
                    break;
                case 7:
                    enhedBox.SelectedItem = MaaleEnhed.Liter;
                    break;
                case 8:
                    enhedBox.SelectedItem = MaaleEnhed.Hektoliter;
                    break;
                default:
                    break;
            }
            switch (affaldspost.Kategori)
            {
                case 1:
                    kategoribox.SelectedItem = Kategori.Batterier;
                    break;
                case 2:
                    kategoribox.SelectedItem = Kategori.Biler;
                    break;
                case 3:
                    kategoribox.SelectedItem = Kategori.Elektronikaffald;
                    break;
                case 4:
                    kategoribox.SelectedItem = Kategori.ImprægneretTræ;
                    break;
                case 5:
                    kategoribox.SelectedItem = Kategori.Inventar;
                    break;
                case 6:
                    kategoribox.SelectedItem = Kategori.OrganiskAffald;
                    break;
                case 7:
                    kategoribox.SelectedItem = Kategori.PapOgPapir;
                    break;
                case 8:
                    kategoribox.SelectedItem = Kategori.Plastemballager;
                    break;
                case 9:
                    kategoribox.SelectedItem = Kategori.PVC;
                    break;
                default:
                    break;
            }
            beskrivTxt.Text = affaldspost.Beskrivelse.ToString();

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal mængde = Convert.ToDecimal(maengdeTxt.Text.Trim().Replace(",", "."));
                int måleenhed = 0;
                int kategori = 0;
                string beskrivelse = beskrivTxt.Text.Trim();
                switch (enhedBox.SelectedItem)
                {
                    case MaaleEnhed.Colli:
                        måleenhed = 1;
                        break;
                    case MaaleEnhed.Stk:
                        måleenhed = 2;
                        break;
                    case MaaleEnhed.Ton:
                        måleenhed = 3;
                        break;
                    case MaaleEnhed.Kilogram:
                        måleenhed = 4;
                        break;
                    case MaaleEnhed.Gram:
                        måleenhed = 5;
                        break;
                    case MaaleEnhed.M3:
                        måleenhed = 6;
                        break;
                    case MaaleEnhed.Liter:
                        måleenhed = 7;
                        break;
                    case MaaleEnhed.Hektoliter:
                        måleenhed = 8;
                        break;
                    default:
                        break;
                }
                switch (kategoribox.SelectedItem)
                {
                    case Kategori.Batterier:
                        kategori = 1;
                        break;
                    case Kategori.Biler:
                        kategori = 2;
                        break;
                    case Kategori.Elektronikaffald:
                        kategori = 3;
                        break;
                    case Kategori.ImprægneretTræ:
                        kategori = 4;
                        break;
                    case Kategori.Inventar:
                        kategori = 5;
                        break;
                    case Kategori.OrganiskAffald:
                        kategori = 6;
                        break;
                    case Kategori.PapOgPapir:
                        kategori = 7;
                        break;
                    case Kategori.Plastemballager:
                        kategori = 8;
                        break;
                    case Kategori.PVC:
                        kategori = 9;
                        break;
                    default:
                        break;
                }

                Affaldspost affaldspost = new Affaldspost
                {
                    ID = Affaldspost.ID,
                    Maengde = mængde,
                    Maaleenhed = måleenhed,
                    Kategori = kategori,
                    Beskrivelse = beskrivelse,
                    Ansvarlig = Affaldspost.Ansvarlig,
                    VirksomhedID = Affaldspost.VirksomhedID,
                    Dato = Affaldspost.Dato
                };

                sql.UpdatePost(affaldspost);

                MessageBox.Show("Affaldspost opdateret!");
                ErrorLogger.SaveMsg($"{Brugernavn} har opdateret en affaldspost. {DateTime.Now}");
                this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Mængden er i et forkert format!");

            }

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sql.RemovePost(Affaldspost.ID);
                MessageBox.Show("Affaldspost slettet!");
                ErrorLogger.SaveMsg($"{Brugernavn} har slettet en affaldspost. {DateTime.Now}");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ikke muligt at slette posten! : {ex.Message}");

            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

    }

}
