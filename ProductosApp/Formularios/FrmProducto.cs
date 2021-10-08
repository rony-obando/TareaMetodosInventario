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
        public FrmProducto(int b)
        {
            InitializeComponent();
            this.a=b;
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
                label5.Visible = false;
                label6.Visible = false;
                txtNombre.Visible = false;
                txtDesc.Visible = false;
                nudPrice.Visible = false;
                dtpCaducity.Visible = false;
                cmbMeasureUnit.Visible = false;
                lblID.Visible = false;
                nudID.Visible = false;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (a == 2)
            {
                Producto p = new Producto((int)nudExist.Value, nudPrice.Value, PModel.GetLastProductoId() + 1, txtNombre.Text, txtDesc.Text, dtpCaducity.Value, (UnidadMedida)cmbMeasureUnit.SelectedIndex)
                {
                    //Id = PModel.GetLastProductoId() + 1,
                    //Existencia = (int)nudExist.Value,
                };
                Dispose();
            }
            else
            {
                Producto p = new Producto((int)nudExist.Value, nudPrice.Value, PModel.GetLastProductoId() + 1, txtNombre.Text, txtDesc.Text, dtpCaducity.Value, (UnidadMedida)cmbMeasureUnit.SelectedIndex)
                {
                    //Id = PModel.GetLastProductoId() + 1,
                   // Nombre = txtNombre.Text,
                   // Descripcion = txtDesc.Text,
                   // Existencia = (int)nudExist.Value,
                    //Precio = nudPrice.Value,
                   // FechaVencimiento = dtpCaducity.Value,
                    //UnidadMedida = (UnidadMedida)cmbMeasureUnit.SelectedIndex
                };

                PModel.Create(p);

                Dispose();
            }
        }
    }
}
