using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Productos.Inventario
{
    public class InventarioModel:IInventarioModel
    {
        public CalculoInventario[] inventario;
        public void Add(CalculoInventario c)
        {
            Add(c, ref inventario);
            Array.Sort(inventario, new CalculoInventario.InventaById());
        }
        public void Add(CalculoInventario p, ref CalculoInventario[] pds)
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
                if (c.especie == Especie.Entrada)
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
            }
            else
            {
               d = a - b;
            }
            return d;
        }
        public decimal GetTotalPrecio()
        {
            decimal a = 0;
            decimal b = 0;
            decimal precioT;
            foreach (CalculoInventario c in inventario)
            {
                if (c.especie == Especie.Entrada)
                {
                    precioT = c.Existencia * c.Precio;
                    a = precioT + a;
                }
                else
                {
                    precioT = c.Existencia * c.Precio;
                    b = precioT + b;
                }
            }
            decimal d = 0;
            if (b > a)
            {
            }
            else
            {
                d = a - b;
            }
            return d;
        }
        public decimal PEPSSalida(int ventas, DateTime dt, int ID)
        {
            decimal PrecioV = 0;
            CalculoInventario[] inv = new CalculoInventario[inventario.Length];
            Array.Copy(inventario, inv, inventario.Length);
            int i = 0;
            while (ventas > 0)
            {
                if (inv[i].Existencia != 0)
                {
                    if (ventas > inv[i].Existencia)
                    {
                        if (inv[i].especie == Especie.Entrada)
                        {
                            PrecioV = inv[i].Precio;
                            int a = ventas;
                            ventas = ventas - inv[i].Existencia;
                            if (ventas > 0)
                            {
                                CalculoInventario model = new CalculoInventario(ID, inv[i].Existencia, PrecioV, Especie.Salida, dt)
                                {
                                    Metodo = Metodo.PEPS,
                                };
                                Add(model);
                            }
                            else
                            {
                                CalculoInventario model = new CalculoInventario(ID, a, PrecioV, Especie.Salida, dt)
                                {
                                    Metodo = Metodo.PEPS,
                                };
                                Add(model);

                            }

                            i++;
                        }
                    }
                    else
                    {
                        if (inv[i].especie == Especie.Entrada)
                        {
                            PrecioV = inv[i].Precio;
                            CalculoInventario model = new CalculoInventario(ID, ventas, PrecioV, Especie.Salida, dt)
                            {
                                Metodo = Metodo.PEPS,
                            };
                            Add(model);
                            ventas = 0;
                        }
                    }
                }
                else
                {
                    i++;
                }

            }
            return PrecioV;
        }
        public decimal UEPSSalida(int ventas, DateTime dt, int ID)
        {
            decimal PrecioV = 0;
            CalculoInventario[] inv = new CalculoInventario[inventario.Length];
            Array.Copy(inventario, inv, inventario.Length);
            int i = inventario.Length - 1;
            while (ventas > 0)
            {
                if (inv[i].Existencia != 0)
                {
                    if (ventas > inv[i].Existencia)
                    {
                        if (inv[i].especie == Especie.Entrada)
                        {
                            PrecioV = inv[i].Precio;
                            int a = ventas;
                            ventas = ventas - inv[i].Existencia;
                            if (ventas > 0)
                            {
                                CalculoInventario model = new CalculoInventario(ID, inv[i].Existencia, PrecioV, Especie.Salida, dt)
                                {
                                    Metodo = Metodo.PEPS,
                                };
                                Add(model);
                            }
                            else
                            {
                                CalculoInventario model = new CalculoInventario(ID, a, PrecioV, Especie.Salida, dt)
                                {
                                    Metodo = Metodo.PEPS,
                                };
                                Add(model);

                            }

                            i--;
                        }
                    }
                    else
                    {
                        if (inv[i].especie == Especie.Entrada)
                        {
                            PrecioV = inv[i].Precio;
                            CalculoInventario model = new CalculoInventario(ID, ventas, PrecioV, Especie.Salida, dt)
                            {
                                Metodo = Metodo.PEPS,
                            };
                            Add(model);
                            ventas = 0;
                        }
                    }
                }
                else
                {
                    i--;
                }

            }
            return PrecioV;
        }
        public void PromedioSimple(int ventas, DateTime dt, int ID)
        {
            if (inventario == null)
            {
                return;
            }
            int a = 0;
            decimal b = 0;
            foreach (CalculoInventario c in inventario)
            {
                if (c.especie == Especie.Entrada)
                {
                    b = c.Precio + b;
                    a++;
                }
            }
            decimal promedio = b / a;
            CalculoInventario model = new CalculoInventario(ID, ventas, promedio, Especie.Salida, dt)
            {
                Metodo = Metodo.PromedioSimple,
            };
            Add(model);
        }
        public void PromedioPonderado(int ventas, DateTime dt, int ID)
        {
            if (inventario == null)
            {
                return;
            }
            int a = 0;
            decimal b = 0;
            foreach (CalculoInventario c in inventario)
            {
                int f = c.Existencia;
                if (c.especie == Especie.Entrada)
                {
                    b = (c.Precio * f)+b;
                    a=f+a;
                }
            }
            decimal promedio = b / a;
            CalculoInventario model = new CalculoInventario(ID, ventas, promedio, Especie.Salida, dt)
            {
                Metodo = Metodo.PromedioPonderado,
            };
            Add(model);
        }
        public string Mostrar()
        {
            string control = "";
            for(int i=inventario.Length-1;i>=0; i--)
            {
                if (inventario[i].especie == Especie.Salida)
                {
                    control = $@"Fecha: {inventario[i].date}, Especie: {inventario[i].especie}, Existencias: {inventario[i].Existencia}, Precio/Unidad: {inventario[i].Precio}, Precio Total:{inventario[i].Precio * inventario[i].Existencia}, Metodo: {inventario[i].Metodo}{Environment.NewLine}" + control;
                }
                else
                {

                    control = $@"Fecha: {inventario[i].date}, Especie: {inventario[i].especie}, Existencias: {inventario[i].Existencia}, Precio/Unidad: {inventario[i].Precio}, Precio Total:{inventario[i].Precio * inventario[i].Existencia}{Environment.NewLine}" + control;
                }
            }
         string total = $"{Environment.NewLine}Total Existencias: {GetExistencias()} Total de Saldo:{GetTotalPrecio()}";
            control = control + total;
            return control;
          
        }
        public void Create(CalculoInventario t)
        {
            throw new NotImplementedException();
        }

        public int Update(CalculoInventario t)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CalculoInventario t)
        {
            throw new NotImplementedException();
        }

        public CalculoInventario[] FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
