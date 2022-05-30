using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Domein
{
    public class Toestel
    {

        public Toestel(int toestelNummer, string toestelType, string status = "Beschikbaar")
        {
            ToestelNummer = toestelNummer;
            ToestelType = toestelType;
            Status = status;
        }

        public int ToestelNummer { get; set; }
        public string ToestelType { get; set; }
        public string Status { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Toestel toestel &&
                   ToestelNummer == toestel.ToestelNummer &&
                   ToestelType == toestel.ToestelType &&
                   Status == toestel.Status;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ToestelNummer, ToestelType, Status);
        }

        public override string ToString()
        {
            return $"Toestel {ToestelNummer} ({ToestelType}) - {Status}";
        }
    }
}
