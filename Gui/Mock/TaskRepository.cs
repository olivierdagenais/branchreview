using System;
using System.Data;
using SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui.Mock
{
    class TaskRepository : ITaskRepository
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
    }
}
