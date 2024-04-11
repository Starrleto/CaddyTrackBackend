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

        public List<Trackermodel> GetTrackersByUser(string user){
            return _service.GetTrackersByUser(user);
        }

        public bool AddTracker(TrackerDTO t){
            return _service.AddTracker(t);
        }
        public bool EditTracker(TrackerDTO t){
            return _service.EditTracker(t);
        }
        public bool DeleteTracker(TrackerDTO t){
            return _service.DeleteTracker(t);
        }
    }
}