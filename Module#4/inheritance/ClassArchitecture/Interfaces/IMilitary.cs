using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Interfaces
{
    internal interface IMilitary
    {
        public int MaxAmmo { get; set; }
        public int CurrentAmmo { get; set; }

        public void StartOffensive();

        public void StopOffensive();

        public void StartRetreat();

        public void StopRetreat();

        public void GetAmmo();

        public void OpenFire();
    }
}
