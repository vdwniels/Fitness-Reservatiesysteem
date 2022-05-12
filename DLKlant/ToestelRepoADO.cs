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
                    if(t.Status=="Beschikbaar")
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
    }
}
