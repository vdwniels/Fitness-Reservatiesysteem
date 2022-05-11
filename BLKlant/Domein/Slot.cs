using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Domein
{
    public class Slot
    {
        public Slot(string slots, int slotID)
        {
            Slots = slots;
            SlotID = slotID;
        }

        public string Slots { get; set; }
        public int SlotID { get; set; }
    }
}
