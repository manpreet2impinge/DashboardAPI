using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Entities
{
    public class CompanyFeelings
    {
        public int Id { get; set; }
        public string CompanyGuid { get; set; }
        public DateTime Day { get; set; }
        public int Happy { get; set; }
        public int Sad { get; set; }
        public int Total { get; set; }
    }
}
