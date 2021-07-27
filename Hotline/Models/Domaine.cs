using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotline.Models
{
    public class Domaine
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public Projet Projet { get; set; }
    }
}
