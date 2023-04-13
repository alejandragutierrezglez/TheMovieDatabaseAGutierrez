using Microsoft.AspNetCore.Mvc;

namespace TheMovieDatabaseAGutierrez.Controllers
{
    public class EstadisticaController : Controller
    {
		[HttpGet]
		public ActionResult GetAll()
        {
            ML.Result result = BL.Estadistica.GetAllTotal();
            ML.Cine cine = new ML.Cine();
            cine.Estadistica = new ML.Estadistica();

            if (result.Correct)
            {

                cine.Estadistica.TotalVentas = result.Objects;
                decimal porcentaje = 0;
                ML.Estadistica resultPorcentaje = BL.Estadistica.CalcularPorcentaje();
                ML.Result result1 = BL.Cine.GetAll();
                cine.Cines = result1.Objects;
                return View(cine);
            }
            else
            {
                return View(cine.Estadistica);
            }
        }
    }
}
