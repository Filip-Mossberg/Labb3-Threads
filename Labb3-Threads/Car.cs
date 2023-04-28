using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_Threads
{
    internal class Car
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Speed { get; set; }
        public int Distance { get; set; }
        public TimeSpan RaceTime { get; set; }
        public TimeSpan ErrorTime { get; set; }
        public string color { get; set; }

        public Car(int iD, string name, double speed, string color)
        {
            ID = iD;
            Name = name;
            Speed = speed;
            this.color = color;
        }
    }
}
