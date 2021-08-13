using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public static class DbMesNuevo
    {
        public static List<List<String>> Listar()
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            List<List<String>> listadoDatos = new List<List<String>>();
            List<String> datos = new List<string>();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "SELECT id,FECHA_COMPARENDO FROM mes_nuevo where FECHA_COMPARENDO>='2017-05-13' and FECHA_COMPARENDO<='2019-05-15'";
               // SqlComando.CommandText = "SELECT id,FECHA_COMPARENDO FROM mes_nuevo";

                SqlComando.CommandType = CommandType.Text;

                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var sad = oSqlDataReader.GetValue(0);
                    var fecha = oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("FECHA_COMPARENDO")) == DBNull.Value
                   ? (DateTime?)null : Convert.ToDateTime(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("FECHA_COMPARENDO")));

                    datos.Add(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("id")) == DBNull.Value
                        ? null : (string)oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("id")));

                    datos.Add(fecha.ToString());

                    listadoDatos.Add(datos);
                    datos = new List<string>();

                };


            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return listadoDatos;
        }
    
    }
}
