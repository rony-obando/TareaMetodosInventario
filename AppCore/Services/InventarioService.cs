using AppCore.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Services
{
    public class InventarioService : IInventarioService
    {
        private IInventarioModel inventarioService;
        public InventarioService(IInventarioModel model)
        {
            this.inventarioService = model;
        }
        public void Add(CalculoInventario t)
        {
            inventarioService.Add(t);
        }

        public void Add(CalculoInventario p, ref CalculoInventario[] pds)
        {
            inventarioService.Add(p,ref pds);
        }

        public void Create(CalculoInventario t)
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

        public int GetExistencias()
        {
            return inventarioService.GetExistencias();
        }

        public decimal GetTotalPrecio()
        {
            return inventarioService.GetTotalPrecio();
        }

        public string Mostrar()
        {
            return inventarioService.Mostrar();
        }

        public decimal PEPSSalida(int ventas, DateTime dt, int ID)
        {
            return inventarioService.PEPSSalida(ventas,dt,ID);
        }

        public void PromedioPonderado(int ventas, DateTime dt, int ID)
        {
            inventarioService.PromedioPonderado(ventas,dt,ID);
        }

        public void PromedioSimple(int ventas, DateTime dt, int ID)
        {
            inventarioService.PromedioSimple(ventas,dt,ID);
        }

        public decimal UEPSSalida(int ventas, DateTime dt, int ID)
        {
            return inventarioService.UEPSSalida(ventas,dt,ID);
        }

        public int Update(CalculoInventario t)
        {
            throw new NotImplementedException();
        }
    }
}
