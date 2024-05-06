using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaddyTrack.Models.Hub;

namespace CaddyTrack.Services.Context
{
    public class SharedDB
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();

        public ConcurrentDictionary<string, UserConnection> connections => _connections;
        
    }
}