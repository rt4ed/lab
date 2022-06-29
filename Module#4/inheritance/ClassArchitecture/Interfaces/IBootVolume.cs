using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Interfaces
{
    internal interface IBootVolume: ISwitchable
    {
        public int VolumeOfBoot { get; set; }
    }
}
