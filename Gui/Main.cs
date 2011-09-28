using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Main : Form
    {
        private readonly ITaskRepository _taskRepository;

        public Main()
        {
            InitializeComponent();
            _taskRepository = new Mock.TaskRepository();
            taskGrid.DataTable = _taskRepository.LoadTasks();
        }

        private void exitMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
