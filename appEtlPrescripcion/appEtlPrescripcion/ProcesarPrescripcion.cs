using AppBot.Modelos;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appEtlPrescripcion
{
    public static class ProcesarPrescripcion
    {

        public static List<List<string>> procesarDatos()
        {
            var listadoDatos = DbMesNuevo.Listar();
            var fechasSuspencion = DbFechasSuspencion.Listar();
            List<List<string>> resultado = new List<List<string>>();
            List<String> resItem = new List<string>();

            EstadoForm.totalRegistros = listadoDatos.Count();

            foreach (var datos in listadoDatos)
            {
                var fechaInicio = Convert.ToDateTime(datos[1]);
        
                DateTime fechaActual = fechaInicio;
                var cantidadDiasSumaAnios = ((fechaInicio.AddYears(3)) - fechaInicio).Days;

                if (resultado.Count() > 20000)
                {
                    cantidadDiasSumaAnios = 0;
                }

                while (cantidadDiasSumaAnios>0)
                {
                    fechaActual = fechaActual.AddDays(1);

                    var esFechaSuspencion = fechasSuspencion.Where(x => x.fecha == fechaActual).FirstOrDefault();

                    if (esFechaSuspencion==null)
                    {
                        cantidadDiasSumaAnios = cantidadDiasSumaAnios - 1;
                    }                  
                }
               
 
                    resItem.Add(fechaInicio.ToString("dd/MM/yyyy"));
                    resItem.Add(fechaActual.ToString("dd/MM/yyyy"));
                    resultado.Add(resItem);
                    resItem = new List<string>();
                EstadoForm.cantidadRegistrosProcesado = resultado.Count();
    
            
            }

            return resultado;
        }
    }
}
