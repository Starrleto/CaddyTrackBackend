using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaddyTrack.Models.DTO
{
    public class UserInfoDTO
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? ProfilePicture { get; set; }
        public List<Trackermodel> Trackers { get; set; }

        public UserInfoDTO(int id, string n, string p, List<Trackermodel> t)   
        {
            UserID = id;
            Username = n;
            ProfilePicture = p;
            Trackers = t;
        }
    }
}