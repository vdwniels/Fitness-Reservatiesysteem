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
        public List<Toestel> BeschikbareToestellen(DateTime datum, string slot)
        {
            List<Toestel> alleToestellen = new List<Toestel>();
            List<Toestel> bezet = new List<Toestel>();
            List<Toestel> beschikbaar = new List<Toestel>();

            alleToestellen = toestelRepo.GetAlleToestellen();
            bezet = toestelRepo.GetBezetteToestellen(datum, slot);
            
            var a = alleToestellen.Except(bezet);
            foreach (Toestel t in a)
            {
                beschikbaar.Add(t);
            }
            return beschikbaar;
        }

    }
}
