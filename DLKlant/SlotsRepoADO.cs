using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLKlant.Interfaces;
using DLKlant.Exceptions;

namespace DLKlant
{
     public class SlotsRepoADO : ISlotsRepository
    {
        private string ConnectieString;
        public SlotsRepoADO(string ConnectieString)
        {
            this.ConnectieString = ConnectieString;
        }
        private SqlConnection getConnection()
        {
            return new SqlConnection(ConnectieString);
        }
        public Dictionary<string,int> GetSlots(List<string> gereserveerdeSloten)
        {
            Dictionary<string,int> IDs = new Dictionary<string,int>();

            SqlConnection connection = getConnection();
            try
            {
                connection.Open();
                foreach (string slot in gereserveerdeSloten)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        string query = "SELECT SlotID FROM dbo.Slots WHERE Slots=@slots";
                        command.Parameters.Add(new SqlParameter("@slots", SqlDbType.NVarChar));
                        command.Parameters["@slots"].Value = slot;
                        command.CommandText = query;
                        int n = (int)command.ExecuteScalar();
                        IDs.Add(slot, n);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SlotsRepoADOException("Get Slots", ex);
            }
            finally
            {
                connection.Close();
            }
            return IDs;
        }
        public List<string> GetAlleSloten()
        {
            List<string> sloten = new List<string>();
            SqlConnection connection = getConnection();
            string query = "SELECT Slots From Slots ";
                

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;

                connection.Open();
                try
                {
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read()) 
                    { 
                        string slot = (string)reader["Slots"];
                        sloten.Add(slot);
                    }
                    return sloten;
                }
                catch (Exception ex)
                {
                    throw new ReservatieRepoADOException("Get Alle Sloten", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

        }
    }
}
