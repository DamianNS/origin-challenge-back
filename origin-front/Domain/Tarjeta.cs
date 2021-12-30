using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace origin_front.Domain
{
    [Table("tarjetas")]
    public class Tarjeta
    {
        [Key]
        public string Id { get; set; }
        public int? Pin { get; set; }
        public bool? Lock { get; set; }
        public decimal? balance { get; set; }
        public int? Intentos { get; set; }


    }
}
