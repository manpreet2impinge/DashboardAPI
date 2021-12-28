using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.Happiness
{
    public class ThoughtsByDateModel
    {
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public bool InLast7Days { get; set; }
        public List<ThoughtsModel> Thoughts { get; set; }
    }
}