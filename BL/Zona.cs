using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
                {
                    var query = context.Zonas.FromSqlRaw("ZonaGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Zona zona = new ML.Zona();
                            ML.Cine cine = new ML.Cine();

                            zona.IdZona = obj.IdZona;
                            zona.Nombre = obj.Nombre;
                            cine.Venta = obj.VENTA_POR_ZONA;

                            result.Objects.Add(zona);
                        }
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
            return result;
        }
    }
}
