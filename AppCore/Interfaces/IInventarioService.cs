﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Interfaces
{
    public interface IInventarioService: IService<CalculoInventario>
    {
         int GetExistencias();
         decimal GetTotalPrecio();
         decimal PEPSSalida(int ventas, DateTime dt, int ID);
         decimal UEPSSalida(int ventas, DateTime dt, int ID);
         void PromedioSimple(int ventas, DateTime dt, int ID);
         void PromedioPonderado(int ventas, DateTime dt, int ID);
         string Mostrar();
         void Add(CalculoInventario p, ref CalculoInventario[] pds);
    }
}
