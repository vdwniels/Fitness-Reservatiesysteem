using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLKlant.Exceptions
{
    public class SlotsRepoADOException : Exception
    {
        public SlotsRepoADOException(string message) : base(message)
        {
        }

        public SlotsRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
