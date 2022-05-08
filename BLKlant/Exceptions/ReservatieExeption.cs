using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Exceptions
{
    public class ReservatieExeption : Exception
    {
        public ReservatieExeption(string message) : base(message)
        {
        }

        public ReservatieExeption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
