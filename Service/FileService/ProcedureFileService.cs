using Katalog_v_2.Models;
using Katalog_v_2.Models.Interface;
using System;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Katalog_v_2.Service.FileService
{
    public class ProcedureFileService: AbstractFileService, IProcedure
    {
        new string Name = "Procedure";
        new string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/Procedure";
        new XmlSerializer xsSubmit = new XmlSerializer(typeof(Procedure));

        public ProcedureFileService()
        {
            base.Name = Name;
            base.xsSubmit = xsSubmit;
            base.currentPath = currentPath;
        }

        public void AddZakaz(Zakaz zakaz) {
            Procedure procedure =(Procedure)base.GetElement(zakaz.procId);
            if (procedure.Zakazs.Find(rec => rec.Name.Equals(zakaz.Name)) != null) {
                throw new Exception("Уже есть заказ с таким названием");
            }
            else
            {
                zakaz.procId = procedure.Id;
                procedure.Zakazs.Add(zakaz);
                UpdateElement(procedure);
            }
        }

        public Zakaz GetZakaz(string name, int Procedure_id)
        {
            Procedure procedure  = (Procedure)base.GetElement(Procedure_id);
            Zakaz zakaz = procedure.Zakazs.FirstOrDefault(rec => rec.Name == name);
            return zakaz;
        }
    }
}