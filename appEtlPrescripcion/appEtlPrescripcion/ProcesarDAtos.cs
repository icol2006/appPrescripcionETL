using AppBot.Modelos;
using CapaDatos;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appEtlPrescripcion
{
    public static class ProcesarDAtos
    {
        public static void realizarConversion()
        {
            DbMesNuevo.ConversionDatos();
        }

        public static List<List<string>> procesarFechas()
        {
            var listadoGrupoFechasComparendo = DbMesNuevo.ObtenerGruposFechaComparendo();
            var listadoGrupoFechasNotificacion=DbMesNuevo.ObtenerGruposFechaNotificacion();
            int contadorProceso = 0;

            List<FechasSuspencion> fechasSuspencion = DbFechasSuspencion.Listar();
            List<List<string>> resultado = new List<List<string>>();
            List<String> resItem = new List<string>();

            EstadoForm.totalRegistros = listadoGrupoFechasComparendo.Count() + listadoGrupoFechasNotificacion.Count();

            foreach (var datos in listadoGrupoFechasComparendo)
            {
                var fechaInicio = Convert.ToDateTime(datos);
                var fechaFinal=  obteneterFechaFinal(fechaInicio, fechasSuspencion);
                contadorProceso++;
                EstadoForm.cantidadRegistrosProcesado = contadorProceso;

                DbMesNuevo.ActualizarResultadoFechasFinalesComparendo(fechaInicio, fechaFinal, true);

                if (EstadoForm.procesarDatos == false)
                {
                    break;
                }
            }

            foreach (var datos in listadoGrupoFechasNotificacion)
            {
                var fechaInicio = Convert.ToDateTime(datos);
                var fechaFinal = obteneterFechaFinal(fechaInicio, fechasSuspencion);
                contadorProceso++;
                EstadoForm.cantidadRegistrosProcesado = contadorProceso;
                DbMesNuevo.ActualizarResultadoFechasFinalesNotificacion(fechaInicio, fechaFinal, true);

                if (EstadoForm.procesarDatos == false)
                {
                    break;
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
    }
}
