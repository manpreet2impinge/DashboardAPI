using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.Happiness
{
    public class AddThoughtModel
    {
        public string Thought { get; set; }
        public string UserGuid { get; set; }
        public string CompanyGuid { get; set; }
    }
}
