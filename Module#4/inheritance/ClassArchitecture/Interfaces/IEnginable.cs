using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassArchitecture.Interfaces
{
    internal interface IEnginable
    {
        public void StartEngine();

        public void StopEngine();

        public void RunDiagnostics();
    }
}
