using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estadistica
    {
        public static decimal CalcularPorcentaje(decimal porcentaje)
        {
            ML.Result result = new ML.Result();
            try
            {
                int totalVentas = TotalVentas();
                porcentaje = 0;
                ML.Cine cine = new ML.Cine();
                porcentaje = cine.Venta * 100 / totalVentas;
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;
                result.Ex = ex;

            }
            return porcentaje;
        }

        public static int TotalVentas()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
                {
                    var query = context.Cines.FromSqlRaw($"TotalVentas").ToList();

                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();
                        result.Objects.Add(cine);
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                result.Correct = false;

            }
            return TotalVentas();
        }

        public static ML.Result GetAllTotal()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
                {
                    var query = context.Zonas.FromSqlRaw($"GetAllTotalVentas").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var resultado in query)
                        {
                            ML.Cine cinezona = new ML.Cine();

                            ML.Zona zona = new ML.Zona();

                            cinezona.Venta = (decimal)resultado.VENTA_POR_ZONA;
                            cinezona.Zona = new ML.Zona();
                                                     
                            cinezona.Zona.IdZona = (int)resultado.IdZona;
                            cinezona.Zona.Nombre = resultado.Nombre;                         


                            result.Objects.Add(cinezona);

                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }

                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.Ex = e;
                result.ErrorMessage = e.Message;

            }
            return result;
        }
    }
}
