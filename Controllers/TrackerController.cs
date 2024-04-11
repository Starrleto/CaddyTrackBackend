using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaddyTrack.Models;
using CaddyTrack.Models.DTO;
using CaddyTrack.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaddyTrack.Controllers
{
    [ApiController]
    [Route("TrackerController")]
    public class TrackerController
    {
        private readonly TrackerService _service;
        public TrackerController(TrackerService s)
        {
            _service = s;
        }


    }
}