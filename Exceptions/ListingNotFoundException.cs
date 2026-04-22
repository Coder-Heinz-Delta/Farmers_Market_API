using System;

namespace Farmers_Market_API.Exceptions
{
    public class ListingNotFoundException : Exception
    {
        public ListingNotFoundException(int id) 
            : base($"Produce listing with ID {id} was not found.")
        {
        }
    }
}