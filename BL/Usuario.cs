using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetByUsername(string username)
        {
            ML.Result result = new ML.Result();
            {
                try
                {
                    using (DL.AgutierrezCineContext context = new DL.AgutierrezCineContext())
                    {
                        var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUsername {username}").AsEnumerable().FirstOrDefault();

                        if (query != null)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.Username = query.Username;
                            usuario.Password = query.Password;


                            result.Object = usuario;

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
    }
}