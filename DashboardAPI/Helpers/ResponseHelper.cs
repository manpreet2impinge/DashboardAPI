using DashboardAPI.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardAPI.Helpers
{
    /// Global Response
    public class ResponseHelper
    {
        /// <summary>
        /// Overloaded Method to getting the positive response for an object.
        /// </summary>
        /// <param name="positiveResponse"></param>
        /// <returns></returns>
        public static ResponseViewModel GetReponse(object positiveResponse)
        {
            return new ResponseViewModel
            {
                data = positiveResponse,
            };
        }

        /// <summary>
        /// Overloaded Method to get a positive response for a collection
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="listName"></param>
        /// <returns></returns>
        public static ResponseViewModel GetResponseForCollection(IEnumerable<object> collection, string listName)
        {
            return new ResponseViewModel
            {
                data = new Dictionary<string, object>()
                {
                    {
                        listName, collection
                    }
                }
            };
        }

        /// <summary>
        /// Method to send the error response for a request.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static ResponseViewModel GetReponseForError(System.Exception exception, int errorCode = 400)
        {
            return new ResponseViewModel
            {
                data = null,
                errorMsg = exception.Message,
                errorID = errorCode,
                errorCode = exception.Data.Values.Count == 1 ? exception.Data["ErrorCode"].ToString() : "E0000",
                error = true
            };
        }
    }
}
