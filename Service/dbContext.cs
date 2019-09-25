using Katalog_v_2.Models;

using System.Data.Entity;

namespace Katalog_v_2.Service
{
    public class dbContext : DbContext
    {
        public dbContext() : base("CompFirm")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public static dbContext Create()
        {
            return new dbContext();
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Zakaz> Zakazs { get; set; }

        public virtual DbSet<Procedure> Procedures { get; set; }

    }
}