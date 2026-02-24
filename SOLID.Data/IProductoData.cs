using SOLID.Entities;

namespace SOLID.Data
{
    public interface IProductoData
    {
        Result<int> Create(Producto prod);
        Result<IEnumerable<Producto>> GetAll();
    }
}