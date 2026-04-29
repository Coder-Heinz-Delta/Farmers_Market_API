using System.Collections.Generic;
using System.Linq;
using Farmers_Market_API.Interfaces;
using Farmers_Market_API.Models;

namespace Farmers_Market_API.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private List<Order> _orders = new List<Order>();

        public List<Order> GetAll() => _orders;

        public Order GetById(int id)
        {
            return _orders.FirstOrDefault(o => o.OrderId == id) 
                   ?? throw new System.Exception($"Order {id} not found.");
        }

        public void Add(Order order)
        {
            // Auto-increment logic
            order.OrderId = _orders.Any() ? _orders.Max(o => o.OrderId) + 1 : 1;
            _orders.Add(order);
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            _orders.Remove(order);
        }

        // Specific logic for Order reporting
        public double GetTotalRevenue()
        {
            return _orders.Sum(o => o.TotalPrice);
        }
    }
}