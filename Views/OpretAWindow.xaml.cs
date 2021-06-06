using System;
using System.Collections.Generic;
using System.Globalization;
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

    //Liam
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

    //Liam
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

    public partial class OpretAWindow : Window
    {
        //Liam
        private int VirksomhedID;
        private string Brugernavn;
        private SqlRepository sql = new SqlRepository();

        //Liam
        public OpretAWindow(string brugernavn, int virksomhedid)
        {
            InitializeComponent();

            VirksomhedID = virksomhedid;
            Brugernavn = brugernavn;
            enhedBox.ItemsSource = Enum.GetValues(typeof(MaaleEnhed));
            kategoribox.ItemsSource = Enum.GetValues(typeof(Kategori));

        }

        //Liam
        private void Opret_Click(object sender, RoutedEventArgs e)
        {
            decimal mængde = 0;
            int måleenhed = 0;
            int kategori = 0;
            string beskrivelse = "";

            try
            {
                mængde = Convert.ToDecimal(maengdeTxt.Text.Trim().Replace(",", "."));
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
                beskrivelse = beskrivTxt.Text.Trim();

                if (måleenhed != 0 && kategori != 0 && mængde > 0)
                {
                    sql.Add(mængde, måleenhed, kategori, beskrivelse, Brugernavn, VirksomhedID);
                    MessageBox.Show("Affaldspost oprettet!");
                    ErrorLogger.SaveMsg($"{Brugernavn} har oprettet en ny affaldspost. {DateTime.Now}");
                    this.Close();
                    
                }
                else if (mængde <= 0)
                {
                    MessageBox.Show("Mængden kan ikke være 0 eller under!");

                }
                else
                {
                    MessageBox.Show("Alle felter skal være fyldt!");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Mængden er i et forkert format!");

            }

        }

        //Liam
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

    }

}
