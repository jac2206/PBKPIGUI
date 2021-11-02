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
    public class GraficarVariablesController : Controller
    {
        // GET: GraficarVariables
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult usp_ObtenerGreaficaVariablesXMinuto(string fecha)
        {
            try
            {
                using (var DB = new PBIoTDBEntities())
                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    // var viewModel = new List<usp_ObtenerReportePromedioEjecucion_Result>();
                    var viewModel = new List<uspObtenerVariablePLCGraficaXMinuto_Result>();
                    var resultado = DB.uspObtenerVariablePLCGraficaXMinuto(Convert.ToDateTime(fecha)).ToList();
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