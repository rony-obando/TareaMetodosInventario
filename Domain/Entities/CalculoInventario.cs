using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CalculoInventario:ProductoGeneral
    {
        

        public Especie especie { get; set; }
        public DateTime date { get; set; }
        public Metodo Metodo { get; set; }
        public CalculoInventario(int c,int a,decimal b,Especie especie,DateTime date):base(c,a,b )
        {
            this.especie = especie;
            this.date = date;
        }
        public class InventaById : IComparer<CalculoInventario>
        {
            public int Compare(CalculoInventario x, CalculoInventario y)
            {
                if (x.date > y.date)
                {
                    return 1;
                }
                else if (x.date < y.date)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
