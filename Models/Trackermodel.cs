using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaddyTrack.Models
{
    public class Trackermodel
    {
        public int ID { get; set; }
        public int UserModelID { get; set; }
        public string? Name { get; set; }
        public int StockYardage { get; set; }
        public int MaxYardage { get; set; }
        public int ConfidenceLevel { get; set; }

        public Trackermodel(string n, int s, int m, int c)
        {
            Name = n;
            StockYardage = s;
            MaxYardage = m;
            ConfidenceLevel = c;
        }
        public Trackermodel(){}
    }
}