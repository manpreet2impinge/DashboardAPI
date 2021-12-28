using DashboardAPI.Context;
using DashboardAPI.Model;
using System.Net;

namespace DashboardAPI.Repositories.Bases
{
    public class DashboardBaseRepository
    {
        protected readonly ExpressDbContext _context;

        public DashboardBaseRepository(ExpressDbContext context)
        {
            _context = context;
        }
        protected ResponseModel<T> Success<T>(string message, T data)
        {
            return new ResponseModel<T>
            {
                Message = message,
                Success = true,
                Data = data
            };
        }
        protected ResponseModel<T> Error<T>(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ResponseModel<T>
            {
                Message = message,
                StatusCode = (int)statusCode
            };
        }
    }
}