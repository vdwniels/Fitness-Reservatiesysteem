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
    public class GereserveerdeSlotenRepoADO : IGereserveerdeSlotenRepository
    {
        private string ConnectieString;
        public GereserveerdeSlotenRepoADO(string ConnectieString)
        {
            this.ConnectieString = ConnectieString;
        }
        private SqlConnection getConnection()
        {
            return new SqlConnection(ConnectieString);
        }

        public void SchrijfGereserveerdeSlotenInDB (Reservatie r)
        {
            {
                SqlConnection conn = getConnection();
                try
                {
                    conn.Open();
                    foreach (var SET in r.gereserveerdeSlotenEnToestellen)
                    {
                        if(!GereserveerdSlotBestaat(r.ReservatieNummer, SET.Value, SET.Key))
                        {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            string query = "INSERT INTO dbo.GereserveerdeSloten (reservatienummer,toestel,slot) "
                            + "output Inserted.reservatienummer VALUES(@reservatienummer,@toestel,@slot)";
                            cmd.Parameters.Add(new SqlParameter("@reservatienummer", System.Data.SqlDbType.Int));
                            cmd.Parameters.Add(new SqlParameter("@toestel", System.Data.SqlDbType.Int));
                            cmd.Parameters.Add(new SqlParameter("@slot", System.Data.SqlDbType.NVarChar));
                            cmd.Parameters["@reservatienummer"].Value = r.ReservatieNummer;
                            cmd.Parameters["@toestel"].Value = SET.Value;
                            cmd.Parameters["@slot"].Value = SET.Key;
                            cmd.CommandText = query;
                            cmd.ExecuteScalar();
                        }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ReservatieRepoADOException("SchrijfReservatieInDB", ex);
                }
                finally
                {
                    conn.Close();
                }

            }

        }
       public bool GereserveerdSlotBestaat(int reservatienummer, int toestel, string slot)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT count(*) FROM dbo.GereserveerdeSloten WHERE reservatienummer=@reservatienummer AND toestel=@toestel AND slot=@slot";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@reservatienummer", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@toestel", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@slot", SqlDbType.NVarChar));

                    command.Parameters["@reservatienummer"].Value = reservatienummer;
                    command.Parameters["@toestel"].Value = toestel;
                    command.Parameters["@slot"].Value = slot;

                    command.CommandText = query;
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new GereserveerdeSlotenRepoADOException("Bestaat Reservatie", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

        }
    }
}
