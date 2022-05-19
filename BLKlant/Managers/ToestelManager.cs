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
    public class ToestelManager
    {
        private IToestelRepository toestelRepo;
        private IReservatieRepository resRepo;

        public ToestelManager(IToestelRepository toestelRepo, IReservatieRepository resRepo)
        {
            this.toestelRepo = toestelRepo;
            this.resRepo = resRepo;
        }

        public void VeranderStatus(Toestel t)
        {
            toestelRepo.UpdateStatus(t);
        }
        public Toestel VoegToestelToe(string toesteltype)
        {
            return toestelRepo.SchrijfNieuwToestelInDB(toesteltype);
        }
        public void VerwijderToestel(List<Toestel> toestellen)
        {
            Toestel toestelMetReservatie = null;
            foreach(Toestel t in toestellen)
            {
                if (resRepo.BestaatReservatieMetToestel(t.ToestelNummer) == true)
                {

                    toestelMetReservatie = t;
                    break;
                }
            }
            if (toestelMetReservatie == null)
                toestelRepo.VerwijderToestelUitDB(toestellen);
            else throw new ToestelManagerException($"Toestel {toestelMetReservatie.ToestelNummer} heeft nog openstaande reservaties");
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
        public List<string> GetToestelTypes()
        {
            return toestelRepo.GetToestelTypes();
        }
        public List<Toestel> GetAlleToestellen()
        {
            return toestelRepo.GetBeschikbaarEnBuitenGebruik();
        }
    }
}
