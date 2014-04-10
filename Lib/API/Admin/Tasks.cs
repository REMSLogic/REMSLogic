using System;
using System.Collections.Generic;
using System.Web;
using Framework.API;
using Lib.Data;
using Lib.Systems.Tasks;

namespace Lib.API.Admin
{
    public class Tasks : Base
    {
        [SecurityRole( "view_admin" )]
        [Method("Admin/Tasks/Run")]
        public static ReturnObject Run(HttpContext context, long task_id)
        {
            if (task_id <= 0)
                return new ReturnObject {Error = true, Message = "Invalid Task."};

            Task item = new Task(task_id);
            Type runnerType = Type.GetType(item.Runner);

            if(runnerType == null)
                return new ReturnObject{Error = true, Message = "Invalid task runner requested."};

            BaseTaskRunner.Deserialize(item.State, runnerType).Run();

            return new ReturnObject
            {
                Error = false,
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "The task completed successfully.",
                        title = "Task Run"
                    }
                }
            };
        }

        [SecurityRole("view_admin")]
        [Method("Admin/Tasks/RunAll")]
        public static ReturnObject RunAll(HttpContext context)
        {
            IList<Task> items = Task.FindAllPending();

            if(items == null || items.Count <= 0)
                return new ReturnObject{Error = true, Message = "No pending tasks found."};

            foreach(Task item in items)
            {
                Type runnerType = Type.GetType(item.Runner);

                if(runnerType != null)
                    BaseTaskRunner.Deserialize(item.State, runnerType).Run();
            }

            return new ReturnObject
            {
                Error = false,
                Growl = new ReturnGrowlObject
                {
                    Type = "default",
                    Vars = new ReturnGrowlVarsObject
                    {
                        text = "All tasks completed successfully.",
                        title = "Task Run"
                    }
                }
            };
        }
    }
}
