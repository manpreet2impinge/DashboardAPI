using DashboardAPI.Model;
using DashboardAPI.Model.Happiness;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAPI.Service.Interfaces
{
    public interface IHappinessService
    {
        Task<ResponseModel<int>> AddThought(AddThoughtModel model);
        Task<ResponseModel<List<ThoughtsByDateModel>>> GetThoughts(string authToken);
        Task<ResponseModel<int>> AddNewFeeling(AddNewFeelingModel model);
        Task<ResponseModel<FeelingPercentagesModel>> GetFeelings(string companyGuid, string day);
        Task<ResponseModel<int>> AddLike(int thoughtId, string userGuid);
        Task<ResponseModel<int>> RemoveLike(int thoughtId, string userGuid);
        Task<ResponseModel<List<ThoughtLikesModel>>> GetThoughtLikes(int thoughtId);        
    }
}
