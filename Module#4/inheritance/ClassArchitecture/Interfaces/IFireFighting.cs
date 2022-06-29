using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Interfaces
{
    internal interface IFireFighting
    {
        public int VolumeOfWater { get; set; }
        public int FireFigtingsMembers { get; set; }
        public void ExtinguishAFire();
    }
}
