using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Domein
{
    public class GereserveerdSlot
    {
        public GereserveerdSlot(int toestel, string slot)
        {
            Toestel = toestel;
            Slot = slot;
        }

        public GereserveerdSlot(int reservatienummer, int toestel, string slot)
        {
            Reservatienummer = reservatienummer;
            Toestel = toestel;
            Slot = slot;
        }

        public int Reservatienummer { get; set; }
        public int Toestel { get; set; }
        public string Slot { get; set; }
        public override string ToString()
        {
            return $"Toestel nr: {Toestel},Slot: {Slot}";
        }
    }
}
