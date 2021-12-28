using DashboardAPI.Service.Interfaces;
using DashboardAPI.Model;
using DashboardAPI.Model.Happiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardAPI.Repositories.Interfaces;

namespace DashboardAPI.Service
{
    public class HappinessService : BaseService, IHappinessService
    {
        private readonly IHappinessRepository _repo;

        public HappinessService(IHappinessRepository repo)
        {
            _repo = repo;
        }
        public async Task<ResponseModel<int>> AddLike(int thoughtId, string userGuid)
        {
            var r =await _repo.AddLike(thoughtId, userGuid);
            return r;
        }
        public async Task<ResponseModel<int>> AddNewFeeling(AddNewFeelingModel model)
        {
            var r = await _repo.AddNewFeeling(model);
            return r;
        }
        public async Task<ResponseModel<int>> AddThought(AddThoughtModel model)
        {
            var r = await _repo.AddThought(model);
            return r;
        }
        public async Task<ResponseModel<FeelingPercentagesModel>> GetFeelings(string companyGuid, string day)
        {
            var r = await _repo.GetFeelings(companyGuid, day);
            return r;
        }
        public async Task<ResponseModel<List<ThoughtLikesModel>>> GetThoughtLikes(int thoughtId)
        {
            var r = await _repo.GetThoughtLikes(thoughtId);
            return r;
        }
        public async Task<ResponseModel<List<ThoughtsByDateModel>>> GetThoughts(string authToken)
        {
            var r = await _repo.GetThoughts(authToken);
            return r;
        }
        public async Task<ResponseModel<int>> RemoveLike(int thoughtId, string userGuid)
        {
            var r = await _repo.RemoveLike(thoughtId, userGuid);
            return r;
        }
    }
}