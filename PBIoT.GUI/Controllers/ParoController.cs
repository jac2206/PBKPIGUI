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
    public class ParoController : Controller
    {
        // GET: Paro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerParosXDia([DataSourceRequest]DataSourceRequest request, string fecha)
        {
            try
            {
                using (var DB = new PBIoTDBEntities())
                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    // var viewModel = new List<usp_ObtenerReportePromedioEjecucion_Result>();
                    var viewModel = new List<uspObtenerInformacionParosXDia_Result>();
                    var resultado = DB.uspObtenerInformacionParosXDia(Convert.ToDateTime(fecha)).ToList();
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

        public ActionResult ObtenerTop5ParosXDia([DataSourceRequest] DataSourceRequest request, string fecha)
        {
            try
            {
                using (var DB = new PBIoTDBEntities())
                {

                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                    var resultado = DB.uspObtenerTop5ParosXDia(Convert.ToDateTime(fecha)).ToList();
                    List<Object> viewModel = new List<object>();
                    foreach (var resultadoItem in resultado)
                    {
                        viewModel.Add(new
                        {
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

    }
}