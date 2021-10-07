using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CalculoInventario:ProductoGeneral
    {
        

        public Especie especie { get; set; }
        public CalculoInventario(int a,decimal b,Especie especie):base(a,b )
        {
            
        }

    }
}
