using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotline.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public Boolean Admin { get; set; }
        ICollection<Reclamation> Reclamations { get; set; }
    }
}
