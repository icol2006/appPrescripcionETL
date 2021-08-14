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
        public static List<DateTime?> ObtenerGruposFechaComparendo()
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            List<DateTime?> listadoDatos = new List<DateTime?>();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "SELECT FECHA_COMPARENDO FROM mes_nuevo " +
                                         " group by FECHA_COMPARENDO";

                SqlComando.CommandType = CommandType.Text;
                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var fecha = oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("FECHA_COMPARENDO")) == DBNull.Value
                    ? (DateTime?)null : Convert.ToDateTime(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("FECHA_COMPARENDO")));

                    listadoDatos.Add(fecha);
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

        public static List<DateTime?> ObtenerGruposFechaNotificacion()
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            List<DateTime?> listadoDatos = new List<DateTime?>();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "SELECT fecha_notificacion FROM mes_nuevo " +
                                         " group by fecha_notificacion";

                SqlComando.CommandType = CommandType.Text;
                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var fecha = oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("fecha_notificacion")) == DBNull.Value
                    ? (DateTime?)null : Convert.ToDateTime(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("fecha_notificacion")));

                    listadoDatos.Add(fecha);
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

        public static Boolean ActualizarResultadoFechasFinalesComparendo(DateTime fecha_comparendo, DateTime res_fecha_comparendo, Boolean procesado)
        {
            Boolean resultado = true;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "update mes_nuevo set res_fecha_comparendo=@res_fecha_comparendo,fc_procesada=@fc_procesada " +
                    " where fecha_comparendo=@fecha_comparendo";

                SqlComando.CommandType = CommandType.Text;

                SqlComando.Parameters.AddWithValue("res_fecha_comparendo", (object)res_fecha_comparendo ?? DBNull.Value);
                SqlComando.Parameters.AddWithValue("fc_procesada", (object)procesado ?? DBNull.Value);
                SqlComando.Parameters.AddWithValue("fecha_comparendo", (object)fecha_comparendo ?? DBNull.Value);

                SqlComando.ExecuteNonQuery();
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                resultado = false;
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return resultado;
        }

        public static Boolean ActualizarResultadoFechasFinalesNotificacion(DateTime fecha_notificacion, DateTime res_fecha_notificacion,
            Boolean procesado)
        {
            Boolean resultado = true;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "update mes_nuevo set res_fecha_notificacion=@res_fecha_notificacion,fn_procesada=@fn_procesada " +
                    " where fecha_notificacion=@fecha_notificacion";

                SqlComando.CommandType = CommandType.Text;

                SqlComando.Parameters.AddWithValue("res_fecha_notificacion", (object)res_fecha_notificacion ?? DBNull.Value);
                SqlComando.Parameters.AddWithValue("fn_procesada", (object)procesado ?? DBNull.Value);
                SqlComando.Parameters.AddWithValue("fecha_notificacion", (object)fecha_notificacion ?? DBNull.Value);

                SqlComando.ExecuteNonQuery();
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                resultado = false;
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return resultado;
        }

        public static Boolean ConversionDatos()
        {
            Boolean resultado = true;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "update  cartera set id= [NRO DOCUMENTO]+[COMPARENDO]+[FECHA COMPARENDO]";
                SqlComando.CommandType = CommandType.Text;
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "delete from repetidos insert into repetidos" +
                    "(registro, cantidad) select id, count(*) as cantidad from cartera group by id HAVING count(*) > 2 order by cantidad";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "ALTER TABLE mes_nuevo DROP COLUMN num";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "Alter Table mes_nuevo Add num Int Identity(1, 1)";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "DELETE FROM cartera WHERE num NOT IN  (SELECT MIN(num) FROM cartera GROUP BY id)";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "update cartera set [FECHA_COMPARENDO]=CONVERT(DATETIME,[FECHA COMPARENDO],103) WHERE [FECHA COMPARENDO] LIKE '%/%'";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "update cartera set [FECHA_NOTIFICACION]=CONVERT(DATETIME,[FECHA NOTIFICACION],103) WHERE [FECHA NOTIFICACION] LIKE '%/%'";
                SqlComando.ExecuteNonQuery();
            }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                resultado = false;
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return resultado;
        }



    }
}
