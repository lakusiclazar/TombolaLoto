using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTombola.Models
{
    [Table("Igrac")]
    public partial class Igrac
    {
        public Igrac()
        {
            Kombinacije = new HashSet<Kombinacija>();
            Pobednici = new HashSet<Pobednik>();
        }

        public int IgracId { get; set; }
        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [InverseProperty("Igrac")]
        public virtual ICollection<Kombinacija> Kombinacije { get; set; }
        [InverseProperty("PobednickiIgrac")]
        public virtual ICollection<Pobednik> Pobednici { get; set; }
    }
}