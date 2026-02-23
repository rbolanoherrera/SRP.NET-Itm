using System;

namespace SRP.Bad.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdTipoProducto { get; set; }
        public string ValorCompra { get; set; }
        public string ValorVenta { get; set; }
        public decimal Iva { get; set; }
        public byte[]? Imagen { get; set; }
        public int IdUnidadMedidaBase { get; set; }
        public int IdUnidadMedidaCompra { get; set; }
        public int IdUnidadMedidaVenta { get; set; }

        /// <summary>
        /// Relación que existe en la Unidad de Mediad de Compra y la Unidad de Mediad de Venta. 
        /// Sera el valor que se descontara del Inventario al realizar una venta
        /// </summary>
        public decimal CantEquivalente { get; set; }
        public string CodigoBarras { get; set; }
        public int ProveedorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public int Estado { get; set; }
    }
}