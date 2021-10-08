using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Productos.Inventario
{
    public class InventarioModel
    {
        public CalculoInventario[] inventario;
        public void Add(CalculoInventario c)
        {
            Add(c, ref inventario);
        }
        private void Add(CalculoInventario p, ref CalculoInventario[] pds)
        {
            if (pds == null)
            {
                pds = new CalculoInventario[1];
                pds[pds.Length - 1] = p;
                return;
            }

            CalculoInventario[] tmp = new CalculoInventario[pds.Length + 1];
            Array.Copy(pds, tmp, pds.Length);
            tmp[tmp.Length - 1] = p;
            pds = tmp;
        }

    }
}
