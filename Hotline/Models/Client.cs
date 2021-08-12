using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotline.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ce champs est requis")]

        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Ce champs est requis")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Ce champs est requis")]
        public string Email { get; set; }
        public ICollection<Projet> Projets { get; set; }


    }
}
