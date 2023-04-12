using Microsoft.AspNetCore.Mvc;

namespace TheMovieDatabaseAGutierrez.Controllers
{
    public class EstadisticaController : Controller
    {
		[HttpGet]
		public ActionResult GetAll()
        {            
            return View();
        }
    }
}
