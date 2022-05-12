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

        public void UpdateStatus(Toestel t)
        {
            SqlConnection connection = getConnection();
            string query = "UPDATE Toestellen SET Status=@status WHERE Id=@id";

            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                try
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
                catch (Exception ex)
                {
                    throw new ToestelRepoADOException("UpdateStatus", ex);
                }
                finally
                {
                    connection.Close();
                }
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
        public void VerwijderToestelUitDB(int id)
        {
            SqlConnection conn = getConnection();
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string query = "DELETE FROM Toestellen WHERE Id=@id";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id;
                    cmd.CommandText = query;
                    cmd.ExecuteScalar();
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
    }
}
