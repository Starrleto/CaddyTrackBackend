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

        [HttpGet]
        [Route("GetTrackersByUser/{name}")]
        public List<Trackermodel> GetTrackersByUser(string name){
            return _service.GetTrackersByUser(name);
        }

        [HttpPost]
        [Route("AddTracker/{user}")]
        public bool AddTracker(TrackerDTO t, string user){
            return _service.AddTracker(t,user);
        }

        [HttpPatch]
        [Route("EditTracker/{user}/{id}")]
        public bool EditTracker(TrackerDTO t, string user, int id){
            return _service.EditTracker(t, user, id);
        }

        [HttpDelete]
        [Route("DeleteTracker/{user}/{id}")]
        public bool DeleteTracker(string user, int id){
            return _service.DeleteTracker(user, id);
        }
    }
}