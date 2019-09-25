using Katalog_v_2.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Katalog_v_2.Models
{
    [Serializable]
    [DataContract]
    public class Zakaz: AModel
    {
        [DataMember]
        public int procId { get; set; }

        [Required(ErrorMessage = "Введите товар")]
        [RegularExpression(@"^.{1,100}$", ErrorMessage = "Введите от 1 до 100 символов")]
        [DataMember]
        public string Product { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [RegularExpression(@"^.{1,100}$", ErrorMessage = "Введите от 1 до 100 символов")]
        [DataMember]
        public string Soder { get; set; }

        [DataMember]
        public string ZakazData { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public string ImageMimeType { get; set; }

        [DataMember]
        public int Procedure_Id { get; set; }

        public virtual Procedure Procedure { get; set; }
    }
}