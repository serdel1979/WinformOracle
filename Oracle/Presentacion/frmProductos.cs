using Oracle.Datos;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace Oracle.Presentacion
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }


        #region "VARIABLES"

        int codigo_prod = 0;
        int estado = 0;

        #endregion

        #region "METODOS"

        private void clearTxt()
        {
            txtMarca.Clear();
            txtProducto.Clear();
            txtProducto.Clear();
            txtStock.Clear();
            txtMedida.Clear();
            comboCategorias.Text = "";
        }

        private void EstadoTexto(bool lEstado)
        {
            txtProducto.Enabled = lEstado;
            txtMarca.Enabled = lEstado;
            txtMedida.Enabled = lEstado;
            txtStock.Enabled = lEstado;
            comboCategorias.Enabled = lEstado;
        }


        private void estadoBotonesPrincipales(bool lEstado)
        {
            btnNuevo.Enabled = lEstado;
            btnActualizar.Enabled = lEstado;
            btnEliminar.Enabled = lEstado;
            btnReporte.Enabled = lEstado;
            btnSalir.Enabled = lEstado;
        }

        private void estadoVisibilidadBotonesProceso(bool lEstado)
        {
            btnGuardar.Visible = lEstado;
            btnCancelar.Visible = lEstado;
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            estado = 1; //estado nuevo

            this.clearTxt();
            this.EstadoTexto(true);
            this.estadoBotonesPrincipales(true);
            this.estadoVisibilidadBotonesProceso(true);
            txtProducto.Focus();
        }

        public  void listado_categorias()
        {
            DProductos Datos = new DProductos();
            comboCategorias.DataSource =  Datos.listado_categorias();
            comboCategorias.ValueMember = "codigo_ca";
            comboCategorias.DisplayMember = "descripcion";   
        }

        private void testDatabaseConnection()
        {
            DProductos dProductos = new DProductos();
            DataTable result = dProductos.testConnection();

            if (result != null && result.Rows.Count > 0)
            {
                Console.WriteLine("Test connection succeeded. Result: " + result.Rows[0][0]);
            }
            else
            {
                Console.WriteLine("Test connection failed or returned no data.");
            }
        }

        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            estado = 1; //estado nuevo

            this.clearTxt();
            this.EstadoTexto(false);
            this.estadoBotonesPrincipales(false);
            this.estadoVisibilidadBotonesProceso(false);
            txtProducto.Focus();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
         //   this.testDatabaseConnection();
            this.listado_categorias();
        }
    }
}
