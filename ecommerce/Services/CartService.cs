using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        // **************************************************

        public List<Cart> GetAll(string include = null)
        {
            return cartRepository.GetAll(include);  // the base function handles the null with if condition
        }

        public Cart Get(int id)
        {
            return cartRepository.Get(id);
        }

        public List<Cart> Get(Func<Cart, bool> where)
        {
            return cartRepository.Get(where);
        }

        public void Insert(Cart cart)
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

            cartRepository.Insert(cart);
        }

        public void Update(Cart updatedCart)
        {
            ///TODO : possible bug place (NULL Refernce Exception)
            Cart cart = Get(updatedCart.Id);

            #region commented mapping 
            //order.User = updatedOrder.User;
            //order.OrderDate = updatedOrder.OrderDate;
            //order.OrderItems = updatedOrder.OrderItems;

            //order.ApplicationUserId = updatedOrder.ApplicationUserId;
            //order.User = updatedOrder.User;

            //order.ShipmentId = updatedOrder.ShipmentId;
            //order.Shipment = updatedOrder.Shipment; 
            #endregion

            cartRepository.Update(cart);
        }

        public void Delete(Cart cart)
        {
            cartRepository.Delete(cart);
        }

        public void Save()
        {
            cartRepository.Save();
        }
    }
}
