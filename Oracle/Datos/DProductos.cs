using Oracle.Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle.Datos
{
    public class DProductos
    {
        public DataTable listado_categorias()
        {
            OracleDataReader Resultado;
            DataTable table = new DataTable();
            OracleConnection Sqlcon = new OracleConnection();
            
            try
            {

                Sqlcon = Conexion.getInstancia().CrearConexion();
                //SELECT 1,2 FROM dual



                OracleCommand command = new OracleCommand("SELECT descripcion, codigo_ca FROM categorias", Sqlcon);
                //               command.CommandType = CommandType.StoredProcedure; //<-- si voy a usar procedimientos almacenados
                command.CommandType = CommandType.Text;
                Sqlcon.Open();
                Resultado = command.ExecuteReader();
                table.Load(Resultado);


                return table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if(Sqlcon.State == ConnectionState.Open) Sqlcon.Close();
            }
        }


        public DataTable listado_productos(string filtro)
        {
            OracleDataReader Resultado;
            DataTable table = new DataTable();
            OracleConnection Sqlcon = new OracleConnection();

            try
            {
                string cTexto = "%" + filtro + "%";
                Sqlcon = Conexion.getInstancia().CrearConexion();

                OracleCommand command = new OracleCommand("select * from vista_productos where descripcion like '"+filtro+"' order by descripcion", Sqlcon);
                command.CommandType = CommandType.Text;
                Sqlcon.Open();
                Resultado = command.ExecuteReader();
                table.Load(Resultado);


                return table;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();
            }
        }


        public string Guardar_producto(int Opcion, Producto producto )
        {
            string Response = "";
            OracleConnection Sqlcon = new OracleConnection();

            try
            {
                Sqlcon = Conexion.getInstancia().CrearConexion();

                OracleCommand command = new 
                    OracleCommand("guardar_producto", Sqlcon);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("pOpcion",OracleDbType.Int32).Value  = Opcion;
                command.Parameters.Add("pCodigo", OracleDbType.Int32).Value = producto.Codigo_pro;
                command.Parameters.Add("pDescripcion", OracleDbType.Varchar2).Value = producto.Descripcion;
                command.Parameters.Add("pMarca", OracleDbType.Varchar2).Value = producto.Marca;
                command.Parameters.Add("pMedida", OracleDbType.Varchar2).Value = producto.Medida;
                command.Parameters.Add("pStock", OracleDbType.Decimal).Value = producto.Stock;
                command.Parameters.Add("pCategoria_id", OracleDbType.Int32).Value = producto.Categoria_id;

                Sqlcon.Open();
                int resultado = command.ExecuteNonQuery();
                Response = resultado == -1 ? "OK" : "No se completó la operación";

            }
            catch (Exception ex)
            {
                Response = ex.Message;
            }
            finally
            {
                if (Sqlcon.State == ConnectionState.Open) Sqlcon.Close();
            }
            return Response;
        }



        public DataTable testConnection()
        {
            DataTable table = new DataTable();
            using (OracleConnection Sqlcon = Conexion.getInstancia().CrearConexion())
            {
                try
                {
                    Sqlcon.Open();
                    OracleCommand command = new OracleCommand("SELECT 1,2 FROM dual", Sqlcon);
                    OracleDataAdapter adapter = new OracleDataAdapter(command);
                    adapter.Fill(table);
                    Console.WriteLine("Prueba de conexión exitosa.");
                    return table;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    throw;
                }
                finally
                {
                    Sqlcon.Close();
                }
            }
        }

    }
}
