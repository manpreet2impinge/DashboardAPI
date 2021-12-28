using System.Linq;
using System.Threading.Tasks;
using DashboardAPI.Repositories.Bases;
using DashboardAPI.Repositories.Interfaces;
using DashboardAPI.Context;
using Microsoft.Extensions.Logging;
using DashboardAPI.Entities;
using DashboardAPI.Model.CompanyLinks;
using DashboardAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace DashboardAPI.Repositories
{
    public class CompanyLinkRepository : DashboardBaseRepository, ICompanyLinkRepository
    {
        private readonly ILogger<CompanyLinkRepository> _logger;
        public CompanyLinkRepository(ExpressDbContext context, ILogger<CompanyLinkRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public async Task<ResponseModel<int>> Create(AddCompanyLinkModel model)
        {
            try
            {
                var dataToAdd = new CompanyLinks
                {
                    Name = model.Name,
                    Link = model.Link,
                    UserGuid = model.UserGuid,
                    Created = DateTime.Now,
                    CompanyGuid = model.CompanyGuid
                };
                _context.CompanyLinks.Add(dataToAdd);
                await _context.SaveChangesAsync();
                return Success("Company link added", dataToAdd.Id);
            }
            catch (Exception ex)
            {
                return Error<int>(ex.Message);
            }
        }
        public async Task<ResponseModel<int>> Update(int linkId, AddCompanyLinkModel model)
        {
            try
            {
                var data = await _context.CompanyLinks.FirstOrDefaultAsync(x => x.Id == linkId);
                if (data == null)
                {
                    return Error<int>("Company link not found");
                }
                data.Link = model.Link;
                data.Name = model.Name;
                data.Updated = DateTime.Now;
                await _context.SaveChangesAsync();
                return Success("Company link updated", data.Id);
            }
            catch (Exception ex)
            {
                return Error<int>(ex.Message);
            }
        }
        public async Task<ResponseModel<int>> Delete(int id)
        {
            try
            {
                var data = await _context.CompanyLinks.FirstOrDefaultAsync(x => x.Id == id);
                if (data == null)
                {
                    return Error<int>("Company link not found");
                }
                _context.CompanyLinks.Remove(data);
                await _context.SaveChangesAsync();
                return Success("Company link deleted", data.Id);
            }
            catch (Exception ex)
            {
                return Error<int>(ex.Message);
            }
        }
        public async Task<ResponseModel<List<GetCompanyLinksModel>>> Get(string companyGuid)
        {
            try
            {
                var data = await _context.CompanyLinks.Where(x => x.CompanyGuid == companyGuid).Select(x => new GetCompanyLinksModel
                {
                    Name = x.Name,
                    Link = x.Link,
                    CompanyGuid = x.CompanyGuid,
                    Created = x.Created,
                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    Updated = x.Updated,
                    UserGuid = x.UserGuid
                }).OrderBy(x => x.OrderNumber).ThenByDescending(x => x.Created).ToListAsync();
                return Success("Company links fetched", data);
            }
            catch (Exception ex)
            {
                return Error<List<GetCompanyLinksModel>>(ex.Message);
            }
        }
        public async Task<ResponseModel<bool>> ReOrder(string companyGuid, List<ReorderCompanyLinksModel> model)
        {
            try
            {
                var companyLinks = await _context.CompanyLinks.Where(x => x.CompanyGuid == companyGuid).ToListAsync();
                companyLinks.ForEach(x =>
                {
                    var updated = model.SingleOrDefault(_ => _.Id == x.Id);
                    if (updated != null)
                    {
                        x.OrderNumber = updated.OrderNumber;
                        x.Updated = DateTime.Now;
                    }
                });
                await _context.SaveChangesAsync();
                return Success("Company links reordered", true);
            }
            catch (Exception ex)
            {
                return Error<bool>(ex.Message);
            }
        }
    }
}