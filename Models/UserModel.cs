using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// This is the format of how User Data will be stores

namespace CaddyTrack.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string? Username { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }

        public UserModel(string name, string s, string h, string p)
        {
            Username = name;
            Salt = s;
            Hash = h;
            ProfilePicture = p;
        }

        public UserModel(){}
    }
}