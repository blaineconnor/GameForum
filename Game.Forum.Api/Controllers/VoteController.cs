using Game.Forum.Application.Models.DTOs.Vote;
using Game.Forum.Application.Models.RequestModels.User;
using Game.Forum.Application.Models.RequestModels.Votes;
using Game.Forum.Application.Services.Abstraction;
using Game.Forum.Domain.Cache.Redis;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Game.Forum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;
        private readonly IRedisCache _redisCache;
        private const string GetAllQuestionsContractKey = "GetAllQuestionsContract";

        public VoteController(IVoteService voteService, IRedisCache redisService)
        {
            _voteService = voteService;
            _redisCache = redisService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddVote(AddVoteDto addVote)
        {
            await _voteService.AddVote(addVote);
            await _redisCache.Remove(GetAllQuestionsContractKey);
            return Ok(UserResponseVM.Success(null, HttpStatusCode.OK));

        }
    }
}
