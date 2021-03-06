using BLKlant.Domein;
using BLKlant.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Managers
{
    public class KlantManager
    {
        private IKlantRepository repo;
        public KlantManager(IKlantRepository repo)
        {
            this.repo = repo;
        }
        public Klant SelecteerKlant (int? klantnummer, string? email)
        {
            Klant k= repo.SelecteerKlant(klantnummer, email);
            return k;
        }
    }
}
