using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IProductoModel : IModel<Producto>
    {
        string GetProductosAsJson();
        Producto[] GetProductosOrderByPrecio();
        int GetLastProductoId();
        Producto[] GetProductosByFechaVencimiento(DateTime dt);
        Producto[] GetProductosByRangoPrecio(decimal start, decimal end);
        Producto[] GetProductosByUnidadMedida(UnidadMedida um);
        string Mostrar(Producto[] inventario);


    }
}
