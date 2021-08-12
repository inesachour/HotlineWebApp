using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotline.Models
{
    public class Projet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ce champs est requis")]
        public string Nom { get; set; }
        public Client Client { get; set; }
        public ICollection<Domaine> Domaines { get; set; }


    }
}
