using BLKlant.Domein;
using BLKlant.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Managers
{
    public class ToestelManager
    {
        private IToestelRepository toestelRepo;
        private IReservatieRepository resRepo;

        public ToestelManager(IToestelRepository toestelRepo, IReservatieRepository resRepo)
        {
            this.toestelRepo = toestelRepo;
            this.resRepo = resRepo;
        }

        public void VeranderStatus (Toestel t)
        {
            toestelRepo.UpdateStatus(t);
        }
        public void VoegToestelToe(string toesteltype)
        {
            toestelRepo.SchrijfNieuwToestelInDB(toesteltype);
        }
        public void VerwijderToestel(int id)
        {
            if (!resRepo.BestaatReservatieMetToestel(id))
                toestelRepo.VerwijderToestelUitDB(id);
        }
    }
}
