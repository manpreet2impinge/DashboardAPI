using DashboardAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardAPI.Service
{
    public abstract class BaseService
    {
        protected ResponseModel<T> Success<T>(string message, T data)
        {
            return new ResponseModel<T>
            {
                Message = message,
                Success = true,
                Data = data
            };
        }
        protected ResponseModel<T> Error<T>(string message)
        {
            return new ResponseModel<T>
            {
                Message = message
            };
        }
    }
}
