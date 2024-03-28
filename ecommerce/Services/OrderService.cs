using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public List<Order> GetAll(string include = null)
        {
            return orderRepository.GetAll(include);  // the base function handles the null value don't worry
        }

        public Order Get(int id)
        {
            return orderRepository.Get(id);
        }

        public List<Order> Get(Func<Order, bool> where)
        {
            return orderRepository.Get(where);
        }

        public void Insert(Order order)
        {
            ///TODO : continue from here
            /// question : do I have to create an instance ? and in the service or repo ? 
           
            // mapping the refernce values in a new instance
            Order newOrder = new Order();

            newOrder.Id = order.Id;
            newOrder.User = order.User;
            newOrder.OrderDate = order.OrderDate;
            newOrder.OrderItems = order.OrderItems;

            newOrder.ApplicationUserId = order.ApplicationUserId;
            newOrder.User = order.User;

            newOrder.ShipmentId = order.ShipmentId;
            newOrder.Shipment = order.Shipment;

            orderRepository.Insert(newOrder);
        }

        public void Update(Order updatedOrder)
        {
            Order order = Get(updatedOrder.Id);

            //updatedOrder.Id = order.Id;
            order.User = updatedOrder.User;
            order.OrderDate = updatedOrder.OrderDate;
            order.OrderItems = updatedOrder.OrderItems;

            order.ApplicationUserId = updatedOrder.ApplicationUserId;
            order.User = updatedOrder.User;

            order.ShipmentId = updatedOrder.ShipmentId;
            order.Shipment = updatedOrder.Shipment;

            orderRepository.Update(order);
        }

        public void Delete(Order order)
        {
            orderRepository.Delete(order);
        }

        public void Save()
        {
            orderRepository.Save();
        }

    }
}
