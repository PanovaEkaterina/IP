using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Katalog_v_2.Models;
using Katalog_v_2.Models.Abstract;
using Katalog_v_2.Models.Interface;

namespace Katalog_v_2.Service.BDService
{
    public class ProcedureService : AbstractDbService<Procedure>, IProcedure
    {
        private dbContext context = new dbContext();

        public override DbSet<Procedure> Pata { get { return context.Procedures; } }

        public override dbContext Сontext { get { return context; } }

        public override List<AModel> GetList()
        {
            List<AModel> amodel = new List<AModel>();
            List<Procedure> Procedures = context.Procedures.ToList();
            List < Zakaz > zakaz= context.Zakazs.ToList();
            foreach (Procedure procedure in Procedures) {
                procedure.Zakazs = zakaz.FindAll(rec => rec.procId == procedure.Id);
                amodel.Add(procedure);
            }
            return amodel;
        }

        public void AddZakaz(Zakaz zakaz) {
            Procedure procedure = context.Procedures.Find(zakaz.procId);
            if (procedure.Zakazs == null) {
                procedure.Zakazs = new List<Zakaz>();
            }
            procedure.Zakazs.Add(zakaz);
            context.SaveChanges();
        }

        public Zakaz GetZakaz(string name, int Procedure_id)
        {
            Zakaz zakaz = context.Zakazs.FirstOrDefault(rec => rec.Name.Equals(name) && rec.procId == Procedure_id);
            
            return zakaz;
        }
    }
}