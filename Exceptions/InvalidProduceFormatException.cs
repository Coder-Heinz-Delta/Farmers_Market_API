using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmers_Market_API.Exceptions
{
    public class InvalidProduceFormatException : Exception
    {
        public InvalidProduceFormatException(string message) : base(message)
        {
        }
    }
}