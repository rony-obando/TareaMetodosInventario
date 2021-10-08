using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CalculoInventario:ProductoGeneral
    {
        

        public Especie especie { get; set; }
        public CalculoInventario(int c,int a,decimal b,Especie especie):base(c,a,b )
        {
            
        }
        public class InventaById : IComparer<CalculoInventario>
        {
            public int Compare(CalculoInventario x, CalculoInventario y)
            {
                if (x.ID < y.ID)
                {
                    return -1;
                }
                else if (x.ID > y.ID)
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
