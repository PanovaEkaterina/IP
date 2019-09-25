using Katalog_v_2.Service;
using System.Collections.Generic;
using System.Data.Entity;

namespace Katalog_v_2.Models.Abstract
{
    public abstract class AbstractDbService<T> : IModelService
         where T : AModel
    {
        public abstract dbContext Сontext { get; }
        public abstract DbSet<T> Pata { get; }

        public void AddElement(AModel model)
        {
            Pata.Add((T)model);
            Сontext.SaveChanges();
        }

        public void DelElement(int id)
        {
            T b = Pata.Find(id);
            if (b != null)
            {
                Pata.Remove(b);
                Сontext.SaveChanges();
            }
        }

        public AModel GetElement(int id)
        {
            T b = Pata.Find(id);
            return b;
        }

        public abstract List<AModel> GetList();

        public void UpdateElement(AModel model)
        {
            Сontext.Entry((T)model).State = EntityState.Modified;
            Сontext.SaveChanges();
        }
    }
}