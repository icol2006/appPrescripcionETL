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
        public static String realizarConversion(String mesActual,String mesAnterior)
        {
            String resultado = "";
            renombrarColumnas(mesActual);

            var query = @"IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'ID') BEGIN ALTER TABLE " + mesActual + " ADD ID varchar(255) END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesAnterior + "') AND name = 'ID') BEGIN ALTER TABLE " + mesAnterior + " ADD ID varchar(255) END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'fecha_comparendo') BEGIN ALTER TABLE " + mesActual + " ADD fecha_comparendo date END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'fecha_notificacion') BEGIN ALTER TABLE " + mesActual + " ADD fecha_notificacion date END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'res_fecha_comparendo') BEGIN ALTER TABLE " + mesActual + " ADD res_fecha_comparendo date END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'res_fecha_notificacion') BEGIN ALTER TABLE " + mesActual + "  ADD res_fecha_notificacion date END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'res_fecha_comparendo_formato') BEGIN ALTER TABLE " + mesActual + " ADD res_fecha_comparendo_formato varchar(255) END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'res_fecha_notificacion_formato') BEGIN ALTER TABLE " + mesActual + "  ADD res_fecha_notificacion_formato varchar(255) END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'prescrito') BEGIN ALTER TABLE " + mesActual + "  ADD prescrito varchar(10) END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'fc_procesada') BEGIN ALTER TABLE " + mesActual + "  ADD fc_procesada bit END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'fn_procesada') BEGIN ALTER TABLE " + mesActual + "  ADD fn_procesada bit END;" + System.Environment.NewLine +
                "update " + mesActual + " set FECHA_COMPARENDO=convert(datetime, FECHA#COMPARENDO, 103) WHERE FECHA#COMPARENDO LIKE '%/%';" + System.Environment.NewLine +
                "update " + mesActual + " set FECHA_NOTIFICACION=convert(datetime, FECHA#notificacion, 103) WHERE FECHA#notificacion LIKE '%/%'" + System.Environment.NewLine +
                "update  " + mesActual + " set id=(NRO#DOCUMENTO+COMPARENDO+FECHA#COMPARENDO);" + System.Environment.NewLine +
                "update  " + mesAnterior + " set id=(NRO#DOCUMENTO+COMPARENDO+FECHA#COMPARENDO);" + System.Environment.NewLine +
                "IF  EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'num') BEGIN ALTER TABLE " + mesActual + "  DROP COLUMN num END;" + System.Environment.NewLine +
                "IF NOT EXISTS (SELECT * FROM   sys.columns WHERE  object_id = OBJECT_ID(N'" + mesActual + "') AND name = 'num') BEGIN ALTER TABLE " + mesActual + "  Add num Int Identity(1, 1) END;" + System.Environment.NewLine +
                "drop table repetidos" + System.Environment.NewLine +
                "select * into repetidos from " + mesActual + " where id in (select  id from " + mesActual + " group by id HAVING count(*) >1)" + System.Environment.NewLine +
                "DELETE FROM repetidos WHERE num NOT IN  (SELECT MIN(num) FROM repetidos GROUP BY id)" + System.Environment.NewLine +
                "DELETE FROM " + mesActual + " WHERE num NOT IN  (SELECT MIN(num) FROM " + mesActual + " GROUP BY id);" + System.Environment.NewLine +
                "IF  EXISTS (SELECT * FROM sys.columns WHERE  object_id = OBJECT_ID(N'nuevos')) BEGIN drop table nuevos END;" + System.Environment.NewLine +
                "select * into nuevos from " + mesActual + " where ID in(select p2.ID from " + mesAnterior + " p1 right join " + mesActual + " p2 on p1.ID=p2.ID where p1.ID is null);";

            var listadoQuerys = query.Split(new[] { Environment.NewLine },StringSplitOptions.None);

            foreach (var item in listadoQuerys)
            {
               var res= DbGeneral.EjecutarQuery(item);
                if(res.Item2==false)
                {
                    resultado += res.Item1 + System.Environment.NewLine;
                }
            }

            return resultado;            
        }

        public static Tuple<string, Boolean> verificarColumnas(String mes)
        {
            String resultado = "";
            Boolean existeError = false;
            List<String> nombresColumnas = new List<string>();
            Tuple<string, Boolean> listadoDatos = null;

            //construir query para insertar los registros nuevos en la tabla de nuevos
            var datosColumnas = DbGeneral.obtenerNombresColumnas(mes);

            foreach (var item in datosColumnas.Item1)
            {
                nombresColumnas.Add(item[0].ToString().ToUpper());
            }

            resultado= nombresColumnas.IndexOf("NRO#DOCUMENTO") > -1 ? "" : "Columna NRO#DOCUMENTO no existe" + Environment.NewLine;
            resultado+= nombresColumnas.IndexOf("COMPARENDO") > -1 ? "" : "Columna COMPARENDO no existe" + Environment.NewLine;
            resultado+= nombresColumnas.IndexOf("FECHA#COMPARENDO") > -1 ? "" : "Columna FECHA#COMPARENDO no existe" + Environment.NewLine;
            resultado+= nombresColumnas.IndexOf("FECHA#NOTIFICACION") > -1 ? "" : "Columna FECHA#NOTIFICACION no existe" + Environment.NewLine;
           
            existeError = resultado.Trim().Length > 0 ? true : false;
            listadoDatos = Tuple.Create(resultado, existeError);

            return listadoDatos;
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

            String queryActualizacionFechas = @"update "+mesActual+ @" set prescrito=null;
                                           update " + mesActual + @" set prescrito='si'  where fecha_notificacion is null and res_fecha_comparendo<'2021-08-31';
                                           update " + mesActual + @" set prescrito='no' where fecha_notificacion is null and res_fecha_comparendo>'2021-08-31';
                                           update " + mesActual + @" set prescrito='si' where fecha_notificacion is not null and fecha_comparendo is not null and res_fecha_comparendo<fecha_notificacion and res_fecha_comparendo<'2021-08-31';
                                           update " + mesActual + @" set prescrito='no' where fecha_notificacion is not null and fecha_comparendo is not null and res_fecha_comparendo<fecha_notificacion and res_fecha_comparendo>'2021-08-31';
                                           update " + mesActual + @" set prescrito='si' where fecha_notificacion is not null and fecha_comparendo is not null and res_fecha_comparendo>fecha_notificacion and res_fecha_notificacion<'2021-08-31';
                                           update " + mesActual + @" set prescrito='no' where fecha_notificacion is not null and fecha_comparendo is not null and res_fecha_comparendo>fecha_notificacion and res_fecha_notificacion>'2021-08-31';
                                           update " + mesActual + @" set res_fecha_comparendo_formato=CONVERT(nvarchar, res_fecha_comparendo,103);
                                           update " + mesActual + @" set res_fecha_notificacion_formato=CONVERT(nvarchar, res_fecha_notificacion,103);";

            DbGeneral.EjecutarQuery(queryActualizacionFechas);
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

        public static Tuple<decimal, string, Boolean> obtnerTotalSaldo(String mes)
        {
            var res = DbGeneral.obtenerSaldo("select  sum(saldo) [Total] from " + mes);

            return res;
        }

        public static Tuple<decimal, string, Boolean> obtnerTotalSaldoEtapaCartera(String mes)
        {
            var res = DbGeneral.obtenerSaldo("select  sum(saldo) [Total] from " + mes + "  WHERE ETAPA='CARTERA'");

            return res;
        }


        public static Tuple<decimal, string, Boolean> obtnerTotalSaldoEtapaContravencional(String mes)
        {
            var res = DbGeneral.obtenerSaldo("select  sum(saldo) [Total] from "+ mes + "  WHERE ETAPA<>'CARTERA'");

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
