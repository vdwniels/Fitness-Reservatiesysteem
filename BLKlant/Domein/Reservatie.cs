using System;
using System.Collections.Generic;
using System.Linq;
using BLKlant.Exceptions;
namespace BLKlant.Domein
{
    public class Reservatie
    {
        public SortedDictionary<string, string> gereserveerdeSlotenEnToestellen = new SortedDictionary<string, string>();
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
        public void ZetSlotEnToestel(string slot, string toestel)
        {
            if (gereserveerdeSlotenEnToestellen.Keys.Count()+1 > 4) throw new ReservatieExeption("ZetSlotEnToestel - te veel sloten");
            if (string.IsNullOrWhiteSpace(slot)) throw new ReservatieExeption("ZetSlotEnToestel - Slot leeg");
            if (string.IsNullOrWhiteSpace(toestel)) throw new ReservatieExeption("ZetSlotEnToestel - Toestel leeg");
            if (gereserveerdeSlotenEnToestellen.ContainsKey(slot)) throw new ReservatieExeption("ZetSlotEnToestel - je heb al een reservatie op dit moment");

            
            //List<int> beginuren = new List<int>();
            //foreach(string a in gereserveerdeSlotenEnToestellen.Keys)
            //{
            //    beginuren.Add(int.Parse(a.ToString()));
            //}

            //List<string> toestellen = new List<string>();
            //foreach(string a in gereserveerdeSlotenEnToestellen.Values)
            //{
            //    toestellen.Add(a);
            //}
            //int opeenvolgend = 0;
            //for (int i = beginuren.Count()-1; i > 0; i--)
            //{
            //    if (beginuren[i] == beginuren[i - 1] + 1)
            //    {
            //        opeenvolgend++;
            //        if (opeenvolgend > 2)
            //        {
            //            if(toestellen[i] == toestellen[i-1] && toestellen[i] == toestellen[i - 2]) { 
            //                throw new ReservatieExeption("ZetSlotEnToestel - meer dan 2 opeenvolgende slots met eenzelfde toestel");
            //            }
            //        }
            //    }
            //}
            gereserveerdeSlotenEnToestellen.Add(slot.Trim(), toestel.Trim());
        }
    }
}
