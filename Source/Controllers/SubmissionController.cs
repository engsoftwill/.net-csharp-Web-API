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
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _service;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/submission/higherScore")]
        public ActionResult<decimal> GetHigherScore(int? challengeId = null)
        {
            if (challengeId == null)
            {
                return NoContent();
            }
            var challegehigherscore = _service.FindHigherScoreByChallengeId(challengeId.GetValueOrDefault());
            return Ok(challegehigherscore);

        }
        [HttpGet("api/submission")]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId = null, int? accelerationId = null)
        {
            if (challengeId != null && accelerationId != null)
            {
                ICollection<Submission> submissions;
                submissions = _service.FindByChallengeIdAndAccelerationId(challengeId.GetValueOrDefault(), accelerationId.GetValueOrDefault());
                var submissionsDTO = _mapper.Map<List<SubmissionDTO>>(submissions);
                return Ok(submissionsDTO);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("api/submission")]
        public ActionResult<SubmissionDTO> Post([FromBody] SubmissionDTO value)
        {
            var submission = _mapper.Map<Submission>(value);
            _service.Save(submission);

            var submissionDTO = _mapper.Map<SubmissionDTO>(submission);
            return Ok(submissionDTO);
        }
    }
}
