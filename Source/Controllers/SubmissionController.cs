﻿using System;
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
        ISubmissionService _service;
        IMapper _mapper;
        public SubmissionController(SubmissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/submission/higherScore")]
        public ActionResult<SubmissionDTO> Get(int? challengeId = null)
        {
            if (challengeId != null)
                return Ok(_mapper.Map<SubmissionDTO>(_service.FindHigherScoreByChallengeId(challengeId.GetValueOrDefault())));
            else
                return NoContent();
        }
        [HttpGet("api/submission")]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId = null, int? accelerationId = null)
        {
            if (challengeId != null && accelerationId != null)
            {
                return _mapper.Map<List<SubmissionDTO>>(_service.FindByChallengeIdAndAccelerationId(challengeId.GetValueOrDefault(),accelerationId.GetValueOrDefault()));
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
