using BLKlant.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Interfaces
{
    public interface IGereserveerdeSlotenRepository
    {
        public void SchrijfGereserveerdeSlotenInDB(Reservatie r);
        public List<GereserveerdSlot> GeefGereserveerdeSloten(int reservatienummer);
        public int GetAantalGereserveerdeSloten(Reservatie r);

        public Dictionary<int, int> GetSlotIdEnToestel(Reservatie r);

    }
}
