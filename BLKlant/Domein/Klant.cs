using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Domein
{
    public class Klant
    {
        public Klant(string voornaam, string achternaam, string email, string adres, DateTime geboorteDatum, string interesse, string klantType)
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            Email = email;
            Adres = adres;
            GeboorteDatum = geboorteDatum;
            Interesse = interesse;
            KlantType = klantType;
        }

        public Klant(int klantNummer, string voornaam, string achternaam, string email, string adres, DateTime geboorteDatum, string interesse, string klantType)
        {
            KlantNummer = klantNummer;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Email = email;
            Adres = adres;
            GeboorteDatum = geboorteDatum;
            Interesse = interesse;
            KlantType = klantType;
        }

        public int KlantNummer { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string? Interesse { get; set; }
        public string KlantType { get; set; }
        public override string ToString()
        {
            return $"{Voornaam} | {Achternaam} | {Email} | {Adres} | {GeboorteDatum} | {Interesse} | {KlantType}";
        }
    }
}
