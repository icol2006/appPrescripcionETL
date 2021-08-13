using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public static class DbFechasSuspencion
    {
        public static List<FechasSuspencion> Listar()
        {
            SqlDataReader oSqlDataReader;
            SqlConnection SqlConexion = new SqlConnection();
            List<FechasSuspencion> listadoDatos = new List<FechasSuspencion>();
            FechasSuspencion datos = new FechasSuspencion();

            try
            {
                SqlConexion.ConnectionString = DConexion.CnBDEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "SELECT id,fecha FROM fechas_suspencion";
                SqlComando.CommandType = CommandType.Text;

                oSqlDataReader = SqlComando.ExecuteReader();

                while (oSqlDataReader.Read())
                {
                    var id = (int)oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("id"));              

                    var fecha = oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("fecha")) == DBNull.Value
                    ? (DateTime?)null : Convert.ToDateTime(oSqlDataReader.GetValue(oSqlDataReader.GetOrdinal("fecha")));

                    datos.id = (id == null ? 0 : Convert.ToInt32(id));

                    datos.fecha = fecha;
   
                    listadoDatos.Add(datos);
                    datos = new FechasSuspencion();
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
