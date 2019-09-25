using Katalog_v_2.Models.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Katalog_v_2.Models
{
    [DataContract]
    public class User: AModel
    {
        [DataMember]
        [Required]
        public string Login { get; set; }

        [DataMember]
        [Required]
        public string Password { get; set; }
    }
}