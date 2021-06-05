using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeamBravo___2.Semester___Eksamensopgave
{
    public partial class LoginWindow : Window
    {
        //Liam
        SqlRepository sql = new SqlRepository();
        List<User> users = new List<User>();

        //Liam
        public LoginWindow()
        {
            InitializeComponent();

            users = sql.GetUsers();
            if (users == null)
            {
                MessageBox.Show("Ingen forbindelse til databasen.");
                Environment.Exit(0);

            }

            UserTxt.Focus();

        }

        //Liam
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UserTxt.Text.Trim();
            string password = username.ToLower() + "123";
            bool IsValid = false;
            User bruger = new User();

            foreach (var user in users)
            {
                if (user.Brugernavn.ToLower() == username.ToLower())
                {
                    bruger = user;
                    IsValid = true;
                    break;
                }

            }

            if (IsValid == true && password == PwBox.Password)
            {
                StartsideWindow ssw = new StartsideWindow(bruger.Brugernavn, bruger.VirksomhedID);
                this.Hide();
                ssw.ShowDialog();

            }
            else if (IsValid == true && password != PwBox.Password)
            {
                MessageBox.Show("Adgangskode forkert!");
                PwBox.Password = "";

            }
            else
            {
                MessageBox.Show("Brugeren findes ikke i systemet!");
                PwBox.Password = "";
                UserTxt.Text = "";

            }

        }

    }

}
