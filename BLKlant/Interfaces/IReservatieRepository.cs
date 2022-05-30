using BLKlant.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Interfaces
{
    public interface IReservatieRepository
    {
        public Reservatie SelecteerReservatie(int klantnummer, DateTime datum);
        public bool BestaatReservatie(int klantnummer, DateTime datum);
        public Reservatie SchrijfReservatieInDB(Reservatie r);

        public bool BestaatReservatieMetToestel(int toestel);
        public List<Reservatie> SelecteerReservatiesOpKlantNR(int klantnummer);

    }
}
