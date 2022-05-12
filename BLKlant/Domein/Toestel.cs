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
    }
}
