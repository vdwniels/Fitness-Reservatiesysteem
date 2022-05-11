using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLKlant.Domein;
using BLKlant.Interfaces;

namespace BLKlant.Managers
{
    public class ReservatieManager
    {
        private IReservatieRepository resRepo;
        private IGereserveerdeSlotenRepository slotRepo;

        public ReservatieManager(IReservatieRepository resRepo, IGereserveerdeSlotenRepository slotRepo)
        {
            this.resRepo = resRepo;
            this.slotRepo = slotRepo;
        }

        public Reservatie MaakReservatie(int klantNummer, string emailadres, string voornaam, string achternaam, DateTime datum, Dictionary<string, int> gereserveerdeSloten)
        {

            if (!resRepo.BestaatReservatie(klantNummer, datum))
            {
                Reservatie r = new Reservatie(klantNummer, emailadres, voornaam, achternaam, datum);
               // Dictionary<int, int> SlotIDsEnToestellen = slotRepo.GetSlots(gereserveerdeSloten);
                r.ZetSlotEnToestel(gereserveerdeSloten);
                resRepo.SchrijfReservatieInDB(r);
                slotRepo.SchrijfGereserveerdeSlotenInDB(r);
                return r;
            }
            else
            {
                Reservatie r = resRepo.SelecteerReservatie(klantNummer, datum);
                r.ZetSlotEnToestel(gereserveerdeSloten);
            }
            return null;
        }
    }
}
