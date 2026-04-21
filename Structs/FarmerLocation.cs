using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmers_Market_API.Enums;

namespace Farmers_Market_API.Structs
{
    public struct FarmerLocation
    {
        public string FarmName { get; set; }
        public Province Province { get; set; }
        public string Town { get; set; }
    }

}