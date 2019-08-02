using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaTombola.Models
{
    [Table("Kombinacija")]
    public partial class Kombinacija
    {
        public Kombinacija()
        {
            Pobednici = new HashSet<Pobednik>();
        }

        public int KombinacijaId { get; set; }
        public int IgracId { get; set; }
        public int PrviBroj { get; set; }
        public int DrugiBroj { get; set; }
        public int TreciBroj { get; set; }
        public int CetvrtiBroj { get; set; }
        public int PetiBroj { get; set; }
        public int SestiBroj { get; set; }

        [ForeignKey("IgracId")]
        [InverseProperty("Kombinacije")]
        public virtual Igrac Igrac { get; set; }
        [InverseProperty("PobednickaKombinacija")]
        public virtual ICollection<Pobednik> Pobednici { get; set; }
    }
}