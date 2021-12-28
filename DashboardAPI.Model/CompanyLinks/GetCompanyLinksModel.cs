using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.CompanyLinks
{
    public class GetCompanyLinksModel : AddCompanyLinkModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
