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
        private IToestelRepository repo;
        public void VeranderStatus (Toestel t)
        {
            repo.UpdateStatus(t);
        }
    }
}
