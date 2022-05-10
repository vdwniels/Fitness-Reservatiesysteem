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
            KlantRepoADO ado = new KlantRepoADO(connectiestring);
            List<Klant> k = ado.LeesKlanten();
            Console.WriteLine(k[0].KlantType);
            Console.WriteLine(k[1].KlantType);
            Console.WriteLine(k[2].KlantType);
            //ado.SchrijfKlantInDB(k);
        }
    }
}
