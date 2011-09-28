using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Mock
{
    internal class TaskRepository : ITaskRepository
    {
        private readonly DataTable _tasks = new DataTable
        {
            Columns =
            {
                new DataColumn("ID", typeof(int)) { Caption = "Case" },
                {"Type", typeof(string)},
                {"Title", typeof(string)},
                {"Status", typeof(string)},
                new DataColumn("RemainingTime", typeof(TimeSpan)) { Caption = "Remaining Time" },
            },
            Rows =
            {
                {1, "Enhancement", "Extract \"Run JUnit test(s)\" from sub-menu", "Fixed", TimeSpan.Zero},
                {2, "Enhancement", "Use Eclipse's JDT templating mechanism", "New", new TimeSpan(3, 0, 0, 0)},
                {3, "Enhancement", "Generate smart templates", "Fixed", TimeSpan.Zero},
                {4, "Defect", "Implicit default constructor not called", "Accepted", new TimeSpan(1, 0, 0, 0)},
                {5, "Defect", "Test method body can have duplicate variable names", "Accepted", TimeSpan.Zero},
                {6, "Defect", "Not all parameter types are supported", "Accepted", TimeSpan.Zero},
                {7, "Defect", "Variables representing class instance parameters should be initialized with an instance", "Accepted", TimeSpan.Zero},
                {8, "Defect", "Generating tests for a class which has overloaded methods will produce duplicate test method names", "Accepted", TimeSpan.Zero},
                {9, "Defect", "Constructor is considered a method to test", "Accepted", TimeSpan.Zero},
            }
        };

        public DataTable LoadTasks()
        {
            return _tasks;
        }

        public IList<MenuAction> GetActionsForTask(object taskId)
        {
            IList<MenuAction> actionsForTask;
            if (null == taskId)
            {
                actionsForTask = new[]
                {
                    new MenuAction("create", "Create Bug", true, () => Debug.WriteLine("Creating bug")),
                    new MenuAction("createFeature", "Create Feature", true, () => Debug.WriteLine("Creating feature")),
                    new MenuAction("createTask", "Create Task", true, () => Debug.WriteLine("Creating task")),
                };
            }
            else
            {
                var id = (int) taskId;
                var row = _tasks.Select("[ID] = " + id).FirstOrDefault();
                actionsForTask = new[]
                {
                    new MenuAction("open", "&Open && Launch", true, () => Debug.WriteLine("Opening task {0}", id)),
                    new MenuAction("resolve", "&Resolve", (string) row["Status"] == "Accepted",
                                   () => Debug.WriteLine("Resolving task {0}", id)),
                    new MenuAction("sep1", MenuAction.Separator, true, null),
                    new MenuAction("close", "&Close", (string) row["Status"] == "Fixed",
                                    () => Debug.WriteLine("Closing task {0}", id)),
                };
            }
            return actionsForTask;
        }
    }
}
