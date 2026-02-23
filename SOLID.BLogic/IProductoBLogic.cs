using SOLID.Entities;

namespace SOLID.BLogic
{
    public interface IProductoBLogic
    {
        Result<int> Create(Producto prod);
    }
}