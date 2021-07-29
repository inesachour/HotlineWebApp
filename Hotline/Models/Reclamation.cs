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
        public Client Client { get; set; }
        public Projet Projet { get; set; }
        public Domaine Domaine { get; set; }
        public string Description { get; set; }
        public DateTime DateSoumission { get; set; }
        public string Statut { get; set; }
        public DateTime? DateAffectation { get; set; }
        public User Responsable { get; set; }
        public DateTime? DateResolution { get; set; }
        public string Solution { get; set; }
    }
}
