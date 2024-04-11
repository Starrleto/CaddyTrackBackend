using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaddyTrack.Models.DTO
{
    public class TrackerDTO
    {
        public string? Name { get; set; }
        public int StockYardage { get; set; }
        public int MaxYardage { get; set; }
        public int ConfidenceLevel { get; set; }

        public TrackerDTO(string n, int s, int m, int c)
        {
            Name = n;
            StockYardage = s;
            MaxYardage = m;
            ConfidenceLevel = c;
        }
    }
}