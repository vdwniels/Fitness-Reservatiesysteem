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
        public void UpdateStatus(Toestel t);
        public Toestel SchrijfNieuwToestelInDB(string toesteltype);
        public void VerwijderToestelUitDB(int id);

    }
}
