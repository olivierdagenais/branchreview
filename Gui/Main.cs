﻿using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using SoftwareNinjas.BranchAndReviewTools.Core;
using SoftwareNinjas.BranchAndReviewTools.Gui.Properties;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    public partial class Main : Form
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ReadOnlyCollection<SearchableDataGridView> _searchableGrids;

        public Main()
        {
            InitializeComponent();
            // one per tab
            _searchableGrids = new ReadOnlyCollection<SearchableDataGridView>(new[]
            {
                taskGrid,
                null, // TODO: branches
                null, // TODO: activity
                null,
            });
            Load += Main_Load;
            FormClosing += Main_Closing;
            _taskRepository = new Mock.TaskRepository();
            taskGrid.DataTable = _taskRepository.LoadTasks();
        }

        void Main_Load(object sender, System.EventArgs e)
        {
            var settings = Settings.Default;
            ClientSize = settings.WindowSize;
            WindowState = settings.WindowState;
            menuStrip.Location = settings.MenuLocation;
            searchStrip.Location = settings.SearchLocation;
        }

        void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var settings = Settings.Default;
            settings.MenuLocation = new Point(menuStrip.Location.X - 3, menuStrip.Location.Y);
            settings.SearchLocation = searchStrip.Location;
            settings.WindowSize = ClientSize;
            settings.WindowState = WindowState;
            settings.Save();
        }

        private void exitMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void searchTextBox_TextChanged(object sender, System.EventArgs e)
        {
            var grid = _searchableGrids[tabs.SelectedIndex];
            if (grid != null)
            {
                grid.Filter = searchTextBox.Text;
            }
        }
    }
}