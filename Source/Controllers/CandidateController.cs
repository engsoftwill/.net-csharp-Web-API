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
    public class CandidateController : ControllerBase
    {

        ICandidateService _service;
        IMapper _mapper;
        public CandidateController(ICandidateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/candidate/{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId )
        {
            return Ok(_mapper.Map<CandidateDTO>(_service.FindById(userId, accelerationId, companyId)));
        }
        [HttpGet("api/candidate")]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? companyId = null, int? accelerationId = null)
        {
            if (companyId != null && accelerationId==null)
            {
                return _mapper.Map<List<CandidateDTO>>(_service.FindByCompanyId(companyId.GetValueOrDefault()));
            }
            if (accelerationId != null && companyId == null)
            {
                return _mapper.Map<List<CandidateDTO>>(_service.FindByAccelerationId(accelerationId.GetValueOrDefault()));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("api/candidate")]
        public ActionResult<CandidateDTO> Post([FromBody] CandidateDTO value)
        {
            var candidate = _mapper.Map<Candidate>(value);
            _service.Save(candidate);

            var candidateDTO = _mapper.Map<AccelerationDTO>(candidate);
            return Ok(candidateDTO);
        }
    }
}
