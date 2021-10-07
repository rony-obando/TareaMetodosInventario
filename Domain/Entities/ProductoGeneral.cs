using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductoGeneral
    {
        

        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public ProductoGeneral(int existencia, decimal precio)
        {
            Existencia = existencia;
            Precio = precio;
        }
    }

}
