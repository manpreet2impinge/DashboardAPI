using DashboardAPI.Model.CompanyLinks;
using DashboardAPI.Model;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace DashboardAPI.Repositories.Interfaces
{
    public interface ICompanyLinkRepository
    {
        Task<ResponseModel<int>> Create(AddCompanyLinkModel model);
        Task<ResponseModel<int>> Update(int linkId, AddCompanyLinkModel model);
        Task<ResponseModel<List<GetCompanyLinksModel>>> Get(string companyGuid);
        Task<ResponseModel<int>> Delete(int id);
        Task<ResponseModel<bool>> ReOrder(string companyGuid, List<ReorderCompanyLinksModel> model);
    }
}