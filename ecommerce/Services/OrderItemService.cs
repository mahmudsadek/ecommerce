using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }

        //***************************************************

        public List<OrderItem> GetAll(string include = null)
        {
            return orderItemRepository.GetAll(include);
        }


        public OrderItem Get(int id)
        {
            return orderItemRepository.Get(id); // who ever calls should the null probability
        }


        public List<OrderItem> Get(Func<OrderItem, bool> where)
        {
            return orderItemRepository.Get(where);
        }


        public void Insert(OrderItem item)
        {
            orderItemRepository.Insert(item);
            orderItemRepository.Save();
        }


        public void Update(OrderItem item)
        {
            orderItemRepository.Update(item);
        }


        public void Delete(OrderItem item)
        {
            orderItemRepository.Delete(item);
        }


        public void Save()
        {
            orderItemRepository.Save();
        }

        public List<OrderItem> Get(Func<Order, bool> where)
        {
            throw new NotImplementedException();
        }


    }
}
