using System.Text.Json;

namespace DashboardAPI.Common.ViewModels
{
    public class DataViewModel
    {
        public int code { get; set; }  
        public bool status { get; set; } = true;  
        public string message { get; set; } = string.Empty; 
        public object data { get; set; } = JsonSerializer.Deserialize<object>("{}");
    }
}