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
        public static int TiempoEsperaComando  = 30;

        public static Tuple<List<String>,string, Boolean>  ObtenerMeses()
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            List<String> listadoDatos = new List<String>();
            Tuple<List<String>, string, Boolean> resultado = null;
          
            String mesnsajeError = "";
            Boolean procesado = true;

            String baseNombre = "DbDatos";
         
            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(DConexion.CnBDEmpresa);
                var baseDatos = builder.InitialCatalog;

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
               

                SqlComando.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' " +
                    " and (table_catalog='"+ baseNombre+"' and TABLE_NAME<>'repetidos' and TABLE_NAME<>'nuevos' and TABLE_NAME<>'fechas_suspencion')";

                SqlComando.CommandType = CommandType.Text;
                SqlComando.CommandTimeout = TiempoEsperaComando;

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
                procesado = false;
                mesnsajeError = ex.Message;
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            resultado = Tuple.Create(listadoDatos, mesnsajeError, procesado);

            return resultado;

        }

        public static Tuple<string, Boolean> EjecutarQuery(string query)
        {
            String mesnsajeError = "";
            Boolean resultado = true;
            SqlConnection SqlConexion = new SqlConnection();
            Tuple<string, Boolean> datos = null;

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = query;
                SqlComando.CommandType = CommandType.Text;
                SqlComando.CommandTimeout = TiempoEsperaComando;

                SqlComando.ExecuteNonQuery();

              }

            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                mesnsajeError = ex.Message;
                resultado = false;
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            datos = Tuple.Create(mesnsajeError, resultado);

            return datos;
        }

        public static Tuple<decimal, string,Boolean> obtenerSaldo(string query)
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            Tuple<decimal, string, Boolean> listadoDatos = null;
            decimal cantidad = 0;        
            String mesnsajeError = "";
            Boolean res = true;

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = query;
                SqlComando.CommandType = CommandType.Text;
                SqlComando.CommandTimeout = TiempoEsperaComando;

                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    cantidad= (decimal)oSqlDataReader.GetValue(0);
                };
            }

            catch (Exception ex)
            {
                mesnsajeError = ex.Message;
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                res = false;
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            listadoDatos = Tuple.Create(cantidad, mesnsajeError,res);

            return listadoDatos;
        }

        public static Tuple<int, string, Boolean> obtenerCantidades(string query)
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            Tuple<int, string, Boolean> listadoDatos = null;
            int cantidad = 0;
            String mesnsajeError = "";
            Boolean res = true;

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = query;
                SqlComando.CommandType = CommandType.Text;
                SqlComando.CommandTimeout = TiempoEsperaComando;

                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    cantidad = (int)oSqlDataReader.GetValue(0);
                };
            }

            catch (Exception ex)
            {
                mesnsajeError = ex.Message;
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            listadoDatos = Tuple.Create(cantidad, mesnsajeError, res);


            return listadoDatos;
        }

        public static Tuple<List<object[]>, string, Boolean> ExportarRegistroNuevos(String mesActual)
        {
            String comando = "SELECT * FROM  " + mesActual + " where nuevo='si'";
            return exportarDatos(comando, mesActual);
        }

        public static Tuple<List<object[]>, string, Boolean> ExportarRegistroRepetidos(String mesActual)
        {
            String comando = "SELECT * FROM  " + mesActual + " where repetido='si'";
            return exportarDatos(comando, mesActual);
        }

        public static Tuple<List<object[]>, string, Boolean> ExportarMesNuevo(String mesActual)
        {
            String comando = "SELECT * FROM " + mesActual;
            return exportarDatos(comando, mesActual);
        }

        public static Tuple<List<object[]>, string, Boolean> ExportarMesViejo(String mesAnterior)
        {
            String comando = "SELECT * FROM " + mesAnterior;
            return exportarDatos(comando, mesAnterior);
        }

        public static Tuple<List<object[]>, string, Boolean> exportarDatos(String comando, String tabla)
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            String mesnsajeError = ""; Boolean existeError = false;
            Tuple<List<object[]>, string, Boolean> listadoDatos = null;
            List<object[]> datos = new List<object[]>();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;

                //Obtener datos
                SqlComando.CommandText = comando;
                SqlComando.CommandType = CommandType.Text;
                SqlComando.CommandTimeout = TiempoEsperaComando;

                oSqlDataReader = SqlComando.ExecuteReader();

                object[] output = new object[oSqlDataReader.FieldCount];
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                    output[i] = oSqlDataReader.GetName(i);

                while (oSqlDataReader.Read())
                {
                    oSqlDataReader.GetValues(output);
                    datos.Add(output);
                    output = new object[oSqlDataReader.FieldCount];
                }

                oSqlDataReader.Close();
            }

            catch (Exception ex)
            {
                mesnsajeError = ex.Message;
                existeError = false;
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            listadoDatos = Tuple.Create(datos, mesnsajeError, existeError);

            return listadoDatos;
        }


        public static DataTable obtenerDatosDatables(String query)
        {
            SqlConnection SqlConexion = new SqlConnection();
            DataTable dataTable = new DataTable();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;

                SqlCommand cmd = new SqlCommand(query, SqlConexion);
                cmd.CommandTimeout = TiempoEsperaComando;

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


        public static Tuple<List<object[]>, string, Boolean> obtenerNombresColumnas(String tabla)
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            String mesnsajeError = ""; Boolean existeError = false;
            Tuple<List<object[]>, string, Boolean> listadoDatos = null;
            List<object[]> datos = new List<object[]>();
            String queryColumnas = "SELECT column_name  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + tabla + "'";

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;

                //Obtener datos
                SqlComando.CommandText = queryColumnas;
                SqlComando.CommandType = CommandType.Text;
                SqlComando.CommandTimeout = TiempoEsperaComando;

                oSqlDataReader = SqlComando.ExecuteReader();

                object[] output = new object[oSqlDataReader.FieldCount];
                for (int i = 0; i < oSqlDataReader.FieldCount; i++)
                    output[i] = oSqlDataReader.GetName(i);

                while (oSqlDataReader.Read())
                {
                    oSqlDataReader.GetValues(output);
                    datos.Add(output);
                    output = new object[oSqlDataReader.FieldCount];
                }

                oSqlDataReader.Close();
            }

            catch (Exception ex)
            {
                mesnsajeError = ex.Message;
                existeError = false;
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            listadoDatos = Tuple.Create(datos, mesnsajeError, existeError);

            return listadoDatos;
        }
    }

}