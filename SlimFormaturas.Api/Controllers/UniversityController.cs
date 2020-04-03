﻿using Microsoft.AspNetCore.Mvc;
using SlimFormaturas.Api.Dto;
using SlimFormaturas.Domain.Entities;
using SlimFormaturas.Domain.Interfaces.Service;
using SlimFormaturas.Domain.Notifications;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlimFormaturas.Domain.Entities;

namespace SlimFormaturas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ApiController
    {
        readonly IUniversityService _universityService;
        readonly IMapper _mapper;
        public UniversityController
        (
            IUniversityService universityService,
            NotificationHandler notifications,
            IMapper mapper

        ) : base(notifications)
        {
            _universityService = universityService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(UniversityDto universityDto)
        {
            var university = _mapper.Map<University>(universityDto);
            return Ok(await _universityService.Insert(university)); 
        }

        [HttpPut]
        public async Task<ActionResult> Put(University university) 
        {
            university = await _universityService.Update(university);
            return Response(university);
        }
    }
}