using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
                {
                    var query = context.Cines.FromSqlRaw($"CineGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine = obj.IdCine;
                            cine.Latitud = obj.Latitud.Value;
                            cine.Longitud = obj.Longitud.Value;
                            cine.Direccion = obj.Direccion;
                            cine.Venta = obj.Venta.Value;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = obj.Zona.Value;
                            cine.Zona.Nombre = obj.NombreZona;
                            result.Objects.Add(cine);
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

        public static ML.Result GetById(int IdCine)
        {
            ML.Result result = new ML.Result();
            {
                try
                {
                    using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
                    {
                        var query = context.Cines.FromSqlRaw($"CineGetById {IdCine}").AsEnumerable().FirstOrDefault();

                        if (query != null)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine = query.IdCine;
                            cine.Latitud = query.Latitud.Value;
                            cine.Longitud = query.Longitud.Value;
                            cine.Direccion = query.Direccion;
                            cine.Venta = query.Venta.Value;

                            cine.Zona = new ML.Zona();
                            cine.Zona.IdZona = query.Zona.Value;
                            cine.Zona.Nombre = query.NombreZona;

                            result.Object = cine;

                            result.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Ex = ex;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            }

        }

        public static ML.Result Update(ML.Cine cine)
        {
            using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
            {
                Result result = new ML.Result();
                try
                {

                    //var query = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.UserName, usuario.Passwordd, DateTime.Parse(usuario.FechaNacimiento), usuario.Rol.IdRol, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    var query = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Latitud}', '{cine.Longitud}','{cine.Direccion}','{cine.Venta}',{cine.Zona.IdZona}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
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

        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
                {
                    // var query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.UserName, usuario.Passwordd, DateTime.Parse(usuario.FechaNacimiento), usuario.Rol.IdRol, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    var query = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Latitud}', '{cine.Longitud}', '{cine.Direccion}','{cine.Venta}',{cine.Zona.IdZona}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int IdCine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineDelete {IdCine}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
