using BLKlant.Domein;
using BLKlant.Exceptions;
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
        private IGereserveerdeSlotenRepository GSRepo;
        private ISlotsRepository slotRepo;
        public List<GereserveerdSlot> gs;

        public GereserveerdeSlotsManager(IGereserveerdeSlotenRepository gSRepo, ISlotsRepository slotRepo)
        {
            GSRepo = gSRepo;
            this.slotRepo = slotRepo;
        }

        public List<GereserveerdSlot> SelecteerGereserveerdeSloten (int reservatienummer)
        {
            gs = GSRepo.GeefGereserveerdeSloten(reservatienummer);
            return gs ;
        }

        public List<string> BeschikbareSloten()
        {
            List<string> alleSloten = new List<string>();
            List<string> bezet = new List<string>();
            List<string> beschikbaar = new List<string>();

            alleSloten = slotRepo.GetAlleSloten();
            
            foreach (GereserveerdSlot GS in gs)
            {
                bezet.Add(GS.Slot);
            }
            var a = alleSloten.Except(bezet);
            foreach (string s in a)
            {
                beschikbaar.Add(s);
            }
            return beschikbaar;
        }
        private bool OpeenvolgendeSloten(Reservatie r)
        {




            Dictionary<int, int> a = new Dictionary<int, int>();
            a = GSRepo.GetSlotIdEnToestel(r);

            List<int> IDs = new List<int>();
            
            foreach(int i in a.Keys)
            {
                IDs.Add(i);
            }

                bool TeveelSloten = false;
            for (int i = IDs.Count; i > 1; i--)
            {
                if ((IDs[i] == IDs[i - 1] + 1) && (IDs[i - 1] == IDs[i - 2] + 1))
                {
                    if(a[i] == a[i - 1] && a[i] == a[i - 2])
                    TeveelSloten = true;
                }
            }
            return TeveelSloten;
        }
        public void VoegSlotToeAanReservatie(Reservatie r)
        {
            //if (r.gereserveerdeSlotenEnToestellen.Count() > 2)
            //{
            //    if (OpeenvolgendeSloten(r) == true)
            //    {
            //        throw new GereserveerdeSlotsManagerException("maximaal 2 opeenvolgende sloten met hetzelfde toestel");
            //    }
            //}


            if (GSRepo.GetAantalGereserveerdeSloten(r) < 4)
                GSRepo.SchrijfGereserveerdeSlotenInDB(r);
            else throw new GereserveerdeSlotsManagerException("Maximaal aantal sloten per dag berijkt");
        }
    }
}
