using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Productos.Inventario
{
    public class UEPSModel : InventarioModel
    {
        //CalculoInventario[] inventarios = new CalculoInventario[0];
        ProductoModel m;
        decimal PrecioV;
        public decimal UEPSSalida(int ventas,DateTime dt)
        {
            //inventarios = new CalculoInventario[inventario.Length];
           // Array.Copy(inventario,inventarios,inventario.Length);
            int i = inventario.Length-1;
            while (ventas > 0)
            {
                if (inventario[i].Existencia != 0)
                {
                    if (ventas > inventario[i].Existencia)
                    {
                        PrecioV = inventario[i].Precio;
                        inventario[i].Existencia = ventas - inventario[i].Existencia;
                        ventas = inventario[i].Existencia;
                        CalculoInventario model = new CalculoInventario(m.GetLastProductoId() + 1, inventario[i].Existencia, PrecioV, Especie.Salida,dt);
                        Add(model);
                        i = i - 1;
                    }
                    else
                    {
                        PrecioV = inventario[i].Precio;
                        inventario[i].Existencia = inventario[i].Existencia-ventas;
                        CalculoInventario model = new CalculoInventario(m.GetLastProductoId() + 1, ventas, PrecioV, Especie.Salida,dt);
                        Add(model);
                        ventas = 0;
                    }
                }
                else
                {
                    break;
                }
            }
            return PrecioV;
        }
        
    }
}
