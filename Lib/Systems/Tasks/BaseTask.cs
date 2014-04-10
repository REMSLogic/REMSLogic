using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Lib.Systems.Tasks
{
    public interface ITask
    {
        void Run();
        string Serialize();
        void Deserialize(string state);
    }

    public abstract class BaseTask : ITask
    {
        #region ITask Implementation
        public abstract void Run();

        public virtual string Serialize()
        {
            // Derived classes can override this, but by default use JSON
            // for serialization.
            return JsonConvert.SerializeObject(this);
        }

        public virtual void Deserialize(string state)
        {
            // Convert the string into an object.  Then let RestoreState take it
            // from there.  This is split into two so that derrived ovjects don't
            // have to worry about re-implementing the JSON deserialization.
            RestoreState(JsonConvert.DeserializeObject(state));
        }
        #endregion

        #region Utility Methods
        protected virtual void RestoreState(Object state)
        {
            // MJL 2013-11-06 - This hasn't been tested at all.  Theoretically it
            // should work...

            // TODO: Test this method

            Type objectType = GetType();
            IEnumerable<PropertyInfo> properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(PropertyInfo property in properties)
                property.SetValue(this, property.GetValue(state, null), null);
        }
        #endregion
    }
}
