using Oracle.Datos;
using Oracle.Entidades;
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
            // btnNuevo.Enabled = lEstado;
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

        private void listado_categorias()
        {
            DProductos Datos = new DProductos();
            comboCategorias.DataSource = Datos.listado_categorias();
            comboCategorias.ValueMember = "codigo_ca";
            comboCategorias.DisplayMember = "descripcion";
        }

        private void listado_productos(string filtro)
        {
            DProductos productos = new DProductos();
            dgvListado.DataSource = productos.listado_productos(filtro);

            this.formateo_datos();
        }


        private void formateo_datos()
        {
            dgvListado.Columns[0].Width = 100;
            dgvListado.Columns[0].HeaderText = "Codigo";

            dgvListado.Columns[1].Width = 180;
            dgvListado.Columns[1].HeaderText = "PRODUCTO";

            dgvListado.Columns[2].Width = 130;
            dgvListado.Columns[2].HeaderText = "MARCA";

            dgvListado.Columns[3].Width = 100;
            dgvListado.Columns[3].HeaderText = "MEDIDA";

            dgvListado.Columns[4].Width = 120;
            dgvListado.Columns[4].HeaderText = "STOCK";

            dgvListado.Columns[6].Width = 158;
            dgvListado.Columns[6].HeaderText = "CATEGORIA";


            dgvListado.Columns[5].Visible = false;
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
            this.listado_productos("%");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtProducto.Text == string.Empty ||
                txtMarca.Text == string.Empty ||
                txtMedida.Text == string.Empty ||
                txtStock.Text == string.Empty ||
                comboCategorias.Text == string.Empty)
            {
                MessageBox.Show("Ingresar datos requeridos (*)",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                string Respuesta = "";
                Producto producto = new Producto()
                {
                    Codigo_pro = codigo_prod,
                    Descripcion = txtProducto.Text,
                    Marca = txtMarca.Text,
                    Medida = txtMedida.Text,
                    Stock = Convert.ToDecimal(txtMedida.Text),
                    Categoria_id = Convert.ToInt32(comboCategorias.SelectedValue)
                };
                DProductos productos = new DProductos();
                Respuesta = productos.Guardar_producto(estado, producto);

                if (Respuesta.Equals("OK"))
                {
                    codigo_prod = 0;
                    this.clearTxt();
                    this.EstadoTexto(false);
                    this.estadoVisibilidadBotonesProceso(false);
                    this.estadoBotonesPrincipales(false);
                    this.listado_productos("%");
                    MessageBox.Show("Datos guardados",
                                        "Aviso",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Respuesta,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }

                
            }
        }
    }
}
