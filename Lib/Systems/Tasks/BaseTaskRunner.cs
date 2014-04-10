using System;
using Newtonsoft.Json;

namespace Lib.Systems.Tasks
{
    public abstract class BaseTaskRunner : ITaskRunner
    {
        #region Utility Methods
        public abstract void Run();
        
        public string Serialize()
        {
            // Derived classes can override this, but by default use JSON
            // for serialization.
            return JsonConvert.SerializeObject(this);
        }

        public static ITaskRunner Deserialize(string state, Type runnerType)
        {
            // Convert the string into an object.
            return (ITaskRunner)JsonConvert.DeserializeObject(state, runnerType);
        }
        #endregion
    }
}
