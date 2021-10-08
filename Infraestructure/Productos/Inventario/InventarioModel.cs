using Domain.Entities;
using Domain.Enums;
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

        public int GetExistencias()
        {
            int a = 0;
            int b = 0;
            foreach (CalculoInventario c in inventario)
            {
                if (c.especie==Especie.Entrada)
                {
                    a = c.Existencia + a;
                }
                else
                {
                    b = c.Existencia + b;
                }
            }
            int d = 0;
            if (b > a)
            {
                throw new ArgumentException("Error de cantidades");
            }
            else
            {
               d = a - b;
            }
            return d;
        }
       // public CalculoInventario H=new CalculoInventario();
        public void RegistrarUEPS(CalculoInventario c,Especie e)
        {
            if (e == Especie.Salida)
            {
                for (int i = inventario.Length - 1; i < 0; i--)
                {

                    if (inventario[i].Existencia < c.Existencia)
                    {
                        if (c.Existencia != 0)
                        {
                            c.Existencia = c.Existencia - inventario[i].Existencia;
                            Delete(inventario[i]);
                            c.Precio = inventario[i].Precio;
                            c.especie = e;
                            Add(c);
                        }
                       
                    }


                }
            }

            
        }
        /*public void RegistrarPEPS(CalculoInventario c, Especie e)
        {

            if (e == Especie.Salida)
            {
                c.Precio = inventario[inventario.Length - 1].Precio;
                c.especie = e;
                Add(c);
            }


        }*/
        public bool Delete(CalculoInventario p)
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

            if (index != inventario.Length - 1)
            {
                inventario[index] = inventario[inventario.Length - 1];
            }

            CalculoInventario[] tmp = new CalculoInventario[inventario.Length - 1];
            Array.Copy(inventario, tmp, tmp.Length);
            inventario = tmp;
            Array.Sort(inventario, new CalculoInventario.InventaById());
            return inventario.Length == tmp.Length;
        }
        private int GetIndexById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El id no puede ser negativo o cero.");
            }

            int index = int.MinValue, i = 0;
            if (inventario == null)
            {
                return index;
            }

            foreach (CalculoInventario p in inventario)
            {
                if (p.ID == id)
                {
                    index = i;
                    break;
                }
                i++;
            }

            return index;
        }
    }
}
