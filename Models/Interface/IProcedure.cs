using Katalog_v_2.Service;

namespace Katalog_v_2.Models.Interface
{
    interface IProcedure: IModelService
    {
        void AddZakaz(Zakaz zakaz);
        Zakaz GetZakaz(string name, int Procedure_id);
    }
}
