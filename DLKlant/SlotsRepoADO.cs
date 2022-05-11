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
        public Dictionary<int,int> GetSlots(Dictionary<string, int> gereserveerdeSloten)
        {
            Dictionary<int,int> IDs = new Dictionary<int,int>();

            SqlConnection connection = getConnection();
            try
            {
                connection.Open();
                foreach (var slot in gereserveerdeSloten)
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        string query = "SELECT SlotID FROM dbo.Slots WHERE Slots=@slots";
                        command.Parameters.Add(new SqlParameter("@slots", SqlDbType.NVarChar));
                        command.Parameters["@slots"].Value = slot.Key;
                        command.CommandText = query;
                        int n = (int)command.ExecuteScalar();
                        IDs.Add(n, slot.Value);
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

    }
}
