using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oracle.Datos
{
    public class Conexion
    {
        private static Conexion con = null;


        public Conexion()
        {
            
        }



        public DataTable testConnection()
        {
            DataTable table = new DataTable();
            using (OracleConnection Sqlcon = Conexion.getInstancia().CrearConexion())
            {
                try
                {
                    Sqlcon.Open();
                    OracleCommand command = new OracleCommand("SELECT 1 FROM dual", Sqlcon);
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



        public OracleConnection CrearConexion()
        {
            OracleConnection cadena = new OracleConnection();
            try
            {
                //"Data Source=localhost;User ID=sergio;Password=admin;Unicode=True"
               // cadena.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521)))(CONNECT_DATA=(SID=xe)));User Id=sergio;Password=admin;";
               cadena.ConnectionString = "Data Source=localhost;User ID=sergio;Password=admin"; //<-- esta cadena funcionó

            }
            catch (Exception)
            {
                cadena = null;
                throw;
            }
            return cadena;
        }

        public static Conexion getInstancia()
        {
            if(con == null)
            {
                con = new Conexion();
            }
            return con;
        }

    }
}
