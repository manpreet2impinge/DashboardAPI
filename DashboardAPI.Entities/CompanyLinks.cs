using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DashboardAPI.Entities
{
    [Table(nameof(CompanyLinks))]
    public class CompanyLinks
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int OrderNumber { get; set; }
        public string CompanyGuid { get; set; }
        public string UserGuid { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
