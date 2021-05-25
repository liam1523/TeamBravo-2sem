using System;
using System.Collections.Generic;
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

        public int[] GetKategorier(int virksomhedid)
        {
            int[] kategorier = new int[8];

            try
            {
                string command = string.Format("SELECT Kategori FROM Affaldspost WHERE VirksomhedID = {0}", virksomhedid);
                SqlCommand cmd = new SqlCommand(command, cnn);
                cnn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < kategorier.Length; i++)
                    {
                        kategorier[i] = Convert.ToInt32(reader[0]);
                    }
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

            return kategorier;

        }

        public double[] GetChartPosts(int kategori, int maaleenhed, int virksomhedid)
        {
            List<double> tal = new List<double>();

            try
            {
                string command = string.Format("SELECT Maengde FROM Affaldspost WHERE Kategori = {0} AND Maaleenhed = {1} " +
                    "AND VirksomhedID = {2}", kategori, maaleenhed, virksomhedid);
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

        public DateTime[] GetChartDates(int kategori, int maaleenhed, int virksomhedid)
        {
            List<DateTime> dateTimes = new List<DateTime>();

            try
            {
                string command = string.Format("SELECT Dato FROM Affaldspost WHERE Kategori = {0} AND Maaleenhed = {1} " +
                    "AND VirksomhedID = {2}", kategori, maaleenhed, virksomhedid);
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

        public void UpdatePost()
        {

        }

        public void GetPosts(int virksomhedID)
        {



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

    }

}
