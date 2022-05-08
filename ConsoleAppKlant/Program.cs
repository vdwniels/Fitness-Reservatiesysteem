using System;
using BLKlant.Domein;
namespace ConsoleAppKlant
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Reservatie r = new Reservatie(1, "a", "a", "a", DateTime.Now.AddDays(1));
            r.gereserveerdeSlotenEnToestellen.Add("9-10", "25");
            r.gereserveerdeSlotenEnToestellen.Add("10-11", "25");
            r.gereserveerdeSlotenEnToestellen.Add("8-9", "25");

            foreach(var a in r.gereserveerdeSlotenEnToestellen)
            {
                Console.WriteLine(a.Key + a.Value);
            }
        }
    }
}
