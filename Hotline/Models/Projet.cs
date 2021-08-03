using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotline.Models
{
    public class Projet
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public Client Client { get; set; }
        public ICollection<Domaine> Domaines { get; set; }


    }
}
