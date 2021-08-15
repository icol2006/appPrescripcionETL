﻿using AppBot.Modelos;
using appEtlPrescripcion.Dto;
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

            List<FechasSuspencion> fechasSuspencion = DbFechasSuspencion.Listar();
            List<List<string>> resultado = new List<List<string>>();
            List<FechasProcesada> fechasProcesadas = new List<FechasProcesada>();

            EstadoForm.totalRegistros = listadoGrupoFechasComparendo.Count() + listadoGrupoFechasNotificacion.Count();

            foreach (var datos in listadoGrupoFechasComparendo)
            {
                if (datos != null) { 
                    var fechaInicio = Convert.ToDateTime(datos);
                    DateTime fechaFinal = DateTime.Now;
                    if (fechaInicio>Convert.ToDateTime("2017-01-01"))
                    {
                        fechaFinal = obteneterFechaFinal(fechaInicio, fechasSuspencion);
                        DbMesNuevo.ActualizarResultadoFechasFinalesComparendo(fechaInicio, fechaFinal, true);
                    }
                                      
            

                    fechasProcesadas.Add(new FechasProcesada()
                    {
                        fechaInicio=fechaInicio,
                        fechaFinal=fechaFinal
                    });

                    if (EstadoForm.procesarDatos == false)
                    {
                        break;
                    }

                }
                EstadoForm.cantidadRegistrosProcesado++;

            }

            foreach (var datos in listadoGrupoFechasNotificacion)
            {
                if (datos != null)
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

                    DbMesNuevo.ActualizarResultadoFechasFinalesNotificacion(fechaInicio, fechaFinal, true);

          
                }
                EstadoForm.cantidadRegistrosProcesado++;
            }

            return resultado;
        }

        private static DateTime obteneterFechaFinal(DateTime? pFechaInicio, List<FechasSuspencion> fechasSuspencion)
        {
            var fechaInicio = Convert.ToDateTime(pFechaInicio);

            DateTime fechaFinal = fechaInicio;
            var cantidadDiasSumaAnios = ((fechaInicio.AddYears(3)) - fechaInicio).Days;
            //cantidadDiasSumaAnios--;

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
