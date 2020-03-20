using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Company { get; set; }
        
        [ForeignKey("PersoneId")]
        public int PersoneId { get; set; }
        public string Email { get; set; }

        public Persone Persone { get; set; }

    }
}
