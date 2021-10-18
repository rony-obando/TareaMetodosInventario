using AppCore.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Productos
{
    public class ProductoModel:IProductoModel
    {
        private Producto[] productos;

        #region CRUD
        public void Add(Producto p)
        {
            Add(p, ref productos);
        }

        public int Update(Producto p)
        {
            if(p == null)
            {
                throw new ArgumentException("El producto no puede ser null.");
            }

            int index = GetIndexById(p.ID);
            if(index < 0)
            {
                throw new Exception($"El producto con id {p.ID} no se encuentra.");
            }

            productos[index] = p;
            return index;
        }

        public bool Delete(Producto p)
        {
            if (p == null)
            {
                throw new ArgumentException("El producto no puede ser null.");
            }

            int index = GetIndexById(p.ID);
            if (index < 0)
            {
                throw new Exception($"El producto con id {p.ID} no se encuentra.");
            }

            if(index != productos.Length - 1)
            {
                productos[index] = productos[productos.Length - 1];
            }

            Producto[] tmp = new Producto[productos.Length - 1];
            Array.Copy(productos, tmp, tmp.Length);
            productos = tmp;

            return productos.Length == tmp.Length;
        }
        public Producto[] GetAll()
        {
            return productos;
        }

        #endregion

        #region Queries
        public Producto GetProductoById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException($"El Id: {id} no es valido.");
            }

            int index = GetIndexById(id);            

            return index <= 0 ? null : productos[index];
        }

        public Producto[] GetProductosByUnidadMedida(UnidadMedida um)
        {
            Producto[] tmp = null;
            if (productos == null)
            {
                return tmp;
            }

            foreach (Producto p in productos)
            {
                if(p.UnidadMedida == um)
                {
                    Add(p, ref tmp);
                }
            }

            return tmp;
        }

        public Producto[] GetProductosByFechaVencimiento(DateTime dt)
        {
            Producto[] tmp = null;
            if(productos == null)
            {
                return tmp;
            }

            foreach(Producto p in productos)
            {
                if(p.FechaVencimiento.CompareTo(dt) <= 0)
                {
                    Add(p, ref tmp);
                }
            }

            return tmp;
        }

        public Producto[] GetProductosByRangoPrecio(decimal start, decimal end)
        {
            Producto[] tmp = null;
            if(productos == null)
            {
                return tmp;
            }

            foreach(Producto p in productos)
            {
                if(p.Precio >= start && p.Precio <= end)
                {
                    Add(p, ref tmp);
                }
            }

            return tmp;
        }

        public string GetProductosAsJson()
        {
            return JsonConvert.SerializeObject(productos);
        }

        public Producto[] GetProductosOrderByPrecio()
        {
            Array.Sort(productos, new Producto.ProductoOrderByPrecio());
            return productos;
        }

        public int GetLastProductoId()
        {
            int a = int.MinValue;
            if (productos == null)
            {
                return 0;
            }
            else
            {
                foreach(Producto p in productos)
                {
                    if (p.ID>a)
                    {
                        a = p.ID;
                    }
                }
            }
            return a;
        }
        #endregion

        #region Private Method
        private void Add(Producto p, ref Producto[] pds)
        {
            if(pds == null)
            {
                pds = new Producto[1];
                pds[pds.Length - 1] = p;
                return;
            }

            Producto[] tmp = new Producto[pds.Length + 1];
            Array.Copy(pds, tmp, pds.Length);
            tmp[tmp.Length - 1] = p;
            pds = tmp;
        }

        private int GetIndexById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

            int index = int.MinValue, i = 0;
            if(productos == null)
            {
                return index;
            }

            foreach (Producto p in productos)
            {
                if(p.ID == id)
                {
                    index = i;
                    break;
                }
                i++;
            }

            return index;
        }

        public void Create(Producto t)
        {
            Add(t, ref productos);
        }

        public Producto[] FindAll()
        {
            throw new NotImplementedException();
        }
        public string Mostrar(Producto[] inventario)
        {
            string control = "";
            for (int i = inventario.Length - 1; i >= 0; i--)
            {
               control = $@"Fecha: {inventario[i].FechaVencimiento}, Existencias: {inventario[i].Existencia}, Precio/Unidad: {inventario[i].Precio}, Precio Total:{inventario[i].Precio * inventario[i].Existencia}{Environment.NewLine}" + control;
                
            }
            return control;

        }
        #endregion
    }
}
