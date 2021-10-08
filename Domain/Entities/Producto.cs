using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Producto:ProductoGeneral
    {
       

        
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
        public Producto(int id,int exist, decimal prec,string nombre, string descripcion, DateTime fechaVencimiento, UnidadMedida unidadMedida) : base(id, exist, prec)
        {
            this.Existencia = exist;
            this.Descripcion = descripcion;
            this.Precio = prec;
            this.FechaVencimiento = fechaVencimiento;
            this.UnidadMedida = unidadMedida;
            this.Nombre = nombre;
        }
    public class ProductoOrderByPrecio : IComparer<Producto>
        {
            public int Compare(Producto x, Producto y)
            {
                if(x.Precio < y.Precio)
                {
                    return -1;
                }else if(x.Precio > y.Precio)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
