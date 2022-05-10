using BLKlant.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Interfaces
{
    public interface IKlantRepository
    {
        public void SchrijfKlantInDB(List<Klant> k);
        public List<Klant> LeesKlanten();

    }
}
