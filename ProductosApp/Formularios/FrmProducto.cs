using AppCore.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infraestructure.Productos;
using Infraestructure.Productos.Inventario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductosApp.Formularios
{    
    public partial class FrmProducto : Form
    {
        public IProductoService PModel { get; set; }
        public InventarioModel Inventario;
        public int a;
        public FrmProducto(int b, InventarioModel i)
        {
            InitializeComponent();
            this.a=b;
            Inventario = i;
        }
        public FrmProducto(InventarioModel i)
        {
            InitializeComponent();
            Inventario = i;
        }
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            cmbMeasureUnit.Items.AddRange(Enum.GetValues(typeof(UnidadMedida))
                                              .Cast<object>()
                                              .ToArray()
                                          );
            if (a == 2)
            {
                lblSalida.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label6.Visible = false;
                txtNombre.Visible = false;
                txtDesc.Visible = false;
                nudPrice.Visible = false;
                cmbMeasureUnit.Visible = false;
                lblMetodo.Visible = true;
                cmbMetodos.Visible = true;
               
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (a == 2)
            {
                switch (cmbMetodos.SelectedIndex)
                {
                    case 0:
                        Inventario.PEPSSalida((int)nudExist.Value, dtpCaducity.Value, PModel.GetLastProductoId());
                        break;
                    case 1:
                        Inventario.UEPSSalida((int)nudExist.Value, dtpCaducity.Value, PModel.GetLastProductoId());
                        break;
                    case 2:
                        Inventario.PromedioSimple((int)nudExist.Value, dtpCaducity.Value, PModel.GetLastProductoId());
                        break;
                    case 3:
                        Inventario.PromedioPonderado((int)nudExist.Value, dtpCaducity.Value, PModel.GetLastProductoId());
                        break;
                }
                Dispose();
            }
            else
            {
                Producto p = new Producto(PModel.GetLastProductoId() + 1, (int)nudExist.Value, nudPrice.Value, txtNombre.Text, txtDesc.Text, dtpCaducity.Value, (UnidadMedida)cmbMeasureUnit.SelectedIndex);
                CalculoInventario c = new CalculoInventario(p.ID,p.Existencia,p.Precio,Especie.Entrada, dtpCaducity.Value);
                PModel.Create(p);
                Inventario.Add(c);
                Dispose();
            }
        }
    }
}
