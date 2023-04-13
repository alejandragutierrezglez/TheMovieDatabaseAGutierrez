using DL;
using Microsoft.AspNetCore.Mvc;

namespace TheMovieDatabaseAGutierrez.Controllers
{
    public class CineController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Cine cine = new ML.Cine();

            ML.Result result = BL.Cine.GetAll();
            ML.Result resultZona = BL.Zona.GetAll();
            //ML.Result resultTotalVentas = BL.Estadistica.TotalVentas();

            cine.Zona = new ML.Zona();
           
            

            if (result.Correct && resultZona.Correct)
            {
                cine.Cines = result.Objects;
                cine.Zona.Zonas = resultZona.Objects;
               
                return View(cine);
            }
            else
            {
                return View(cine);
            }
        }

        [HttpGet]
        public ActionResult Form(int IdCine)
        {
            ML.Result resultZona = BL.Zona.GetAll();

            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();


            if (resultZona.Correct)
            {

                cine.Zona.Zonas = resultZona.Objects;
            }
            if (IdCine == 0)
            {
                //add //formulario vacio
                return View(cine);
            }
            else
            {
                //getById
                ML.Result result = BL.Cine.GetById(IdCine); //2

                if (result.Correct)
                {
                    cine = (ML.Cine)result.Object;//unboxing
                    cine.Zona.Zonas = resultZona.Objects;
                    //update
                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "Ocurrio al consultar la información del cine";
                    return View("Modal");
                }
            }
        }

        [HttpPost] //Hacer el registro
        public ActionResult Form(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            if (cine.IdCine != null)
            {
                result = BL.Cine.Update(cine);
                ViewBag.Message = "Se ha modificado el registro";
            }
            else
            {
                result = BL.Cine.Add(cine);
                ViewBag.Message = "Se ha agregado el registro";
            }
            if (result.Correct)
            {
                return PartialView("Modal");
            }
            else
            {
                return PartialView("Modal");
            }
        }

        public ActionResult Delete(int IdCine)
        {
            ML.Result result = BL.Cine.Delete(IdCine);

            if (result.Correct)
            {
                ViewBag.Message = "Se ha eliminado el cine";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se ha podido eliminar el registro del cine seleccionado" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }

    }
}
