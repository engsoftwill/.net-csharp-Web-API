using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase 
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {
            if (accelerationName == null && companyId != null)
            {
                var idsuser = _service.FindByCompanyId(companyId.GetValueOrDefault());
                var idsuserDTO = _mapper.Map<List<UserDTO>>(idsuser);
                return Ok(idsuserDTO);
            }
            if (accelerationName != null && companyId == null)
            {
                var idsuser = _service.FindByAccelerationName(accelerationName);
                var idsuserDTO = _mapper.Map<List<UserDTO>>(idsuser);
                return Ok(idsuserDTO);
            }
            else
            {
                return NoContent();
            }

        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            var idsuser = _service.FindById(id);
            var idsuserDTO = _mapper.Map<UserDTO>(idsuser);
            return Ok(idsuserDTO);
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            var user = _mapper.Map<User>(value);
            _service.Save(user);

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }   
     
    }
}
