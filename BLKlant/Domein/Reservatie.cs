using System;
using System.Collections.Generic;
using System.Linq;
using BLKlant.Exceptions;
namespace BLKlant.Domein
{
    public class Reservatie
    {
        public Dictionary<string, int> gereserveerdeSlotenEnToestellen = new Dictionary<string, int>();
        private Dictionary<int, int> gereserveerdeSlotIDsEnToestellen = new Dictionary<int, int>();
        public Reservatie(int klantNummer, string emailadres, string voornaam, string achternaam, DateTime datum)
        {
            
            KlantNummer = klantNummer;
            Emailadres = emailadres;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Datum = datum;
        }

        public Reservatie(int reservatieNummer, int klantNummer, string emailadres, string voornaam, string achternaam, DateTime datum)
        {
            ReservatieNummer = reservatieNummer;
            KlantNummer = klantNummer;
            Emailadres = emailadres;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Datum = datum;
        }

        public int ReservatieNummer { get; set; }
        public int KlantNummer { get; set; }
        public string Emailadres { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime Datum { get; set; }


        public void ZetReservatienummer(int reservatienummer)
        {
            if (reservatienummer <= 0) throw new ReservatieExeption("ZetReservatienummer - kleiner dan nul");
            ReservatieNummer = reservatienummer;

        }
        public void ZetKlantnummer(int klantnummer)
        {
            if (klantnummer <= 0) throw new ReservatieExeption("ZetKlantnummer - kleiner dan nul");
            KlantNummer = klantnummer;
        }
        public void ZetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ReservatieExeption("ZetEmail - leeg");
            if (!email.Contains('@')) throw new ReservatieExeption("ZetEmail - Ongeldig emailadres");
            Emailadres = email.Trim();
        }
        public void ZetVoornaam(string voornaam)
        {
            if (string.IsNullOrWhiteSpace(voornaam)) throw new ReservatieExeption("ZetVoornaam - leeg");
            Voornaam = voornaam.Trim();
        }
        public void ZetAchternaam(string achternaam)
        {
            if (string.IsNullOrWhiteSpace(achternaam)) throw new ReservatieExeption("ZetAchternaam - leeg");
            Achternaam = achternaam.Trim();
        }
        public void ZetDatum(DateTime datum)// TODO controle op " && DateTime.Now.Hour >= uur" => om 9u geen 8-9 slot
        {
            if (datum < DateTime.Today || datum > DateTime.Today.AddDays(7)) throw new ReservatieExeption("ZetDatum - geen reservatie mogelijk op deze datum");
            Datum = datum;
        }
        public void ZetSlotEnToestel(Dictionary<string, int> gereserveerd)
        {
            foreach (var g in gereserveerd)
            {
                if (gereserveerdeSlotenEnToestellen.Keys.Count() + 1 > 4) throw new ReservatieExeption("ZetSlotEnToestel - te veel sloten");
                if (string.IsNullOrWhiteSpace(g.Key)) throw new ReservatieExeption("ZetSlotEnToestel - Slot leeg");
                if (g.Value < 0) throw new ReservatieExeption("ZetSlotEnToestel - ongeldig toestel");
                if (gereserveerdeSlotenEnToestellen.ContainsKey(g.Key)) throw new ReservatieExeption("ZetSlotEnToestel - je heb al een reservatie op dit moment");

                //var v = gereserveerdeSlotIDsEnToestellen.GroupBy(t => t.Value).OrderByDescending(t => t.Count()).Select(t => t.First());

                //for (int i = gereserveerdeSlotenEnToestellen.Count(); i > 1; i--)
                //{
                //    if ((SlotIDs[i] == SlotIDs[i - 1] + 1) && (SlotIDs[i - 1] == SlotIDs[i - 2] + 1))
                //    {
                //        if (toestellen[i] == toestellen[i - 1] && toestellen[i] == toestellen[i - 2])
                //            throw new ReservatieExeption("ZetSlotEnToestel - meer dan 2 reservaties met hetzelfde toestel op rij");
                //    }
                //}
                gereserveerdeSlotenEnToestellen.Add(g.Key, g.Value);

            }
        }
        public override string ToString()
        {
            string s = $"{ReservatieNummer},{KlantNummer},{Emailadres},{Voornaam},{Achternaam},{Datum}";
            //foreach (var v in gereserveerdeSlotenEnToestellen)
            //{
            //    s += $" || {v.Key} , {v.Key}";
            //}
            return s;
        }
    }
}
