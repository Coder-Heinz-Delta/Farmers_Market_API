using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmers_Market_API.Enums;
using Farmers_Market_API.Models;

namespace Farmers_Market_API.Repositories
{
    public interface IProduceRepository
    {
        List<ProduceListing> GetAll();
        ProduceListing GetById(int id);
        List<ProduceListing> GetAvailable();
        List<ProduceListing> GetByCategory(Category category);
        void Add(ProduceListing listing);
    }
}