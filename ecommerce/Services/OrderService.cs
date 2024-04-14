using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        //***************************************************

        public List<Order> GetAll(string include = null)
        {
            return orderRepository.GetAll(include);  // the base function handles the null with if condition
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
            ///TODO : possible bug place (NULL Refernce Exception)
            /// question : do I have to create an instance ? and in the service or repo ?

            #region commented mapping the refernce values in a new instance
            // mapping the refernce values in a new instance
            //Order newOrder = new Order();

            //newOrder.Id = order.Id;
            //newOrder.User = order.User;
            //newOrder.OrderDate = order.OrderDate;
            //newOrder.OrderItems = order.OrderItems;

            //newOrder.ApplicationUserId = order.ApplicationUserId;
            //newOrder.User = order.User;

            //newOrder.ShipmentId = order.ShipmentId;
            //newOrder.Shipment = order.Shipment; 
            #endregion

            orderRepository.Insert(order);
            orderRepository.Save();
        }

        public void Update(Order updatedOrder)
        {
            ///TODO : possible bug place (NULL Refernce Exception)
            Order order = Get(updatedOrder.Id);

            #region commented mapping 
            //order.User = updatedOrder.User;
            //order.OrderDate = updatedOrder.OrderDate;
            //order.OrderItems = updatedOrder.OrderItems;

            //order.ApplicationUserId = updatedOrder.ApplicationUserId;
            //order.User = updatedOrder.User;

            //order.ShipmentId = updatedOrder.ShipmentId;
            //order.Shipment = updatedOrder.Shipment; 
            #endregion

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

        public Order InsertOrder(Order order)
        {
            Order o = orderRepository.InsertOrder(order);
            orderRepository.Save();
            return o;
        }
    }
}
