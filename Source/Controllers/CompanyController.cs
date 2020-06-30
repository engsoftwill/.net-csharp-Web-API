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
    public class CompanyController : ControllerBase
    {
        ICompanyService _service;
        IMapper _mapper;
        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("api/company/{id}")]


}
}
