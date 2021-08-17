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
        public static List<DateTime?> ObtenerGruposFechaComparendo(string mesActual)
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
                SqlComando.CommandText = "SELECT FECHA_COMPARENDO FROM  " + mesActual+
                                         " group by FECHA_COMPARENDO";

                SqlComando.CommandType = CommandType.Text;
                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var fecha = oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("FECHA_COMPARENDO")) == DBNull.Value
                    ? (DateTime?)null : Convert.ToDateTime(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("FECHA_COMPARENDO")));

                    listadoDatos.Add(fecha);
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

        public static List<DateTime?> ObtenerGruposFechaNotificacion(string mesActual)
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
                SqlComando.CommandText = "SELECT fecha_notificacion FROM  " + mesActual  +
                                         " group by fecha_notificacion";

                SqlComando.CommandType = CommandType.Text;
                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var fecha = oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("fecha_notificacion")) == DBNull.Value
                    ? (DateTime?)null : Convert.ToDateTime(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("fecha_notificacion")));

                    listadoDatos.Add(fecha);
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
                SqlComando.CommandText = "update "+ mesActual+"  set res_fecha_comparendo=@res_fecha_comparendo,fc_procesada=@fc_procesada " +
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
                SqlComando.CommandText = "update "+ mesActual+" set res_fecha_notificacion=@res_fecha_notificacion,fn_procesada=@fn_procesada " +
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

        public static Boolean ConversionDatos(string mesActual)
        {
            Boolean resultado = true;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "update  "+ mesActual+" set id= [NRO DOCUMENTO]+[COMPARENDO]+[FECHA COMPARENDO]";
                SqlComando.CommandType = CommandType.Text;
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "delete from repetidos insert into repetidos" +
                    "(registro, cantidad) select id, count(*) as cantidad from mes_nuevo group by id HAVING count(*) > 2 order by cantidad";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "ALTER TABLE mes_nuevo DROP COLUMN num";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "Alter Table mes_nuevo Add num Int Identity(1, 1)";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "DELETE FROM mes_nuevo WHERE num NOT IN  (SELECT MIN(num) FROM mes_nuevo GROUP BY id)";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "update mes_nuevo set [FECHA_COMPARENDO]=CONVERT(DATETIME,[FECHA COMPARENDO],103) WHERE [FECHA COMPARENDO] LIKE '%/%'";
                SqlComando.ExecuteNonQuery();

                SqlComando.CommandText = "update mes_nuevo set [FECHA_NOTIFICACION]=CONVERT(DATETIME,[FECHA NOTIFICACION],103) WHERE [FECHA NOTIFICACION] LIKE '%/%'";
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


        public static void ExportarRegistroNuevos(String mesActual)
        {
            String comando = "SELECT * FROM  "+ mesActual;
            exportarDatos(comando);
        }


        public static void ExportarRegistroRepetidos(String mesActual)
        {
            var comando = "SELECT * FROM "+ mesActual;
            exportarDatos(comando);
        }


        public static void ExportarMesNuevo(String mesActual)
        {
            String comando = "SELECT * FROM "+ mesActual;
            exportarDatos(comando);
        }

        public static void ExportarMesViejo(String mesAnterior)
        {
            String comando = "SELECT * FROM "+ mesAnterior;
            exportarDatos(comando);
        }

        public static void exportarDatos(String comando)
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
   
            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = comando;
                                       

                SqlComando.CommandType = CommandType.Text;
                oSqlDataReader = SqlComando.ExecuteReader();
                

                string fileName = "datos.csv";
                StreamWriter sw = new StreamWriter(fileName);
                object[] output = new object[oSqlDataReader.FieldCount];

                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                    output[i] = oSqlDataReader.GetName(i);

                sw.WriteLine(string.Join(";", output));

                while (oSqlDataReader.Read())
                {
                    oSqlDataReader.GetValues(output);
                    sw.WriteLine(string.Join(";", output));
                }

                sw.Close();
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
        }

        public static DataTable obtenerDatosDatables(String query)
        {
            SqlConnection SqlConexion = new SqlConnection();
            DataTable dataTable = new DataTable();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
              
                SqlCommand cmd = new SqlCommand(query, SqlConexion);
                SqlConexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                SqlConexion.Close();
                da.Dispose();

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

            return dataTable;
        }



    }
}
