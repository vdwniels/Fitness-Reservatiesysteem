using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Interfaces
{
    public interface ISlotsRepository
    {
        public Dictionary<string, int> GetSlots(List<string> gereserveerdeSloten);
        public List<string> GetAlleSloten();

    }
}
