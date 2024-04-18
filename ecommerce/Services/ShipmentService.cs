using ecommerce.Models;
using ecommerce.Repository;

namespace ecommerce.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository shipmentRepository;

        public ShipmentService(IShipmentRepository shipmentRepository)
        {
            this.shipmentRepository = shipmentRepository;
        }

        //*************************************************

        public List<Shipment> GetAll(string? include = null)
        {
            return shipmentRepository.GetAll(include); // the generic base function handels the null
        }


        public Shipment Get(int id)
        {
            return shipmentRepository.Get(id);
        }


        public List<Shipment> Get(Func<Shipment, bool> where)
        {
            return shipmentRepository.Get(where);
        }


        public void Insert(Shipment item)
        {
            shipmentRepository.Insert(item);
            shipmentRepository.Save();
        }


        public void Update(Shipment item)
        {
            shipmentRepository.Update(item);
        }


        public void Delete(Shipment item)
        {
            shipmentRepository.Delete(item);
        }   

        public void Save()
        {
            shipmentRepository.Save();
        }
    }
}
