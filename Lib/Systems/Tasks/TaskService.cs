using System;
using System.Collections.Generic;
using Lib.Data;

namespace Lib.Systems.Tasks
{
    public class TaskService
    {
        /// <summary>
        /// Serialize a task to the database to be run at a later date.
        /// </summary>
        /// <param name="task">Any object that implements ITask</param>
        public static void ScheduleTask(BaseTaskRunner runner, DateTime run_at)
        {
            // MJL 2013-11-07 - The idea is to use reflection or an IoC container
            // (StructureMap, Unity, etc...) to instantiate an object based on its
            // fully qualified type name.  Serialize / Deserialize can then be
            // used to restore the object's state.  
            
            // The database table would look something like:
                // Tasks(id, type_name, state, run_at)

            // Saving a task would go something like:
                //string type_name = task.GetType().FullName;
                //string state = task.Serialize();

                //Database.Save(type_name, state, run_at)

            // Restoring a task would be something like
                // ITask instance = Activator.CreateInstance(Type.GetType(type_name));
                // instance.Deserialize(state);
                // instance.Run();

            Task task = new Task(0)
            {
                CreatedAt = DateTime.Now,
                RunAt = run_at,
                State = runner.Serialize(),
                Runner = runner.GetType().FullName
            };

            // make sure this was called.
            //task.State = Task.Serialize<T>(task);
            task.Save();
        }

        public static IList<Task> GetPending()
        {
            return Task.FindAllPending();
        }
    }
}
