using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TheMovieDatabaseAGutierrez.Models;

namespace TheMovieDatabaseAGutierrez.Controllers
{
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public MovieController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public ActionResult GetPopularMovies()
        {
            Models.Movie movie = new Models.Movie();
            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("movie/popular?api_key=eba03a37a4b8824ee1a58415b544159d&language=es-ES&page=1");
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsStringAsync();
                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    movie.Movies = new List<object>();
                    foreach (var resultItem in resultJson.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.IdMovie = resultItem.id;
                        movieItem.Descripcion = resultItem.overview;
                        movieItem.Titulo = resultItem.title;
                        movieItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;

                        movie.Movies.Add(movieItem);
                    }
                }
            }
            return View(movie);
        }


        public ActionResult AddFavoriteMovie(int? IdMovie, int IdAccion)
        {

            Models.Movie movie = new Models.Movie();
            movie.media_id = IdMovie.Value;
            movie.media_type = "movie";
            movie.favorite = true;

            if (IdAccion == 1)
            {
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var postTask = client.PostAsJsonAsync<Models.Movie>("account/18702691/favorite?api_key=eba03a37a4b8824ee1a58415b544159d&session_id=f09a94b6482162cd7b22b099853a92573747326b", movie);
                    postTask.Wait();

                    var resultServicio = postTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha agregado a Favoritos";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha agregado a Favoritos";
                        return PartialView("Modal");
                    }

                }

            }
            if (IdAccion == 0)
            {
                Models.Movie movie1 = new Models.Movie();
                movie1.media_id = IdMovie.Value;
                movie1.media_type = "movie";
                movie1.favorite = false;

                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var postTask = client.PostAsJsonAsync<Models.Movie>("account/18702691/favorite?api_key=eba03a37a4b8824ee1a58415b544159d&session_id=f09a94b6482162cd7b22b099853a92573747326b", movie1);
                    postTask.Wait();

                    var resultServicio = postTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha eliminado de Favoritos";
                        return PartialView("ModalFav");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha eliminado de Favoritos";
                        return PartialView("ModalFav");
                    }

                }

            }

            return View(movie);
        }

        //public ActionResult DeleteFavoriteMovie(int? IdMovie)
        //{
        //    Models.Movie movie = new Models.Movie();
        //    movie.media_id = IdMovie.Value;
        //    movie.media_type = "movie";
        //    movie.favorite = false;

        //    if (IdMovie != null)
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            string urlApi = _configuration["urlApi"];
        //            client.BaseAddress = new Uri(urlApi);

        //            var postTask = client.PostAsJsonAsync<Models.Movie>("account/18702691/favorite?api_key=eba03a37a4b8824ee1a58415b544159d&session_id=f09a94b6482162cd7b22b099853a92573747326b", movie);
        //            postTask.Wait();

        //            var resultServicio = postTask.Result;

        //            if (resultServicio.IsSuccessStatusCode)
        //            {
        //                ViewBag.Message = "Se ha eliminado de favoritos";
        //                return PartialView("ModalFav");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "No se ha eliminado de favoritos";
        //                return PartialView("ModalFav");
        //            }

        //        }

        //    }

        //    return View(movie);
        //}

        public ActionResult GetFavoriteMovie()
        {
            Models.Movie movie = new Models.Movie();
            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlApi"];
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("account/18702691/favorite/movies?api_key=eba03a37a4b8824ee1a58415b544159d&session_id=f09a94b6482162cd7b22b099853a92573747326b&language=es-ES&sort_by=created_at.asc&page=1");
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsStringAsync();
                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();
                    movie.Movies = new List<object>();
                    foreach (var resultItem in resultJson.results)
                    {
                        Models.Movie movieItem = new Models.Movie();
                        movieItem.IdMovie = resultItem.id;
                        movieItem.Descripcion = resultItem.overview;
                        movieItem.Titulo = resultItem.title;
                        movieItem.Imagen = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;

                        movie.Movies.Add(movieItem);
                    }
                }
            }
            return View(movie);

        }
    }
}
