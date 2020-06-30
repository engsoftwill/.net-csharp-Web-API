using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _service;
        private readonly IMapper _mapper;

        public ChallengeController(IChallengeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/challenge")]
        public ActionResult<IEnumerable<ChallengeDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId != null && userId != null)
            {
                ICollection<Models.Challenge> challenges;
                challenges = _service.FindByAccelerationIdAndUserId(accelerationId.GetValueOrDefault(), userId.GetValueOrDefault());
                var challengesDTO = _mapper.Map<List<ChallengeDTO>>(challenges);

                return Ok(challengesDTO);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("api/challenge")]
        public ActionResult<ChallengeDTO> Post([FromBody] ChallengeDTO value)
        {
            var challenge = _mapper.Map<Models.Challenge>(value);
            _service.Save(challenge);

            var challengeDTO = _mapper.Map<ChallengeDTO>(challenge);
            return Ok(challengeDTO);
        }
    }
}
