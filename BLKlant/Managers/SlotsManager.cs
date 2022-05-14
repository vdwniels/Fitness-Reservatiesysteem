using BLKlant.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Managers
{
    public class SlotsManager
    {
        private ISlotsRepository repo;

        public SlotsManager(ISlotsRepository repo)
        {
            this.repo = repo;
        }
        //public Dictionary<int,int> GetSloten()
        //{
        //    Dictionary<int, int> slots;
        //    slots = repo.GetSlots();
        //}
    }
}
