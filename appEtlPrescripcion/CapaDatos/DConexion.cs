using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DConexion
    {

        public static String CnBDEmpresa = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public String ChequearConexion()
        {
            String mensaje = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();
                mensaje = "Y";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                SqlConexion.Close();
            }

            return mensaje;
        }
    }
}
