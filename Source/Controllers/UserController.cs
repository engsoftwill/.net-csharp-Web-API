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
        IUserService _service;
        IMapper _mapper;

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
                return _mapper.Map<List<UserDTO>>(_service.FindByAccelerationName(accelerationName));
            }
            if (accelerationName != null && companyId == null)
            {
                return _mapper.Map<List<UserDTO>>(_service.FindByAccelerationName(accelerationName));
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
            return Ok(_mapper.Map<UserDTO>(_service.FindById(id)));
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
