using Farmers_Market_API.Interfaces;
using Farmers_Market_API.Models;

namespace Farmers_Market_API.Repositories
{
    public class BuyerRepository : IRepository<Buyer>
    {
        private List<Buyer> _buyers = new();

        public List<Buyer> GetAll() => _buyers;

        public Buyer GetById(int id) => 
            _buyers.FirstOrDefault(b => b.BuyerId == id) ?? throw new Exception("Buyer not found");

        public void Add(Buyer entity)
        {
            entity.BuyerId = _buyers.Any() ? _buyers.Max(b => b.BuyerId) + 1 : 1;
            _buyers.Add(entity);
        }

        public void Delete(int id) => _buyers.RemoveAll(b => b.BuyerId == id);
    }
}