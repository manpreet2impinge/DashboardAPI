using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardAPI.Service.Interfaces;
using DashboardAPI.Model.CompanyLinks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JustLogin.TokenProvider.Controllers;

namespace DashboardAPI.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CompanyLinksController : BaseController
    {
        public readonly ICompanyLinkService _service = null;
        public CompanyLinksController(ICompanyLinkService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddCompanyLinkModel model)
        {
            var result = await _service.Create(model);
            return Ok(result);
        }
        [HttpPut, Route("{linkId}")]
        public async Task<IActionResult> Update(int linkId, AddCompanyLinkModel model)
        {
            var result = await _service.Update(linkId, model);
            return Ok(result);
        }
        [HttpGet, Route("{companyGuid}")]
        public async Task<IActionResult> Get(string companyGuid)
        {
            var result = await _service.Get(companyGuid);
            return Ok(result);
        }
        [HttpDelete, Route("{linkId}")]
        public async Task<IActionResult> Delete(int linkId)
        {
            var result = await _service.Delete(linkId);
            return Ok(result);
        }
        [HttpPut, Route("ReOrder/{companyGuid}")]
        public async Task<IActionResult> ReOrder(string companyGuid, List<ReorderCompanyLinksModel> model)
        {
            var result = await _service.ReOrder(companyGuid, model);
            return Ok(result);
        }
    }
}
