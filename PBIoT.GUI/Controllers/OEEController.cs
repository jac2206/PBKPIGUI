using PBIoT.GUI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PBIoT.GUI.Controllers
{
    public class OEEController : Controller
    {
        // GET: OEE
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult usp_ObtenerGreaficaVariablesOEEXHora(string fecha)
        {
            try
            {
                using (var DB = new PBIoTDBEntities())
                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    // var viewModel = new List<usp_ObtenerReportePromedioEjecucion_Result>();
                    var viewModel = new List<uspObtenerVariablesOeeHora_Result>();
                    var resultado = DB.uspObtenerVariablesOeeHora(Convert.ToDateTime(fecha)).ToList();
                    foreach (var resultadoItem in resultado)
                    {
                        viewModel.Add(resultadoItem);
                    }

                    return Json(viewModel);

                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }
        }

        public ActionResult usp_ObtenerGreaficaOEEXHora(string fecha)
        {
            try
            {
                using (var DB = new PBIoTDBEntities())
                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    // var viewModel = new List<usp_ObtenerReportePromedioEjecucion_Result>();
                    var viewModel = new List<uspObtenerOEEXHora_Result>();
                    var resultado = DB.uspObtenerOEEXHora(Convert.ToDateTime(fecha)).ToList();
                    foreach (var resultadoItem in resultado)
                    {
                        viewModel.Add(resultadoItem);
                    }

                    return Json(viewModel);

                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(550, "Some error" + ex.Message);
            }
        }

    }
}