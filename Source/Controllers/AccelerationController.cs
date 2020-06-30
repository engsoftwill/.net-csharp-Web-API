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
    public class AccelerationController : ControllerBase
    {
        IAccelerationService _service;
        IMapper _mapper;
        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/acceleration/{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
        {
            return Ok(_mapper.Map<AccelerationDTO>(_service.FindById(id)));
        }
        [HttpGet("api/acceleration")]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if (companyId != null)
            {
                return _mapper.Map<List<AccelerationDTO>>(_service.FindByCompanyId(companyId.GetValueOrDefault()));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("api/acceleration")]
        public ActionResult<AccelerationDTO> Post([FromBody] AccelerationDTO value)
        {
            var acceleration = _mapper.Map<Acceleration>(value);
            _service.Save(acceleration);

            var accelerationDTO = _mapper.Map<AccelerationDTO>(acceleration);
            return Ok(accelerationDTO);
        }

    }
}
