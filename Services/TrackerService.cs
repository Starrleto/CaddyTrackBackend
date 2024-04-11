using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaddyTrack.Models;
using CaddyTrack.Models.DTO;
using CaddyTrack.Services.Context;

namespace CaddyTrack.Services
{
    public class TrackerService
    {
        public DataContext _context;

        public TrackerService(DataContext c){
            _context = c;
        }
        public List<Trackermodel> GetTrackersByUser(string user){
            return null;
        }

        public bool AddTracker(TrackerDTO t){
            return _context.SaveChanges() != 0;
        }
        public bool EditTracker(TrackerDTO t){
            return _context.SaveChanges() != 0;
        }
        public bool DeleteTracker(TrackerDTO t){
            return _context.SaveChanges() != 0;
        }
    }
}