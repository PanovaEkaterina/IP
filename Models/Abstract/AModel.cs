using System;
using System.ComponentModel.DataAnnotations;

namespace Katalog_v_2.Models.Abstract
{
    public abstract class AModel
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "Введите название")]
        [RegularExpression(@"^.{1,100}$", ErrorMessage = "Введите от 1 до 100 символов")]
        public String Name { set; get; }
    }
}