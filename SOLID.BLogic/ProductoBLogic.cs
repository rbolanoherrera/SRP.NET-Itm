using SOLID.Data;
using SOLID.Entities;

namespace SOLID.BLogic
{
    public class ProductoBLogic : IProductoBLogic
    {
        private readonly IProductoData prodData;

        public ProductoBLogic(IProductoData productoData)
        {
            this.prodData = productoData;
        }

        public Result<int> Create(Producto prod)
        {
            return prodData.Create(prod);
        }
    }
}