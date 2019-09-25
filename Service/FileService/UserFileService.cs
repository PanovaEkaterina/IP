using Katalog_v_2.Models;
using Katalog_v_2.Models.Abstract;
using Katalog_v_2.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Katalog_v_2.Service.FileService
{
    public class UserFileService: AbstractFileService, IUser
    {
        new string Name = "User";
        new string currentPath = HttpContext.Current.Server.MapPath("~") + "/Files/User";
        new XmlSerializer xsSubmit = new XmlSerializer(typeof(User));

        public UserFileService()
        {
            base.Name = Name;
            base.xsSubmit = xsSubmit;
            base.currentPath = currentPath;
        }

        public bool Registrations(User new_user)
        {
            List<User> users = new List<User>();
            List<AModel> context = base.GetList();
            foreach (User userel in context) {
                users.Add(userel);
            }
            User user = users.FirstOrDefault(rec => rec.Login.Equals(new_user.Login));
            if (user != null)
            {
                return false;
            }
            else
            {
                AddElement(new_user);
                return true;
            }
        }

        public bool Authorization(User new_user)
        {
            List<User> users = new List<User>();
            List<AModel> context = base.GetList();
            foreach (User userel in context)
            {
                users.Add(userel);
            }
            User user = users.FirstOrDefault(rec => rec.Login.Equals(new_user.Login) && rec.Password.Equals(new_user.Password));
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