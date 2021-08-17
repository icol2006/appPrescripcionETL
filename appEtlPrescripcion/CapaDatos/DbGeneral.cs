using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public static class DbGeneral
    {
        public static List<String> ObtenerMeses()
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            List<String> listadoDatos = new List<String>();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' " +
                    " and (table_catalog='DbTestImport' and TABLE_NAME<>'repetidos' and TABLE_NAME<>'nuevos' and TABLE_NAME<>'fechas_suspencion')";

                SqlComando.CommandType = CommandType.Text;
                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var dato=  oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("TABLE_NAME")) == DBNull.Value
                    ? null : (string)oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("TABLE_NAME"));

                    listadoDatos.Add(dato);
                };
                oSqlDataReader.Close();
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
