using ecommerce.Models;

namespace ecommerce.Repository
{
    public class ShipmentRepository : Repository<Shipment> , IShipmentRepository
    {
        private readonly Context context;

        public ShipmentRepository(Context context) : base(context)
        {
            this.context = context;
        }
    }
}
