using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Model.Happiness
{
    public class ThoughtsModel
    {
        public int Id { get; set; }
        public string UserGuid { get; set; }
        public string FullName { get; set; }
        public string Thought { get; set; }
        public DateTime Created { get; set; }
        public List<ThoughtLikesModel> Likes { get; set; }
    }
}
