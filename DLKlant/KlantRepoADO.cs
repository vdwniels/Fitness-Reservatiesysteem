using BLKlant.Domein;
using BLKlant.Interfaces;
using DLKlant.Exceptions;
using System;
using System.Collections.Generic;
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
    }
}
