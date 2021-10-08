using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductoGeneral
    {
        

        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public int ID { get; set; }
        public ProductoGeneral(int id,int existencia, decimal precio)
        {
            ID = id;
            Existencia = existencia;
            Precio = precio;
        }
    }

}
