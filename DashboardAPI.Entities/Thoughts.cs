using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DashboardAPI.Entities
{
    [Table(nameof(Thoughts))]
    public class Thoughts
    {
        [Key]
        public int Id { get; set; }
        public string UserGuid { get; set; }
        public string CompanyGuid { get; set; }
        public string Thought { get; set; }
        public int Likes { get; set; }
        public DateTime Created { get; set; }
    }
}
