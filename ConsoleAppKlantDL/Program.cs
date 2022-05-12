using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using BLKlant.Domein;
using DLKlant;

namespace ConsoleAppKlantDL
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string connectiestring = @"Data Source=LAPTOP-N78Q08DH\SQLEXPRESS;Initial Catalog=FitnessReservatiesysteem;Integrated Security=True";
            //KlantRepoADO ado = new KlantRepoADO(connectiestring);
            //List<Klant> k = ado.LeesKlanten();
            //Console.WriteLine(k[0].KlantType);
            //Console.WriteLine(k[1].KlantType);
            //Console.WriteLine(k[2].KlantType);
            //ado.SchrijfKlantInDB(k);
            //SlotsRepoADO ado = new SlotsRepoADO(connectiestring);
            //Dictionary<string, int> gereserveerdeSloten = new Dictionary<string, int>;
            //gereserveerdeSloten.Add("8-9", 4);
            //gereserveerdeSloten.Add("9-10", 4);
            //gereserveerdeSloten.Add("13-14", 1);
            //gereserveerdeSloten.Add("15-16", 2);

            //ado.GetSlots()
            //ReservatieRepoADO ado = new ReservatieRepoADO(connectiestring);
            //Reservatie r = new Reservatie(4, "a@b.be", "niels", "vdw", DateTime.Today.AddYears(-20));
            //r.gereserveerdeSlotenEnToestellen.Add("20-21", 4);

            //Reservatie a = ado.SchrijfReservatieInDB(r);
            //GereserveerdeSlotenRepoADO GSRado = new GereserveerdeSlotenRepoADO(connectiestring);
            //GSRado.SchrijfGereserveerdeSlotenInDB(a);
            //Console.WriteLine(a.ToString());

            ReservatieRepoADO ado = new ReservatieRepoADO(connectiestring);
            Reservatie r = ado.SelecteerReservatie(88, DateTime.Today);
            //Reservatie r = ado.SelecteerReservatie(4, DateTime.Today.AddYears(-20));
            Console.WriteLine(r.ToString());

            //Toestel t = ado.SchrijfNieuwToestelInDB("Fiets");
            //ado.VerwijderToestelUitDB(16);
        }
    }
}
