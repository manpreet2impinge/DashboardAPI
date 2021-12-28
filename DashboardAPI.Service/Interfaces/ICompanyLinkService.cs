using DashboardAPI.Model;
using DashboardAPI.Model.CompanyLinks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DashboardAPI.Service.Interfaces
{
    public interface ICompanyLinkService
    {
        Task<ResponseModel<int>> Create(AddCompanyLinkModel model);
        Task<ResponseModel<int>> Update(int linkId, AddCompanyLinkModel model);
        Task<ResponseModel<List<GetCompanyLinksModel>>> Get(string companyGuid);
        Task<ResponseModel<int>> Delete(int id);
        Task<ResponseModel<bool>> ReOrder(string companyGuid, List<ReorderCompanyLinksModel> model);
    }
}
