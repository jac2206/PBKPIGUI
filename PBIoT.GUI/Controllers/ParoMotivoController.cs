using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PBIoT.GUI.Models;

namespace PBIoT.GUI.Controllers
{
    public class ParoMotivoController : Controller
    {
        // GET: ParoMotivo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerParosMotivoXMes([DataSourceRequest] DataSourceRequest request, string fecha)
        {
            try
            {
                using (var DB = new PBIoTDBEntities())
                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    var resultado = DB.uspObtenerParosMotivoXMes(Convert.ToDateTime(fecha)).ToList();
                    List<Object> viewModel = new List<object>();
                    foreach (var resultadoItem in resultado)
                    {
                        viewModel.Add(new
                        {
                            IdParo = resultadoItem.IdParo,
                            Motivo = resultadoItem.Motivo,
                            Descripcion = resultadoItem.Descripcion,
                            Inicio = resultadoItem.Inicio,
                            Fin = resultadoItem.Fin,
                            TotalTiempo = resultadoItem.TotalTiempo

                        });
                    }

                    DataSourceResult result = viewModel.ToDataSourceResult(request);
                    var jsonresult = Json(result);
                    return Json(result);

                }

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ActualizarParoMotivo([DataSourceRequest] DataSourceRequest request, uspObtenerParosMotivoXMes_Result paroMotivoEdit)
        {
            try
            {
                using (var DB = new PBIoTDBEntities())
                {

                    if (paroMotivoEdit != null)
                    {

                        DB.uspActualizarMotivoParo(
                            paroMotivoEdit.IdParo
                            , paroMotivoEdit.Motivo

                            );
                        //DB.Receta.Attach(referenciasEdit);
                        //DB.Entry(referenciasEdit).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();
                    }

                    //return Json(new[] { paroMotivoEdit }.ToDataSourceResult(request, ModelState));
                    return Json(true, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }

        }
    }
}