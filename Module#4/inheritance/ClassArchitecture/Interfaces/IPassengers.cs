using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Interfaces
{
    internal interface IPassengers
    {
        public int NumOfPassengers { get; set; }

        public void AcceptPassenger();
        public void DropOfPassenger();
    }
}
