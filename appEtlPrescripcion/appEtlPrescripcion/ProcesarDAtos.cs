using AppBot.Modelos;
using appEtlPrescripcion.Dto;
using CapaDatos;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace appEtlPrescripcion
{
    public static class ProcesarDAtos
    {
        public static String realizarConversion(String mesActual)
        {
            String resultado = "";
            List<String> listadoQuery = new List<string>();
            renombrarColumnas(mesActual);

            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'ID') BEGIN ALTER TABLE " + mesActual + " ADD ID varchar(255) END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'fecha_comparendo') BEGIN ALTER TABLE " + mesActual + " ADD fecha_comparendo date END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'fecha_notificacion') BEGIN ALTER TABLE " + mesActual + " ADD fecha_notificacion date END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'res_fecha_comparendo') BEGIN ALTER TABLE " + mesActual + " ADD res_fecha_comparendo date END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'res_fecha_notificacion') BEGIN ALTER TABLE " + mesActual + "  ADD res_fecha_notificacion date END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'res_fecha_comparendo_formato') BEGIN ALTER TABLE " + mesActual + " ADD res_fecha_comparendo_formato varchar(255) END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'res_fecha_notificacion_formato') BEGIN ALTER TABLE " + mesActual + "  ADD res_fecha_notificacion_formato varchar(255) END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'prescrito') BEGIN ALTER TABLE " + mesActual + "  ADD prescrito varchar(10) END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'fc_procesada') BEGIN ALTER TABLE " + mesActual + "  ADD fc_procesada bit END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'fn_procesada') BEGIN ALTER TABLE " + mesActual + "  ADD fn_procesada bit END;");

            listadoQuery.Add("update  " + mesActual + " set id=(NRO#DOCUMENTO+COMPARENDO+FECHA#COMPARENDO)");
            listadoQuery.Add("delete from repetidos  insert into repetidos (registro, cantidad) select id, count(*) as cantidad from " + mesActual + " group by id HAVING count(*) > 2 order by cantidad");

            listadoQuery.Add("IF  EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'num') BEGIN ALTER TABLE " + mesActual + "  DROP COLUMN num END;");
            listadoQuery.Add("IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'num') BEGIN ALTER TABLE " + mesActual + "  Add num Int Identity(1, 1) END;");

            listadoQuery.Add("DELETE FROM " + mesActual + " WHERE num NOT IN  (SELECT MIN(num) FROM " + mesActual + " GROUP BY id)");
            listadoQuery.Add("update " + mesActual + " set FECHA_COMPARENDO=convert(datetime, FECHA#COMPARENDO, 103) WHERE FECHA#COMPARENDO LIKE '%/%'");
            listadoQuery.Add("update " + mesActual + " set FECHA_NOTIFICACION=convert(datetime, FECHA#notificacion, 103) WHERE FECHA#notificacion LIKE '%/%'");

            foreach (var item in listadoQuery)
            {
               var res= DbGeneral.EjecutarQuery(item);
                if(res.Item2==false)
                {
                    resultado += res.Item1 + System.Environment.NewLine;
                }
            }

            return resultado;            
        }

        public static String procesarFechas(string mesActual)
        {
            var listadoGrupoFechasComparendo = DbMesNuevo.ObtenerGruposFechaComparendo(mesActual);
            var listadoGrupoFechasNotificacion=DbMesNuevo.ObtenerGruposFechaNotificacion(mesActual);
            String resultado = "";
            resultado += listadoGrupoFechasComparendo.Item2 + System.Environment.NewLine;
            resultado += listadoGrupoFechasNotificacion.Item2 + System.Environment.NewLine;

            List<FechasSuspencion> fechasSuspencion = DbFechasSuspencion.Listar();
            List<FechasProcesada> fechasProcesadas = new List<FechasProcesada>();
    

            EstadoForm.totalRegistros = listadoGrupoFechasComparendo.Item1.Count() + listadoGrupoFechasNotificacion.Item1.Count();

            if(listadoGrupoFechasComparendo.Item3==true && listadoGrupoFechasNotificacion.Item3==true)
            {
                foreach (var datos in listadoGrupoFechasComparendo.Item1)
                {
                    if (datos != null && EstadoForm.procesarDatos == true)
                    {
                        var fechaInicio = Convert.ToDateTime(datos);
                        DateTime fechaFinal = DateTime.Now;

                        fechaFinal = obteneterFechaFinal(fechaInicio, fechasSuspencion);
                        DbMesNuevo.ActualizarResultadoFechasFinalesComparendo(fechaInicio, fechaFinal, true, mesActual);

                        fechasProcesadas.Add(new FechasProcesada()
                        {
                            fechaInicio = fechaInicio,
                            fechaFinal = fechaFinal
                        });

                        if (EstadoForm.procesarDatos == false)
                        {
                            break;
                        }

                    }
                    EstadoForm.cantidadRegistrosProcesado++;
                }
          
                foreach (var datos in listadoGrupoFechasNotificacion.Item1)
                {
                    if (datos != null && EstadoForm.procesarDatos == true)
                    {
                        var fechaInicio = Convert.ToDateTime(datos);
                        DateTime fechaFinal = new DateTime();

                        var selecionFecha = fechasProcesadas.Where(x => x.fechaInicio == fechaInicio).FirstOrDefault();
                        if (selecionFecha != null)
                        {
                            fechaFinal = Convert.ToDateTime(selecionFecha.fechaFinal);
                        }
                        else
                        {
                            fechaFinal = obteneterFechaFinal(fechaInicio, fechasSuspencion);
                        }

                        DbMesNuevo.ActualizarResultadoFechasFinalesNotificacion(fechaInicio, fechaFinal, true, mesActual);

                    }
                    EstadoForm.cantidadRegistrosProcesado++;
                }
            }

            return resultado;
        }

        private static DateTime obteneterFechaFinal(DateTime? pFechaInicio, List<FechasSuspencion> fechasSuspencion)
        {
            var fechaInicio = Convert.ToDateTime(pFechaInicio);

            DateTime fechaFinal = fechaInicio;
            var cantidadDiasSumaAnios = ((fechaInicio.AddYears(3)) - fechaInicio).Days;

                while (cantidadDiasSumaAnios > 0)
                {
                    fechaFinal = fechaFinal.AddDays(1);

                    var esFechaSuspencion = fechasSuspencion.Where(x => x.fecha == fechaFinal).FirstOrDefault();

                    if (esFechaSuspencion == null)
                    {
                        cantidadDiasSumaAnios = cantidadDiasSumaAnios - 1;
                    }

                    if (EstadoForm.procesarDatos == false)
                    {
                        break;
                    }
                }         

            return fechaFinal;
     
        }

        public static void GenerarCsv(List<object[]> datos,String nombreArchivo, List<object[]> columnas)
        {
            StreamWriter sw = new StreamWriter("datos.csv");
            var resColumnas = "";

            foreach (var item in columnas)
            {
                resColumnas += (string)item[0] + ";";
               // sw.WriteLine(string.Join(";", item));
            }

            sw.WriteLine(resColumnas);

            foreach (var item in datos)
            {
                sw.WriteLine(string.Join(";", item));
            }
             sw.Close();

            File.Move("datos.csv", nombreArchivo);
        }

        public static Tuple<string[], int[]> generarGrafico(string columna,string mesActual)
        {
            var dt = DbGeneral.obtenerDatosDatables("select " + columna + ", COUNT(*) [Total] from " + mesActual + " group by  " + columna);
            Tuple<string[], int[]> listadoDatos = null;

            //Get the names of Cities.
            string[] x = (from p in dt.AsEnumerable()
                          orderby p.Field<string>(columna) ascending
                          select p.Field<string>(columna)).ToArray();

            //Get the Total of Orders for each City.
            int[] y = (from p in dt.AsEnumerable()
                       orderby p.Field<string>(columna) ascending
                       select p.Field<int>("Total")).ToArray();

            listadoDatos = Tuple.Create(x, y);
            return listadoDatos;
        }

        public static Tuple<int, string, Boolean> obtnerTotalRegistro(String mes)
        {
            var res= DbGeneral.obtenerCantidades("select  COUNT(*) [Total] from " + mes);

            return res;
        }

        public static Tuple<double, string, Boolean> obtnerTotalSaldo(String mes)
        {
            var res = DbGeneral.obtenerSaldo("select  sum(saldo) [Total] from " + mes);

            return res;
        }

        //modificar el nombre que empiecen o termine en un caracter en blanco
        public static void renombrarColumnas(String mes)
        {
            var nombresColumnas = DbGeneral.obtenerNombresColumnas(mes);

            foreach (var item in nombresColumnas.Item1)
            {
                var tempVerIni = item[0].ToString().Substring(0, 1);

                var largo = item[0].ToString().Length-1;
                var tempVerFin = item[0].ToString().Substring(largo, 1);

                if (tempVerIni.Trim().Length==0 || tempVerFin.Trim().Length == 0)
                {
                    var query= "EXEC sp_RENAME '"+mes+"."+item[0].ToString()+ "', '" + item[0].ToString().Trim() + "', 'COLUMN'";
                    DbGeneral.EjecutarQuery(query);
                }
            }
        }

    }
}
