using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.Happiness
{
    public class AddNewFeelingModel
    {
        public string UserGuid { get; set; }
        public string CompanyGuid { get; set; }
        public byte Feeling { get; set; }
    }
}