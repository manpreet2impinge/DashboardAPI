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
using DashboardAPI.Model.Happiness;
using System.Globalization;
using DashboardAPI.Model.API.User;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace DashboardAPI.Repositories
{
    public class HappinessRepository : DashboardBaseRepository, IHappinessRepository
    {
        private readonly ILogger<CompanyLinkRepository> _logger;
        public HappinessRepository(ExpressDbContext context, ILogger<CompanyLinkRepository> logger) : base(context)
        {
            _logger = logger;
        }
        public async Task<ResponseModel<int>> AddThought(AddThoughtModel model)
        {
            try
            {
                var dataToAdd = new Thoughts
                {
                    UserGuid = model.UserGuid,
                    Thought = model.Thought,
                    Created = DateTime.Now,
                    CompanyGuid = model.CompanyGuid
                };
                _context.Thoughts.Add(dataToAdd);
                await _context.SaveChangesAsync();
                return Success("Thought added", dataToAdd.Id);
            }
            catch (Exception ex)
            {
                return Error<int>(ex.Message);
            }
        }
        public async Task<ResponseModel<List<ThoughtsByDateModel>>> GetThoughts(string authToken)
        {
            try
            {
                var users = await GetMembershipUsers(authToken);
                if (!users.Success)
                {
                    return Error<List<ThoughtsByDateModel>>(users.Message, (HttpStatusCode)users.StatusCode);
                }

                DateTime dateToComplare = DateTime.Now.AddMonths(-3);
                var list = await (from thought in _context.Thoughts
                                  where thought.Created > dateToComplare
                                  orderby thought.Created descending
                                  //To-Do: need to pull user details from membership db. No details provided yet
                                  select new ThoughtsModel
                                  {
                                      Id = thought.Id,
                                      UserGuid = thought.UserGuid,
                                      Thought = thought.Thought,
                                      Created = thought.Created
                                  }).ToListAsync();

                List<int> thoughtIds = list.Select(x => x.Id).ToList();
                var thoughsLikes = (await (from like in _context.ThoughtLikes
                                               //join user in _context.Users on like.UserGuid equals user.UserGuid //To-Do: need to pull from membership db. No details provided yet
                                           where thoughtIds.Contains(like.ThoughtId)
                                           select new ThoughtLikesModel
                                           {
                                               ThoughtId = like.ThoughtId,
                                               UserGuid = like.UserGuid,
                                               FullName = "test"// user.FirstName + " " + user.LastName
                                           }).ToListAsync()).GroupBy(x => x.ThoughtId).Select(x => new { x.Key, Data = x.ToList() }).ToList();

                foreach (var thought in list)
                {
                    thought.Likes = thoughsLikes.Where(x => x.Key == thought.Id).SelectMany(x => x.Data).ToList();
                    thought.FullName = UserFullName(users.Data, thought.UserGuid);
                    thought.Likes.ForEach(x =>
                    {
                        x.FullName = UserFullName(users.Data, x.UserGuid);
                    });
                }

                var thougts = list.GroupBy(x => x.Created.Date).Select(x => new ThoughtsByDateModel
                {
                    Date = x.Key,
                    Thoughts = x.ToList(),
                    Day = x.Key.Date == DateTime.Now.Date ? "Today" : x.Key.ToString("dddd"),
                    InLast7Days = x.Key.Date > DateTime.Now.AddDays(-7).Date
                }).ToList();

                return Success("Thoughts Fetched", thougts);
            }
            catch (Exception ex)
            {
                return Error<List<ThoughtsByDateModel>>(ex.Message);
            }
        }
        public async Task<ResponseModel<int>> AddNewFeeling(AddNewFeelingModel model)
        {
            try
            {
                var dataToAdd = new Feelings
                {
                    UserGuid = model.UserGuid,
                    Feeling = model.Feeling,
                    Day = DateTime.Now.Date,
                    Created = DateTime.Now
                };
                _context.Feelings.Add(dataToAdd);
                await _context.SaveChangesAsync();

                var companyFeelings = await _context.CompanyFeelings.Where(x => x.CompanyGuid == model.CompanyGuid
                && x.Day.Year == dataToAdd.Day.Year
                && x.Day.Month == dataToAdd.Day.Month
                && x.Day.Day == dataToAdd.Day.Day
                ).SingleOrDefaultAsync();
                if (companyFeelings == null)
                {
                    companyFeelings = new CompanyFeelings
                    {
                        CompanyGuid = model.CompanyGuid,
                        Day = dataToAdd.Day,
                        Happy = model.Feeling == 1 ? 1 : 0,
                        Sad = model.Feeling == 0 ? 1 : 0,
                        Total = 1
                    };
                    _context.CompanyFeelings.Add(companyFeelings);
                }
                else
                {
                    var userFeelings = await _context.Feelings.Where(x => x.UserGuid == model.UserGuid && x.Day == dataToAdd.Day).OrderByDescending(x => x.Id).Take(2).ToListAsync();
                    if (userFeelings.Count == 1)
                    {
                        if (model.Feeling == 1)
                        {
                            companyFeelings.Happy++;
                        }
                        else
                        {
                            companyFeelings.Sad++;
                        }
                        companyFeelings.Total++;
                    }
                    else
                    {
                        var userFeeling = userFeelings[1];
                        if (model.Feeling != userFeeling.Feeling)
                        {
                            if (model.Feeling == 1)
                            {
                                companyFeelings.Happy++;
                                companyFeelings.Sad = companyFeelings.Sad == 0 ? 0 : companyFeelings.Sad - 1;
                            }
                            else
                            {
                                companyFeelings.Sad++;
                                companyFeelings.Happy = companyFeelings.Happy == 0 ? 0 : companyFeelings.Happy - 1;
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return Success("Feeling added", dataToAdd.Id);
            }
            catch (Exception ex)
            {
                return Error<int>(ex.Message);
            }
        }
        public async Task<ResponseModel<FeelingPercentagesModel>> GetFeelings(string companyGuid, string day)
        {
            try
            {
                DateTime date = DateTime.ParseExact(day, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var feelingsOfDay = await _context.CompanyFeelings.Where(x => x.CompanyGuid == companyGuid && x.Day == date).SingleOrDefaultAsync();
                if (feelingsOfDay == null)
                {
                    return Success("Thoughts Fetched", new FeelingPercentagesModel
                    {
                        Percentage = new Dictionary<string, double> { { "Good", 0 }, { "Bad", 0 } },
                        Actual = new Dictionary<string, int> { { "Good", 0 }, { "Bad", 0 } }
                    });
                }
                var model = new FeelingPercentagesModel
                {
                    Percentage = new Dictionary<string, double> {
                         { "Good",Math.Round(((double)feelingsOfDay.Happy / feelingsOfDay.Total) * 100, 0)},
                         { "Bad",Math.Round(((double)feelingsOfDay.Sad / feelingsOfDay.Total) * 100, 0)}
                     },
                    Actual = new Dictionary<string, int> {
                         { "Good",feelingsOfDay.Happy},
                         { "Bad", feelingsOfDay.Sad  }
                     }
                };
                return Success("Thoughts Fetched", model);
            }

            catch (Exception ex)
            {
                return Error<FeelingPercentagesModel>(ex.Message);
            }
        }
        public async Task<ResponseModel<int>> AddLike(int thoughtId, string userGuid)
        {
            try
            {
                var thought = await _context.Thoughts.SingleOrDefaultAsync(x => x.Id == thoughtId);
                if (thought == null)
                {
                    return Error<int>("Thought not found");
                }
                var thougtLike = await _context.ThoughtLikes.SingleOrDefaultAsync(x => x.ThoughtId == thoughtId && x.UserGuid == userGuid);
                if (thougtLike == null)
                {
                    thougtLike = new ThoughtLikes
                    {
                        ThoughtId = thoughtId,
                        UserGuid = userGuid,
                        Created = DateTime.Now
                    };
                    _context.ThoughtLikes.Add(thougtLike);
                }
                await _context.SaveChangesAsync();
                thought.Likes = await _context.ThoughtLikes.Where(x => x.ThoughtId == thoughtId).CountAsync();
                await _context.SaveChangesAsync();
                return Success("Like added", thougtLike.Id);
            }
            catch (Exception ex)
            {
                return Error<int>(ex.Message);
            }
        }
        public async Task<ResponseModel<int>> RemoveLike(int thoughtId, string userGuid)
        {
            try
            {
                var thought = await _context.Thoughts.SingleOrDefaultAsync(x => x.Id == thoughtId);
                if (thought == null)
                {
                    return Error<int>("Thought not found");
                }
                var thougtLike = await _context.ThoughtLikes.SingleOrDefaultAsync(x => x.ThoughtId == thoughtId && x.UserGuid == userGuid);
                if (thougtLike == null)
                {
                    return Error<int>("Thought like not found");
                }
                _context.ThoughtLikes.Remove(thougtLike);
                await _context.SaveChangesAsync();
                thought.Likes = await _context.ThoughtLikes.Where(x => x.ThoughtId == thoughtId).CountAsync();
                await _context.SaveChangesAsync();
                return Success("Like removed", thougtLike.Id);
            }
            catch (Exception ex)
            {
                return Error<int>(ex.Message);
            }
        }
        public async Task<ResponseModel<List<ThoughtLikesModel>>> GetThoughtLikes(int thoughtId)
        {
            try
            {
                var list = await (from like in _context.ThoughtLikes
                                  join user in _context.Users on like.UserGuid equals user.UserGuid
                                  where like.ThoughtId == thoughtId
                                  select new ThoughtLikesModel
                                  {
                                      ThoughtId = like.ThoughtId,
                                      UserGuid = user.UserGuid,
                                      FullName = user.FirstName + " " + user.LastName
                                  }).ToListAsync();
                return Success("Thoughts likes Fetched", list);
            }
            catch (Exception ex)
            {
                return Error<List<ThoughtLikesModel>>(ex.Message);
            }
        }

        private async Task<ResponseModel<List<UsersResponseModel>>> GetMembershipUsers(string token)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", token);
                    using (var response = await httpClient.GetAsync("https://apis.justlogindevelopment.xyz/membership/v2/users"))
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            return Success("Success", JsonConvert.DeserializeObject<List<UsersResponseModel>>(apiResponse));
                        }
                        else
                        {
                            return Error<List<UsersResponseModel>>(response.ReasonPhrase, response.StatusCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Error<List<UsersResponseModel>>(ex.Message);
            }
        }
        private string UserFullName(List<UsersResponseModel> users, string userGuid)
        {
            var user = users.Where(x => x.UserGuid == userGuid).FirstOrDefault();
            return user?.FullName;
        }
    }
}