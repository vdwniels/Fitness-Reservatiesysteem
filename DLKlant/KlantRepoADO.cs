using BLKlant.Domein;
using BLKlant.Interfaces;
using DLKlant.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLKlant
{
    public class KlantRepoADO : IKlantRepository
    {
        private string ConnectieString;
        public KlantRepoADO(string ConnectieString)
        {
            this.ConnectieString = ConnectieString;
        }
        private SqlConnection getConnection()
        {
            return new SqlConnection(ConnectieString);
        }
        public List<Klant> LeesKlanten()
        {
            string bronpad = @"C:\Users\niels\OneDrive\Documenten\progr gevorderd\Fitness Resevatiesysteem";
            string bronsbestandsnaam = "klanten.txt";
            string bestand = Path.Combine(bronpad, bronsbestandsnaam);
            List<Klant> klanten = new List<Klant>();
            using (StreamReader sr = new StreamReader(bestand))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        string[] elementen = line.Split(",");
                        string voornaam = elementen[0].Trim('\'');
                        string achternaam = elementen[1].Trim('\'');
                        string email = elementen[2].Trim('\'');
                        string adres = elementen[3].Trim('\'');
                        DateTime geboortedatum = DateTime.Parse(elementen[4].Trim('\''));
                        string interesse = elementen[5].Trim('\'');
                        string klantType = elementen[6].Trim('\'');
                        klanten.Add(new Klant(voornaam, achternaam, email, adres, geboortedatum, interesse, klantType));


                    }
                    catch (Exception ex)
                    {
                        throw new KlantRepoADOException("LeesKlant", ex);
                    }
                }
                return klanten;
            }

        }
        public void SchrijfKlantInDB(List<Klant> klanten)
            {
            SqlConnection conn = getConnection();
            try
            {
                    conn.Open();
                foreach(Klant k in klanten)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                    string query = "INSERT INTO dbo.Klanten(Voornaam,Achternaam,Email,Adres,GeboorteDatum,Interesses,KlantType) "
                    + "VALUES(@Voornaam,@Achternaam,@Email,@Adres,@GeboorteDatum,@Interesses,@KlantType)";
                    cmd.Parameters.Add(new SqlParameter("@Voornaam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Achternaam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Adres", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@GeboorteDatum", System.Data.SqlDbType.DateTime));
                    cmd.Parameters.Add(new SqlParameter("@Interesses", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@KlantType", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@Voornaam"].Value = k.Voornaam;
                    cmd.Parameters["@Achternaam"].Value = k.Achternaam;
                    cmd.Parameters["@Email"].Value = k.Email;
                    cmd.Parameters["@Adres"].Value = k.Adres;
                    cmd.Parameters["@GeboorteDatum"].Value = k.GeboorteDatum;
                    if (k.Interesse == null) cmd.Parameters["@Interesses"].Value = DBNull.Value;
                        else cmd.Parameters["@Interesses"].Value = k.Interesse;
                    cmd.Parameters["@KlantType"].Value = k.KlantType;
                        cmd.CommandText = query;
                        cmd.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                throw new KlantRepoADOException("SchrijfKlantInDB", ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public Klant SelecteerKlant(int? klantnummer, string? email)
        {
            if ((!klantnummer.HasValue) && (string.IsNullOrEmpty(email) == true))
                throw new KlantRepoADOException("SelecteerKlant - no valid input");

            Klant k = null;

            SqlConnection connection = getConnection();
            string query = "SELECT * FROM dbo.Klanten WHERE ";
            if (klantnummer.HasValue) query += "KlantNummer=@klantnummer";
            if (klantnummer.HasValue && !string.IsNullOrEmpty(email)) query += " AND ";
            if (!string.IsNullOrEmpty(email)) query += "Email=@email";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();
                try
                {
                    if (klantnummer.HasValue)
                    {
                        command.Parameters.AddWithValue("@klantnummer", klantnummer);
                    }
                    if (!string.IsNullOrEmpty(email))
                    {
                        command.Parameters.AddWithValue("@email", email);

                    }
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int klantnr = (int)reader["KlantNummer"];
                        string voornaam = (string)reader["Voornaam"];
                        string achternaam = (string)reader["Achternaam"];
                        string mail = (string)reader["Email"];
                        string adres = (string)reader["Adres"];
                        DateTime geboortedatum = (DateTime)reader["GeboorteDatum"];
                        string? interesse = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("Interesses"))) interesse = (string?)reader["Interesses"];
                        string klanttype = (string)reader["KlantType"];
                        k = new Klant(klantnr, voornaam, achternaam, mail, adres, geboortedatum, interesse, klanttype);
                    }
                }
                catch (Exception ex)
                {
                    throw new KlantRepoADOException("Selecteer Klant", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            if (k == null) throw new KlantRepoADOException("Klant bestaat niet");
            return k;
        }
    }
}
