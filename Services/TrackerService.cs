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
        public List<Trackermodel> GetTrackersByUser(string name){

            UserModel user = GetUserByUsername(name);

            if(user != null){
                return _context.Trackers.Where(t => t.UserModelID == user.ID).ToList();
            }
            return null;
        }

        public bool AddTracker(TrackerDTO t, string user){

            Trackermodel newTracker = new Trackermodel(t.Name, t.StockYardage, t.MaxYardage, t.ConfidenceLevel);
            UserModel u = GetUserByUsername(user);

            if(u!=null){
                u.Trackers.Add(newTracker);
                _context.Update<UserModel>(u);
            }

            return _context.SaveChanges() != 0;
        }
        public bool EditTracker(TrackerDTO t, string user, int id){

            List<Trackermodel> trackers = GetTrackersByUser(user);

            if(trackers != null){
                
                Trackermodel model = trackers.FirstOrDefault(t => t.ID == id);
                if(model != null) {
                    model.Name = t.Name;
                    model.StockYardage = t.StockYardage;
                    model.MaxYardage = t.MaxYardage;
                    model.ConfidenceLevel = t.ConfidenceLevel;
                }
            }

            return _context.SaveChanges() != 0;
        }
        public bool DeleteTracker(string user, int id){

            UserModel u = GetUserByUsername(user);

            if(u != null){
                List<Trackermodel> trackers = GetTrackersByUser(user);
                trackers.Remove(trackers.FirstOrDefault(t => t.ID == id));
            }

            return _context.SaveChanges() != 0;
        }

        public UserModel GetUserByUsername(string name){
            return _context.UserInfo.SingleOrDefault(user => user.Username == name);
        }
    }
}