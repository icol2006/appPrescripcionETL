using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public static class DbMesNuevo
    {
        public static int TiempoEsperaComando = 30;

        public static Tuple<List<DateTime?>, string, Boolean> ObtenerGruposFechaComparendo(string mesActual)
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            List<DateTime?> listadoFechas = new List<DateTime?>();
            Tuple<List<DateTime?>, string, Boolean> listadoDatos = null;
            String mesnsajeError = "";
            Boolean resultado = true;

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "SELECT FECHA_COMPARENDO FROM  " + mesActual +
                                         " group by FECHA_COMPARENDO";

                SqlComando.CommandType = CommandType.Text;
                SqlComando.CommandTimeout = TiempoEsperaComando;

                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var fecha = oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("FECHA_COMPARENDO")) == DBNull.Value
                    ? (DateTime?)null : Convert.ToDateTime(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("FECHA_COMPARENDO")));

                    listadoFechas.Add(fecha);
                };
                oSqlDataReader.Close();
            }

            catch (Exception ex)
            {
                mesnsajeError = ex.Message;
                resultado = false;
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            listadoDatos = Tuple.Create(listadoFechas, mesnsajeError, resultado);
            return listadoDatos;
        }

        public static Tuple<List<DateTime?>, string, Boolean> ObtenerGruposFechaNotificacion(string mesActual)
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            List<DateTime?> listadoFechas = new List<DateTime?>();
            Tuple<List<DateTime?>, string, Boolean> listadoDatos = null;
            String mesnsajeError = "";
            Boolean resultado = true;

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "SELECT fecha_notificacion FROM  " + mesActual +
                                         " group by fecha_notificacion";

                SqlComando.CommandType = CommandType.Text;
                SqlComando.CommandTimeout = TiempoEsperaComando;

                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var fecha = oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("fecha_notificacion")) == DBNull.Value
                    ? (DateTime?)null : Convert.ToDateTime(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("fecha_notificacion")));

                    listadoFechas.Add(fecha);
                };
                oSqlDataReader.Close();
            }

            catch (Exception ex)
            {
                mesnsajeError = ex.Message;
                resultado = false;
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            listadoDatos = Tuple.Create(listadoFechas, mesnsajeError, resultado);
            return listadoDatos;
        }

        public static Boolean ActualizarResultadoFechasFinalesComparendo(DateTime fecha_comparendo, DateTime res_fecha_comparendo, Boolean procesado, string mesActual)
        {
            Boolean resultado = true;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "update " + mesActual + "  set res_fecha_comparendo=@res_fecha_comparendo,fc_procesada=@fc_procesada " +
                    " where fecha_comparendo=@fecha_comparendo";

                SqlComando.CommandType = CommandType.Text;

                SqlComando.Parameters.AddWithValue("res_fecha_comparendo", (object)res_fecha_comparendo ?? DBNull.Value);
                SqlComando.Parameters.AddWithValue("fc_procesada", (object)procesado ?? DBNull.Value);
                SqlComando.Parameters.AddWithValue("fecha_comparendo", (object)fecha_comparendo ?? DBNull.Value);
                SqlComando.CommandTimeout = TiempoEsperaComando;

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
            Boolean procesado, string mesActual)
        {
            Boolean resultado = true;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "update " + mesActual + " set res_fecha_notificacion=@res_fecha_notificacion,fn_procesada=@fn_procesada " +
                    " where fecha_notificacion=@fecha_notificacion";

                SqlComando.CommandType = CommandType.Text;

                SqlComando.Parameters.AddWithValue("res_fecha_notificacion", (object)res_fecha_notificacion ?? DBNull.Value);
                SqlComando.Parameters.AddWithValue("fn_procesada", (object)procesado ?? DBNull.Value);
                SqlComando.Parameters.AddWithValue("fecha_notificacion", (object)fecha_notificacion ?? DBNull.Value);
                SqlComando.CommandTimeout = TiempoEsperaComando;

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
