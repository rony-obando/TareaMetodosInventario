using AppCore.Interfaces;
using AppCore.Services;
using Autofac;
using Domain.Interfaces;
using Infraestructure.Empleados;
using Infraestructure.Productos;
using Infraestructure.Productos.Inventario;
using ProductosApp.Formularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductosApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();
            var builder1 = new ContainerBuilder();
            //builder.RegisterType<EmpleadoModel>().As<IEmpleadoModel>();
            //builder.RegisterType<EmpleadoService>().As<IEmpleadoService>();
            //var container = builder.Build();
            builder.RegisterType<ProductoModel>().As<IProductoModel>();
            builder.RegisterType<ProductoService>().As<IProductoService>();
            builder1.RegisterType<InventarioModel>().As<IInventarioModel>();
            builder1.RegisterType<InventarioService>().As<IInventarioService>();
            var container = builder.Build();
            var container1 = builder1.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmGestionEmpleados(container.Resolve<IEmpleadoService>()));
            Application.Run(new FrmProductos(container.Resolve<IProductoService>(), container1.Resolve<IInventarioService>()));
        }
    }
}
