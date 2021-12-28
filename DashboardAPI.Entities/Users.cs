using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DashboardAPI.Entities
{
    [Table(nameof(Users))]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string UserGuid { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
