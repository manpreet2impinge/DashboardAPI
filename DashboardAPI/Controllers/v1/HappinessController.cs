using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardAPI.Service.Interfaces;
using DashboardAPI.Model.Happiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JustLogin.TokenProvider.Controllers;

namespace DashboardAPI.Controllers.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HappinessController : BaseController
    {
        public readonly IHappinessService _service = null;
        public HappinessController(IHappinessService service)
        {
            _service = service;
        }
        [HttpPost, Route("thoughts")]
        public async Task<IActionResult> AddThought(AddThoughtModel model)
        {
            var result = await _service.AddThought(model);
            return Ok(result);
        }
        [HttpGet, Route("thoughts")]
        public async Task<IActionResult> GetThoughts()
        {
            string authToken = Request.Headers["Authorization"];
            var result = await _service.GetThoughts(authToken);
            return Ok(result);
        }
        [HttpPost, Route("feelings")]
        public async Task<IActionResult> AddNewFeeling(AddNewFeelingModel model)
        {
            var result = await _service.AddNewFeeling(model);
            return Ok(result);
        }
        [HttpGet, Route("feelings")]
        public async Task<IActionResult> GetFeelings(string companyGuid, string day)
        {
            var result = await _service.GetFeelings(companyGuid, day);
            return Ok(result);
        }
        [HttpPost, Route("thoughts/{thoughtId}/add-like")]
        public async Task<IActionResult> AddLike(int thoughtId, string userGuid)
        {
            var result = await _service.AddLike(thoughtId, userGuid);
            return Ok(result);
        }
        [HttpPost, Route("thoughts/{thoughtId}/remove-like")]
        public async Task<IActionResult> RemoveLike(int thoughtId, string userGuid)
        {
            var result = await _service.RemoveLike(thoughtId, userGuid);
            return Ok(result);
        }
        [HttpPost, Route("thoughts/{thoughtId}/likes")]
        public async Task<IActionResult> GetThoughtLikes(int thoughtId)
        {
            var result = await _service.GetThoughtLikes(thoughtId);
            return Ok(result);
        }
    }
}