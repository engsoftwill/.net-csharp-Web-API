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
        private readonly IAccelerationService _service;
        private readonly IMapper _mapper;
        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/acceleration/{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
        {
            var accelid = _service.FindById(id);
            var accelidDTO = _mapper.Map<AccelerationDTO>(accelid);
            return Ok(accelidDTO);
        }
        [HttpGet("api/acceleration")]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if (companyId != null)
            {
                var accelid = _service.FindByCompanyId(companyId.GetValueOrDefault());
                var accelidDTO = _mapper.Map<List<AccelerationDTO>>(accelid);
                return Ok(accelidDTO);
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
