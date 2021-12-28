using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.API.User
{
    public class UsersResponseModel
    {
        public Images Images { get; set; }
        public string CompanyGuid { get; set; }
        public string UserGuid { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string WorkEmail { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string Active { get; set; }
        public string SupervisorGuid { get; set; }
        public Department Department { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DeactivateDate { get; set; }
    }
    public class Images
    {
        public string Full { get; set; }
        public string Thumbnail { get; set; }
    }

    public class Department
    {
        public int Status { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string CompanyGuid { get; set; }
        public string Code { get; set; }
    }
}
