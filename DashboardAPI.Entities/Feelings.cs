using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DashboardAPI.Entities
{
    [Table(nameof(Feelings))]
    public class Feelings
    {
        [Key]
        public int Id { get; set; }
        public string UserGuid { get; set; }
        public DateTime Day { get; set; }
        public byte Feeling { get; set; }
        public DateTime Created { get; set; }
    }
}
