using Katalog_v_2.Models;
using Katalog_v_2.Models.Abstract;
using Katalog_v_2.Models.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Katalog_v_2.Service.BDService
{
    public class UserService: AbstractDbService<User>, IUser
    {
        private dbContext context = new dbContext();

        public override DbSet<User> Pata { get { return context.Users; } }

        public override dbContext Сontext { get { return context; } }

        public override List<AModel> GetList()
        {
            List<AModel> amodel = new List<AModel>();
            List<User> Users = context.Users.ToList();
            foreach (User user in Users)
            {
                amodel.Add(user);
            }
            return amodel;
        }     

        public bool Registrations(User new_user) {
            User user = context.Users.FirstOrDefault(rec => rec.Login.Equals(new_user.Login));
            if (user != null)
            {
                return false;
            }
            else {
                AddElement(new_user);
                return true;
            }
        }

        public bool Authorization(User new_user)
        {
            User user = context.Users.FirstOrDefault(rec => rec.Login.Equals(new_user.Login) && rec.Password.Equals(new_user.Password));
            if (user == null)
            {
                return false;
            }
            else
            { 
                return true;
            }
        }
    }
}