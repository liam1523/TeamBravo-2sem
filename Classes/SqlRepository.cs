using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBravo___2.Semester___Eksamensopgave
{
    public class SqlRepository
    {
        public string connectionString;
        public SqlConnection cnn;

        public SqlRepository()
        {
            connectionString = "Data Source = .; Initial Catalog = TeamBravo; Integrated Security = True";
            cnn = new SqlConnection(connectionString);

        }

        public int[] GetKategorier()
        {
            List<int> liste = new List<int>();

            try
            {
                string command = string.Format("SELECT Kategori FROM Affaldspost");
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    liste.Add(Convert.ToInt32(reader[0]));

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under hentning af kategorier! : {ex.Message}");
                return null;

            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            int[] kategorier = new int[liste.Count];
            for (int i = 0; i < kategorier.Length; i++)
            {
                kategorier[i] = liste[i];

            }

            return kategorier;

        }

        public double[] GetChartPosts(int kategori, int maaleenhed)
        {
            List<double> tal = new List<double>();

            try
            {
                string command = string.Format("SELECT Maengde FROM Affaldspost WHERE Kategori = {0} AND Maaleenhed = {1}", kategori, maaleenhed);
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tal.Add(Convert.ToDouble(reader[0]));

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under hentning af mængder til chart!  : {ex.Message}");
                return null;

            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();

                }

            }


            double[] data = new double[tal.Count];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = tal[i];
            }
            return data;

        }

        public DateTime[] GetChartDates(int kategori, int maaleenhed)
        {
            List<DateTime> dateTimes = new List<DateTime>();

            try
            {
                string command = string.Format("SELECT Dato FROM Affaldspost WHERE Kategori = {0} AND Maaleenhed = {1}", kategori, maaleenhed);
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dateTimes.Add(Convert.ToDateTime(reader[0]));

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under hentning af datoer til chart! : {ex.Message}");
                return null;
            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            DateTime[] dates = new DateTime[dateTimes.Count];
            for (int i = 0; i < dates.Length; i++)
            {
                dates[i] = dateTimes[i];
            }
            return dates;

        }

        public void Import(decimal mængde, int måleenhed, int kategori, string beskrivelse, string ansvarlig, int virksomhedid, DateTime dato)
        {
            try
            {
                string command = string.Format("INSERT INTO Affaldspost VALUES ({0}, {1}, {2}, '{3}', '{4}', {5}, '{6}')", mængde, måleenhed, kategori, beskrivelse, ansvarlig, virksomhedid, dato.ToString("yyyy-MM-dd HH:mm"));
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under oprettelse af ny affaldspost! : {ex.Message}");

            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }

            }

        }

        public void Add(decimal mængde, int måleenhed, int kategori, string beskrivelse, string ansvarlig, int virksomhedid)
        {
            try
            {
                string command = string.Format("INSERT INTO Affaldspost VALUES ({0}, {1}, {2}, '{3}', '{4}', {5}, GETDATE())", mængde, måleenhed, kategori, beskrivelse, ansvarlig, virksomhedid);
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under oprettelse af ny affaldspost! : {ex.Message}");

            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }

            }

        }

        public void RemovePost(int postId)
        {
            try
            {
                string command = string.Format("DELETE FROM Affaldspost WHERE PostID = {0}", postId);
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under sletning af affaldspost! : {ex.Message}");
                throw;
            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();

                }

            }

        }

        public void UpdatePost(Affaldspost post)
        {

            try
            {
                string command = string.Format("UPDATE Affaldspost SET " +
                    "Maengde = {0}, " +
                    "Maaleenhed = {1}, " +
                    "Kategori = {2}, " +
                    "Beskrivelse = '{3}' " +
                    "WHERE PostID = {4}", post.Maengde, post.Maaleenhed, post.Kategori, post.Beskrivelse, post.ID);
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under opdatering af en affaldspost! : {ex.Message}");
                throw;
            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }

            }

        }

        public ObservableCollection<Affaldspost> GetAffaldsposts()
        {
            ObservableCollection<Affaldspost> affaldsposts = new ObservableCollection<Affaldspost>();

            try
            {
                string command = string.Format("SELECT * FROM Affaldspost");
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    affaldsposts.Add(new Affaldspost
                    {
                        ID = Convert.ToInt32(reader[0]),
                        Maengde = Convert.ToDecimal(reader[1]),
                        Maaleenhed = Convert.ToInt32(reader[2]),
                        Kategori = Convert.ToInt32(reader[3]),
                        Beskrivelse = reader[4].ToString(),
                        Ansvarlig = reader[5].ToString(),
                        VirksomhedID = Convert.ToInt32(reader[6]),
                        Dato = Convert.ToDateTime(reader[7])
                    });

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under hentning af affaldsposter! : {ex.Message}");
                return null;
            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

            return affaldsposts;

        }


        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            try
            {
                string command = string.Format("SELECT * FROM User_");
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Brugernavn = reader[0].ToString(),
                        VirksomhedID = Convert.ToInt32(reader[1])

                    });

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under hentning af brugere! : {ex.Message}");
                return null;

            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();

                }

            }

            return users;

        }

        public ObservableCollection<Affaldspost> Search(string måleenhed, string kategori, string ansvarlig, string virksomhedid)
        {
            ObservableCollection<Affaldspost> affaldsposts = new ObservableCollection<Affaldspost>();

            try
            {
                string command = string.Format("SELECT * FROM Affaldspost WHERE " +
                    "Maaleenhed like '{0}%' and " +
                    "Kategori like '{1}%' and " +
                    "Ansvarlig like '{2}%' and " +
                    "VirksomhedID like '{3}%'", måleenhed, kategori, ansvarlig, virksomhedid);
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    affaldsposts.Add(new Affaldspost
                    {
                        ID = Convert.ToInt32(reader[0]),
                        Maengde = Convert.ToDecimal(reader[1]),
                        Maaleenhed = Convert.ToInt32(reader[2]),
                        Kategori = Convert.ToInt32(reader[3]),
                        Beskrivelse = reader[4].ToString(),
                        Ansvarlig = reader[5].ToString(),
                        VirksomhedID = Convert.ToInt32(reader[6]),
                        Dato = Convert.ToDateTime(reader[7])

                    });

                }

            }
            catch (Exception ex)
            {
                ErrorLogger.SaveMsg($"Noget gik galt under søgning af affaldsposter! : {ex.Message}");
                return null;
            }
            finally
            {
                if (cnn != null && cnn.State == System.Data.ConnectionState.Open)
                {
                    cnn.Close();

                }

            }

            return affaldsposts;

        }

    }

}
