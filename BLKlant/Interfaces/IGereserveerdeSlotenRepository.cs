using BLKlant.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Interfaces
{
    public interface IGereserveerdeSlotenRepository
    {
        public void SchrijfGereserveerdeSlotenInDB(Reservatie r);

    }
}
