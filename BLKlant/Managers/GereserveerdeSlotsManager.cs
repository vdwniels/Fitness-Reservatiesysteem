using BLKlant.Domein;
using BLKlant.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Managers
{
    public class GereserveerdeSlotsManager
    {
        private IGereserveerdeSlotenRepository slotRepo;

        public GereserveerdeSlotsManager(IGereserveerdeSlotenRepository slotRepo)
        {
            this.slotRepo = slotRepo;
        }

        public IReadOnlyList<GereserveerdSlot> SelecteerGereserveerdeSloten (int reservatienummer)
        {
            return slotRepo.GeefGereserveerdeSloten(reservatienummer);
        }
    }
}
