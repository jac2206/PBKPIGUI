using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PBIoT.GUI.Models;
using Kendo.Mvc.Extensions;

namespace PBIoT.GUI.Controllers
{
    public class ParoPendienteController : Controller
    {
        // GET: ParoPendiente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerTodosParosFinalizar([DataSourceRequest] DataSourceRequest request)
        {
            try
            {

                using (var DB = new PBIoTDBEntities())
                {


                    IQueryable<uspObtenerTodosParosPendientesFinalizar_Result> obtenerParosXFinalizar = (IQueryable<uspObtenerTodosParosPendientesFinalizar_Result>)DB.uspObtenerTodosParosPendientesFinalizar().AsQueryable();
                    DataSourceResult result = obtenerParosXFinalizar.ToList().ToDataSourceResult(request);
                    return Json(result);

                }

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }

        }


        [AcceptVerbs(HttpVerbs.Post)]
        //[HttpPost]
        public ActionResult ParosPendientesFinalizar([DataSourceRequest]DataSourceRequest request)
        {
            {
                try
                {

                    using (var DB = new PBIoTDBEntities())
                    {


                        //var resultado = DB.uspObtenerParoPorFinalizar().ToList();
                        //List<Object> viewModel = new List<object>();
                        var resultado = DB.uspObtenerParoPorFinalizar().FirstOrDefault();
                        Object viewModel = new
                        {

                            IdParo = resultado.IdParo,
                            Inicio = resultado.Inicio.ToString(),
                            Fin = resultado.Fin.ToString(),


                        };

                        //DataSourceResult result = viewModel.ToDataSourceResult(request);
                        //var jsonresult = Json(result);
                        //return Json(result);
                        return Json(viewModel, JsonRequestBehavior.AllowGet);

                    }




                }

                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(550, "Some error" + ex.Message);
                }

            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GuardarParoPendiente([DataSourceRequest] DataSourceRequest request, string IdParo, string CodigoParo)
        {
            try
            {
                using (var DB = new PBIoTDBEntities())
                {


                    //referenciasEdit2.Id = referenciasEdit.Id;

                    DB.uspGuardarParoPendientePorFinalizar(
                        Convert.ToInt64(IdParo),
                        CodigoParo);
                    DB.SaveChanges();


                    return Json("Guardo exitosamente el paro pendiente por finalizar", JsonRequestBehavior.AllowGet);

                }
                //return Json("sisas", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //return Json("No Guardo el paro pendiente por finalizar", JsonRequestBehavior.AllowGet);
                return Content(ex.Message);
            }

        }

    }
}