using System.Windows.Forms;

namespace SoftwareNinjas.BranchAndReviewTools.Gui
{
    partial class HistoryWindow
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.historyContainer = new SoftwareNinjas.BranchAndReviewTools.Gui.History.HistoryContainer();
            this.SuspendLayout();
            //
            // historyContainer
            //
            this.historyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyContainer.Location = new System.Drawing.Point(0, 0);
            this.historyContainer.Name = "historyContainer";
            this.historyContainer.Size = new System.Drawing.Size(792, 550);
            this.historyContainer.TabIndex = 0;
            // 
            // HistoryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.historyContainer);
            this.KeyPreview = true;
            this.Name = "HistoryWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private SoftwareNinjas.BranchAndReviewTools.Gui.History.HistoryContainer historyContainer;
    }
}