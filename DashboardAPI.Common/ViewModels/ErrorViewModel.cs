namespace DashboardAPI.Common.ViewModels
{
    public class ErrorViewModel
    {
        public int code { get; set; }  
        public string message { get; set; } = string.Empty; 
        public object details { get; set; } = new object();
    }
}