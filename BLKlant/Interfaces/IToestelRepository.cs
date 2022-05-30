using BLKlant.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Interfaces
{
    public interface IToestelRepository
    {
        public void UpdateStatus(List<Toestel> toestellen);
        public Toestel SchrijfNieuwToestelInDB(string toesteltype);
        public void VerwijderToestelUitDB(List<Toestel> toestellen);
        public List<Toestel> GetAlleToestellen();
        public List<Toestel> GetBezetteToestellen(DateTime datum, string slot);
        public List<string> GetToestelTypes();
        public List<Toestel> GetBeschikbaarEnBuitenGebruik();

    }
}
