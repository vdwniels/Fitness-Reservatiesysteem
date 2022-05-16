using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLKlant.Exceptions
{
    public class GereserveerdeSlotsManagerException : Exception
    {
        public GereserveerdeSlotsManagerException(string message) : base(message)
        {
        }

        public GereserveerdeSlotsManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
