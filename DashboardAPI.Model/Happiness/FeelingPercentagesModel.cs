using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.Happiness
{
    public class FeelingPercentagesModel
    {
        public Dictionary<string, double> Percentage { get; set; }
        public Dictionary<string, int> Actual { get; set; }
    }
}
