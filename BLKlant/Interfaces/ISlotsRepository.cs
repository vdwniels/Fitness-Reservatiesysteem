using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Interfaces
{
    public interface ISlotsRepository
    {
        public Dictionary<int, int> GetSlots(Dictionary<string, int> gereserveerdeSloten);

    }
}
