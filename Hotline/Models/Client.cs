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
        [Required]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
