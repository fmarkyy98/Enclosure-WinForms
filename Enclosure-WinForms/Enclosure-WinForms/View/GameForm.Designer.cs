namespace Enclosure_WinForms
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.largeBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showRecurion = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.playerLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.redScoreLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.blueScoreLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.saveBtn,
            this.loadBtn,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallBtn,
            this.mediumBtn,
            this.largeBtn});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.newGameToolStripMenuItem.Text = "New Game";
            // 
            // smallBtn
            // 
            this.smallBtn.Name = "smallBtn";
            this.smallBtn.Size = new System.Drawing.Size(180, 22);
            this.smallBtn.Text = "6 x 6";
            // 
            // mediumBtn
            // 
            this.mediumBtn.Name = "mediumBtn";
            this.mediumBtn.Size = new System.Drawing.Size(180, 22);
            this.mediumBtn.Text = "8 x 8";
            // 
            // largeBtn
            // 
            this.largeBtn.Name = "largeBtn";
            this.largeBtn.Size = new System.Drawing.Size(180, 22);
            this.largeBtn.Text = "10 x 10";
            // 
            // saveBtn
            // 
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(77, 20);
            this.saveBtn.Text = "Save Game";
            // 
            // loadBtn
            // 
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(79, 20);
            this.loadBtn.Text = "Load Game";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showRecurion});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem1.Text = "Debug";
            // 
            // showRecurion
            // 
            this.showRecurion.Name = "showRecurion";
            this.showRecurion.Size = new System.Drawing.Size(183, 22);
            this.showRecurion.Text = "Show recursive steps";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.playerLabel,
            this.toolStripStatusLabel3,
            this.redScoreLabel,
            this.toolStripStatusLabel5,
            this.blueScoreLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabel1.Text = "Next Player:";
            // 
            // playerLabel
            // 
            this.playerLabel.Name = "playerLabel";
            this.playerLabel.Size = new System.Drawing.Size(27, 17);
            this.playerLabel.Text = "Red";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(75, 17);
            this.toolStripStatusLabel3.Text = " |   Res Score:";
            // 
            // redScoreLabel
            // 
            this.redScoreLabel.Name = "redScoreLabel";
            this.redScoreLabel.Size = new System.Drawing.Size(13, 17);
            this.redScoreLabel.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabel5.Text = " |   Blue Score:";
            // 
            // blueScoreLabel
            // 
            this.blueScoreLabel.Name = "blueScoreLabel";
            this.blueScoreLabel.Size = new System.Drawing.Size(13, 17);
            this.blueScoreLabel.Text = "0";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 404);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.Text = "Enclosure The Game";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem smallBtn;
        private ToolStripMenuItem mediumBtn;
        private ToolStripMenuItem largeBtn;
        private ToolStripMenuItem saveBtn;
        private ToolStripMenuItem loadBtn;
        private StatusStrip statusStrip1;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel playerLabel;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel redScoreLabel;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripStatusLabel blueScoreLabel;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem showRecurion;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
    }
}