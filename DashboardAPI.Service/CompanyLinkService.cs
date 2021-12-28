using DashboardAPI.Service.Interfaces;
using DashboardAPI.Model;
using DashboardAPI.Model.CompanyLinks;
using System.Collections.Generic;
using System.Threading.Tasks;
using DashboardAPI.Repositories.Interfaces;

namespace DashboardAPI.Service
{
    public class CompanyLinkService : BaseService, ICompanyLinkService
    {
        private readonly ICompanyLinkRepository _repo;

        public CompanyLinkService(ICompanyLinkRepository repo)
        {
            _repo = repo;
        }
        public async Task<ResponseModel<int>> Create(AddCompanyLinkModel model)
        {
            var r = await _repo.Create(model);
            return r;
        }

        public async Task<ResponseModel<int>> Delete(int id)
        {
            var r = await _repo.Delete(id);
            return r;
        }

        public async Task<ResponseModel<List<GetCompanyLinksModel>>> Get(string companyGuid)
        {
            var r = await _repo.Get(companyGuid);
            return r;
        }

        public async Task<ResponseModel<bool>> ReOrder(string companyGuid, List<ReorderCompanyLinksModel> model)
        {
            var r = await _repo.ReOrder(companyGuid, model);
            return r;
        }

        public async Task<ResponseModel<int>> Update(int linkId, AddCompanyLinkModel model)
        {
            var r = await _repo.Update(linkId, model);
            return r;
        }
    }
}