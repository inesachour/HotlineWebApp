using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotline.Models
{
    public class Reclamation
    {
        [Key]
        public int Numero { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Projet Projet { get; set; }
        [Required]
        public Domaine Domaine { get; set; }
        [Required]
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        public DateTime DateSoumission { get; set; }
        public string Statut { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        public DateTime? DateAffectation { get; set; }
        public User Responsable { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        public DateTime? DateResolution { get; set; }
        public string Solution { get; set; }
    }
}
