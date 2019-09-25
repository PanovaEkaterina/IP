using Katalog_v_2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katalog_v_2.Models.Interface
{
    interface IUser: IModelService
    {
        bool Authorization(User new_user);
        bool Registrations(User new_user);
    }
}
