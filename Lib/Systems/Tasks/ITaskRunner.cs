using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Systems.Tasks
{
    public interface ITaskRunner
    {
        void Run();
        string Serialize();
    }
}
