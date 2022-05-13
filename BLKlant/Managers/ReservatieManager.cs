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
            Reservatie r = null;
            if (!resRepo.BestaatReservatie(klantNummer, datum))
            {
                r = new Reservatie(klantNummer, emailadres, voornaam, achternaam, datum);
                resRepo.SchrijfReservatieInDB(r);
            }
            else
            {
                r = resRepo.SelecteerReservatie(klantNummer, datum);
            }
                r.ZetSlotEnToestel(gereserveerdeSloten);
                slotRepo.SchrijfGereserveerdeSlotenInDB(r);
                return r;
        }
        public IReadOnlyList<Reservatie> SelecteerReservatiesOpKlantnummer(int klantnummer)
        {
            return resRepo.SelecteerReservatiesOpKlantNR(klantnummer);
        }
    }
}
