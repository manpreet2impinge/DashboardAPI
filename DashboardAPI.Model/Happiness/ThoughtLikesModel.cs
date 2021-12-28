using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.Happiness
{
    public class ThoughtLikesModel
    {
        public int ThoughtId { get; set; }
        public string UserGuid { get; set; }
        public string FullName { get; set; }
    }
}
