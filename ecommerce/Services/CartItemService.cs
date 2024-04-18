using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository cartItemRepository;

        public CartItemService(ICartItemRepository cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }

        //***************************************************

        public List<CartItem> GetAll(string include = null)
        {
            return cartItemRepository.GetAll(include);  // the base function handles the null with if condition
        }

        public CartItem Get(int id)
        {
            return cartItemRepository.Get(id);
        }

        public List<CartItem> Get(Func<CartItem, bool> where)
        {
            return cartItemRepository.Get(where);
        }

        public void Insert(CartItem cartItem)
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

            cartItemRepository.Insert(cartItem);
        }

        public void Update(CartItem updatedcartItem)
        {
            ///TODO : possible bug place (NULL Refernce Exception)
            CartItem cartItem = Get(updatedcartItem.Id);

            #region commented mapping 
            //order.User = updatedOrder.User;
            //order.OrderDate = updatedOrder.OrderDate;
            //order.OrderItems = updatedOrder.OrderItems;

            //order.ApplicationUserId = updatedOrder.ApplicationUserId;
            //order.User = updatedOrder.User;

            //order.ShipmentId = updatedOrder.ShipmentId;
            //order.Shipment = updatedOrder.Shipment; 
            #endregion

            cartItemRepository.Update(cartItem);
        }

        public void Delete(CartItem cartItem)
        {
            cartItemRepository.Delete(cartItem);
        }

        public void Save()
        {
            cartItemRepository.Save();
        }

    }
}
