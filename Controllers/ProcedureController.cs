using Katalog_v_2.Models;
using Katalog_v_2.Models.Interface;
using Katalog_v_2.Service.BDService;
using Katalog_v_2.Service.FileService;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace Katalog_v_2.Controllers
{
    public class ProcedureController : Controller
    {
        private readonly IProcedure _service;

        public ProcedureController()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["BD"]) == true)
            {
                _service = new ProcedureService();
            }
            else
            {
                _service = new ProcedureFileService();

            }
        }

        [HttpGet]
        public ActionResult Procedures()
        {
            var list = _service.GetList();
            ViewBag.Procedures = list;
            return View();
        }


        [HttpGet]
        public ActionResult Zakaz(string ZakazName, int procId)
        {
            
            Zakaz zakazModel = _service.GetZakaz(ZakazName, procId);
            ViewBag.Zakazs = zakazModel;
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddZakaz(int Id)
        {
            var Procedure = _service.GetElement(Id);
            ViewBag.Procedure = Procedure;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddZakaz(Zakaz zakazModel, HttpPostedFileBase imagedata = null)
        {
            zakazModel.ZakazData = DateTime.Now.ToLongDateString();
            if (imagedata != null)
            {
                zakazModel.ImageMimeType = imagedata.ContentType;
                zakazModel.Image = new byte[imagedata.ContentLength];
                imagedata.InputStream.Read(zakazModel.Image, 0, imagedata.ContentLength);
            }
            _service.AddZakaz(zakazModel);
            return RedirectToAction("Procedures", "Procedure");
        }


        public FileContentResult GetImage(string Name, int procId)
        {
            Zakaz zakazModel = _service.GetZakaz(Name, procId);
            if (zakazModel != null)
            {
                return File(zakazModel.Image, zakazModel.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}