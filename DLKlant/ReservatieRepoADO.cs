using BLKlant.Domein;
using BLKlant.Interfaces;
using DLKlant.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DLKlant
{
    public class ReservatieRepoADO : IReservatieRepository
    {
        private string ConnectieString;
        public ReservatieRepoADO(string ConnectieString)
        {
            this.ConnectieString = ConnectieString;
        }
        private SqlConnection getConnection()
        {
            return new SqlConnection(ConnectieString);
        }
        public bool BestaatReservatie(int klantnummer, DateTime datum)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT count(*) FROM dbo.Reservaties WHERE klantnummer=@klantnummer AND datum=@datum";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@klantnummer", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@datum", SqlDbType.DateTime));
                    command.Parameters["@klantnummer"].Value = klantnummer;
                    command.Parameters["@datum"].Value = datum;
                    command.CommandText = query;
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new ReservatieRepoADOException("Bestaat Reservatie", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool BestaatReservatieMetToestel(int toestel)
        {
            DateTime nu = DateTime.Today;
            SqlConnection connection = getConnection();
            string query = "SELECT Count(*) From Reservaties " +
                "LEFT JOIN GereserveerdeSloten ON Reservaties.reservatienummer = GereserveerdeSloten.reservatienummer " +
                "WHERE toestel = @toestel AND datum>@datum";


            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@toestel", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@datum", SqlDbType.DateTime));
                    command.Parameters["@toestel"].Value = toestel;
                    command.Parameters["@datum"].Value = nu;
                    command.CommandText = query;
                    int n = (int)command.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new ReservatieRepoADOException("Bestaat Reservatie", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Reservatie SchrijfReservatieInDB(Reservatie r)
        {
            SqlConnection conn = getConnection();
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string query = "INSERT INTO dbo.Reservaties (klantnummer, email, voornaam, achternaam, datum) "
                    + "output Inserted.reservatienummer VALUES(@klantnummer, @email, @voornaam, @achternaam, @datum)";
                    cmd.Parameters.Add(new SqlParameter("@klantnummer", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@voornaam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@achternaam", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@datum", System.Data.SqlDbType.DateTime));
                    cmd.Parameters["@klantnummer"].Value = r.KlantNummer;
                    cmd.Parameters["@email"].Value = r.Emailadres;
                    cmd.Parameters["@voornaam"].Value = r.Voornaam;
                    cmd.Parameters["@achternaam"].Value = r.Achternaam;
                    cmd.Parameters["@datum"].Value = r.Datum;
                    cmd.CommandText = query;
                    int newID = (int)cmd.ExecuteScalar();
                    r.ZetReservatienummer(newID);
                    return r;
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
        public Reservatie SelecteerReservatie (int klantnummer,DateTime datum)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT Reservaties.reservatienummer, Reservaties.klantnummer, Reservaties.email, Reservaties.voornaam, Reservaties.achternaam, Reservaties.datum, GereserveerdeSloten.toestel, GereserveerdeSloten.slot From Reservaties " +
                "LEFT JOIN GereserveerdeSloten ON Reservaties.reservatienummer = GereserveerdeSloten.reservatienummer " +
                "WHERE klantnummer = @klantnummer AND datum=@datum";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@klantnummer", klantnummer);
                command.Parameters.AddWithValue("@datum", datum);
                connection.Open();
                try
                {
                    Dictionary<string, int> sloten = new Dictionary<string, int>();
                    IDataReader reader = command.ExecuteReader();
                    Reservatie r = null;
                    while (reader.Read())
                    {
                        int reservatienummer = (int)reader["reservatienummer"];
                        
                        string email = (string)reader["email"];
                        string voornaam = (string)reader["voornaam"];
                        string achternaam = (string)reader["achternaam"];
                        DateTime resdatum = (DateTime)reader["datum"];
                        r = new Reservatie(reservatienummer, klantnummer, email, voornaam, achternaam, resdatum);
                        int toestel = (int)reader["toestel"];
                        string slot = (string)reader["slot"];
                        sloten.Add(slot, toestel);
                    }
                    //r.ZetSlotEnToestel(sloten);
                    return r;
                }
                catch (Exception ex)
                {
                    throw new ReservatieRepoADOException("Selecteer Reservatie", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
