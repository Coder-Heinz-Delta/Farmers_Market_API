using Farmers_Market_API.Interfaces;
using Farmers_Market_API.Models;

namespace Farmers_Market_API.Repositories
{
    public class FarmerRepository : IRepository<Farmer>
    {
        private List<Farmer> _farmers = new();

        public List<Farmer> GetAll() => _farmers;

        public Farmer GetById(int id)
        {
            // Assuming your Farmer model has an Id property
            return _farmers.FirstOrDefault(f => f.FullName.GetHashCode() == id); 
        }

        public void Add(Farmer entity)
        {
            _farmers.Add(entity);
        }

        public void Delete(int id)
        {
            var farmer = GetById(id);
            _farmers.Remove(farmer);
        }
    }
}