using AppCore.Interfaces;
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
    public partial class FrmProductos : Form
    {
        private IProductoService productoModel;
        public InventarioModel Inventario;
        public FrmProductos(IProductoService productoService)
        {
            productoModel = productoService;
            Inventario = new InventarioModel();
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            cmbMeasureUnit.Items.AddRange(Enum.GetValues(typeof(UnidadMedida))
                                              .Cast<object>()
                                              .ToArray()
                                          );
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            FrmProducto frmProducto = new FrmProducto(Inventario);
            frmProducto.PModel = productoModel; 
            frmProducto.ShowDialog();
            rtbProductView.Text = productoModel.GetProductosAsJson();
            
        }

        private void CmbFinderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbFinderType.SelectedIndex)
            {
                case 0:
                    txtFinder.Visible = true;
                    cmbMeasureUnit.Visible = false;
                    break;
                case 3:
                    cmbMeasureUnit.Visible = true;
                    txtFinder.Visible = false;
                    break;                
            }
        }

        private void btnExtraer_Click(object sender, EventArgs e)
        {
            FrmProducto frmProducto = new FrmProducto(2,Inventario);
            frmProducto.PModel = productoModel;
            frmProducto.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                rtbProductView.Text = Inventario.Mostrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
