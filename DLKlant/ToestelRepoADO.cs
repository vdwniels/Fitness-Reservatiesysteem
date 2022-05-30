using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLKlant.Domein;
using BLKlant.Interfaces;
using DLKlant.Exceptions;

namespace DLKlant
{
    public class ToestelRepoADO : IToestelRepository
    {
        private string ConnectieString;
        public ToestelRepoADO(string ConnectieString)
        {
            this.ConnectieString = ConnectieString;
        }
        private SqlConnection getConnection()
        {
            return new SqlConnection(ConnectieString);
        }

        public void UpdateStatus(List<Toestel> toestellen)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE Toestellen SET Status=@status WHERE Id=@id";

            connection.Open();
                    try
                    {
                        foreach (Toestel t in toestellen)
                        {
                            using (SqlCommand command = connection.CreateCommand())
                            {
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@status", SqlDbType.NVarChar));
                        command.Parameters["@id"].Value = t.ToestelNummer;
                        if (t.Status == "Beschikbaar")
                            command.Parameters["@status"].Value = "Buiten Gebruik";
                        else
                            command.Parameters["@status"].Value = "Beschikbaar";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ToestelRepoADOException("UpdateStatus", ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                
            }
        
        public Toestel SchrijfNieuwToestelInDB(string toesteltype)
        {
            SqlConnection conn = getConnection();
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // dbo.Toestellen kolom "status" vult vanzelf "beschikbaar" in.
                    string query = "INSERT INTO dbo.Toestellen  (ToestelType) output Inserted.Id VALUES(@toesteltype)";
                    cmd.Parameters.Add(new SqlParameter("@toesteltype", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters["@toesteltype"].Value = toesteltype;
                    cmd.CommandText = query;
                    int newID = (int)cmd.ExecuteScalar();
                    Toestel t = new Toestel(newID, toesteltype);
                    return t;

                }
            }
            catch (Exception ex)
            {
                throw new ToestelRepoADOException("SchrijfNieuwToestelInDB");
            }
            finally
            {
                conn.Close();
            }
        }
        public void VerwijderToestelUitDB(List<Toestel> toestellen)
        {
            SqlConnection conn = getConnection();
            try
            {
                conn.Open();
                foreach (Toestel t in toestellen)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        string query = "DELETE FROM Toestellen WHERE Id=@id";
                        cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));
                        cmd.Parameters["@id"].Value = t.ToestelNummer;
                        cmd.CommandText = query;
                        cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ToestelRepoADOException("VerwijderToestelUitDB");
            }
            finally
            {
                conn.Close();
            }
        }
        public List<Toestel> GetAlleToestellen()
        {
            List<Toestel> toestellen = new List<Toestel>();
            SqlConnection connection = getConnection();
            string query = "SELECT * From Toestellen WHERE Status = 'beschikbaar'";


            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                connection.Open();
                try
                {
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string toestelType = (string)reader["ToestelType"];
                        string status = (string)reader["Status"];

                        toestellen.Add(new Toestel(id,toestelType,status));
                    }
                    return toestellen;
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("Get Alle Toestellen", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

        }
        public List<Toestel> GetBeschikbaarEnBuitenGebruik()
        {
            List<Toestel> toestellen = new List<Toestel>();
            SqlConnection connection = getConnection();
            string query = "SELECT * From Toestellen";


            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                connection.Open();
                try
                {
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string toestelType = (string)reader["ToestelType"];
                        string status = (string)reader["Status"];

                        toestellen.Add(new Toestel(id, toestelType, status));
                    }
                    return toestellen;
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("GetBeschikbaarEnBuitengebruik", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public List<Toestel> GetBezetteToestellen(DateTime datum,string slot)
        {
            List<Toestel> toestellen = new List<Toestel>();
            SqlConnection connection = getConnection();
            string query = "SELECT Toestellen.* From Toestellen "+
"Join GereserveerdeSloten On Toestellen.Id = GereserveerdeSloten.toestel "+
"Left Join Reservaties ON Reservaties.reservatienummer = GereserveerdeSloten.reservatienummer "+
"WHERE Reservaties.datum = @datum AND GereserveerdeSloten.slot = @slot; ";


            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@slot", slot);
                command.Parameters.AddWithValue("@datum", datum);


                connection.Open();
                try
                {
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string toestelType = (string)reader["ToestelType"];
                        string status = (string)reader["Status"];

                        toestellen.Add(new Toestel(id, toestelType, status));
                    }
                    return toestellen;
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("Get Beschikbare Toestellen", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

        }
        public List<string> GetToestelTypes()
        {
            List<string> toestelTypes = new List<string>();
            SqlConnection connection = getConnection();
            string query = "SELECT * From ToestelType";


            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                connection.Open();
                try
                {
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        string type = (string)reader["Type"];

                        toestelTypes.Add(type);
                    }
                    return toestelTypes;
                }
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("GetToestelTypes", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

        }
    }
}
