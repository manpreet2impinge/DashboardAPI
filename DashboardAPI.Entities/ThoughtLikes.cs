using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DashboardAPI.Entities
{
    [Table(nameof(ThoughtLikes))]
    public class ThoughtLikes
    {
        [Key]
        public int Id { get; set; }
        public int ThoughtId { get; set; }
        public string UserGuid { get; set; } //Liked by user
        public DateTime Created { get; set; }
    }
}
