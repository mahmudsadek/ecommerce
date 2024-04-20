using ecommerce.Models;

namespace ecommerce.Repository
{
    public class ShipmentRepository : Repository<Shipment> , IShipmentRepository
    {
        private readonly Context context;
     //   private readonly IOrderRepository orderRepository;

        public ShipmentRepository(Context context) : base(context)
        {
            this.context = context;
         //   orderRepository = _orderRepository;
        }


        // saeed : get shipment details
        //public List<Product> GetShipmentDetails(Shipment shipment) 
        //{
        //    var query = from shi
        //}
    }
}
