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
        public IInventarioService Inventario;
        public FrmProductos(IProductoService productoService,IInventarioService inventario)
        {
            productoModel = productoService;
            Inventario = inventario;
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
            FrmProducto frmProducto = new FrmProducto();
            frmProducto.PModel = productoModel;
            frmProducto.Inventario = Inventario;
            frmProducto.ShowDialog();
            rtbProductView.Text = productoModel.GetProductosAsJson();
            
        }

        private void CmbFinderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbFinderType.SelectedIndex)
            {
                case 0:
                    nudMayor.Visible = true;
                    nudMenor.Visible = true;
                    dtpBuscar.Visible = false;
                    cmbMeasureUnit.Visible = false;
                    break;
                case 1:
                    nudMayor.Visible = false;
                    nudMenor.Visible = false;
                    dtpBuscar.Visible = true;
                    cmbMeasureUnit.Visible = false;
                    break;
                case 2:
                    nudMayor.Visible = false;
                    nudMenor.Visible = false;
                    dtpBuscar.Visible = false;
                    cmbMeasureUnit.Visible = true;
                    break;
            }
        }

        private void btnExtraer_Click(object sender, EventArgs e)
        {
            FrmProducto frmProducto = new FrmProducto(2);
            frmProducto.PModel = productoModel;
            frmProducto.Inventario = Inventario;
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
        private void btnFind_Click(object sender, EventArgs e)
        {
            string a = "";
            switch (cmbFinderType.SelectedIndex)
            {
                case 0:
                    if (nudMenor.Value > nudMayor.Value)
                    {
                        MessageBox.Show("Error,intercambie los rangos");
                    }
                    else
                    {
                        a = productoModel.Mostrar(productoModel.GetProductosByRangoPrecio(nudMenor.Value, nudMayor.Value));
                        rtbProductView.Text = a;
                    }
                    break;
                case 1:
                     a = productoModel.Mostrar(productoModel.GetProductosByFechaVencimiento(dtpBuscar.Value));
                    rtbProductView.Text = a;
                    break;
                case 2:
                    a = productoModel.Mostrar(productoModel.GetProductosByUnidadMedida((UnidadMedida)cmbMeasureUnit.SelectedIndex));
                    rtbProductView.Text = a;
                    break;
            }
        }
    }
}
