using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTombola.Models
{
    [Table("Pobednik")]
    public partial class Pobednik
    {
        public int PobednikId { get; set; }
        public int PobednickiIgracId { get; set; }
        public int PobednickaKombinacijaId { get; set; }

        [ForeignKey("PobednickaKombinacijaId")]
        [InverseProperty("Pobednici")]
        public virtual Kombinacija PobednickaKombinacija { get; set; }
        [ForeignKey("PobednickiIgracId")]
        [InverseProperty("Pobednici")]
        public virtual Igrac PobednickiIgrac { get; set; }
    }
}