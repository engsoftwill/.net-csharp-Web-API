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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/company/{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            var companyid = _service.FindById(id);
            var companyidDTO = _mapper.Map<CompanyDTO>(companyid);
            return Ok(companyidDTO);
        }
        [HttpGet("api/company")]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId == null && userId != null)
            {
                var idscompany = _service.FindByUserId(userId.GetValueOrDefault());
                var idscompanyDTO = _mapper.Map<List<AccelerationDTO>>(idscompany);
                return Ok(idscompanyDTO);
            }
            if (accelerationId != null && userId == null)
            {
                var idscompany = _service.FindByAccelerationId(accelerationId.GetValueOrDefault());
                var idscompanyDTO = _mapper.Map<List<AccelerationDTO>>(idscompany);
                return Ok(idscompanyDTO);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("api/company")]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO value)
        {
            var company = _mapper.Map<Company>(value);
            _service.Save(company);

            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return Ok(companyDTO);
        }
    }
}
