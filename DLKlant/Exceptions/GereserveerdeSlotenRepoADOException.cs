using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLKlant.Exceptions
{
    public class GereserveerdeSlotenRepoADOException : Exception
    {
        public GereserveerdeSlotenRepoADOException(string message) : base(message)
        {
        }

        public GereserveerdeSlotenRepoADOException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
