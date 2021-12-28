using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.CompanyLinks
{
    public class AddCompanyLinkModel
    {        
        public string Name { get; set; }
        public string Link { get; set; }
        public string UserGuid { get; set; }
        public string CompanyGuid { get; set; }
    }
}
