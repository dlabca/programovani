namespace DungeonCrawler
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label roomNameLabel;
        private System.Windows.Forms.Label roomDescriptionLabel;
        private System.Windows.Forms.ListBox enemiesListBox;
        private System.Windows.Forms.ListBox itemsListBox;
        private System.Windows.Forms.TextBox commandTextBox;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.roomNameLabel = new System.Windows.Forms.Label();
            this.roomDescriptionLabel = new System.Windows.Forms.Label();
            this.enemiesListBox = new System.Windows.Forms.ListBox();
            this.itemsListBox = new System.Windows.Forms.ListBox();
            this.commandTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // roomNameLabel
            // 
            this.roomNameLabel.AutoSize = true;
            this.roomNameLabel.Location = new System.Drawing.Point(13, 13);
            this.roomNameLabel.Name = "roomNameLabel";
            this.roomNameLabel.Size = new System.Drawing.Size(75, 13);
            this.roomNameLabel.TabIndex = 0;
            this.roomNameLabel.Text = "Room Name";
            // 
            // roomDescriptionLabel
            // 
            this.roomDescriptionLabel.AutoSize = true;
            this.roomDescriptionLabel.Location = new System.Drawing.Point(13, 40);
            this.roomDescriptionLabel.Name = "roomDescriptionLabel";
            this.roomDescriptionLabel.Size = new System.Drawing.Size(100, 13);
            this.roomDescriptionLabel.TabIndex = 1;
            this.roomDescriptionLabel.Text = "Room Description";
            // 
            // enemiesListBox
            // 
            this.enemiesListBox.FormattingEnabled = true;
            this.enemiesListBox.Location = new System.Drawing.Point(13, 70);
            this.enemiesListBox.Name = "enemiesListBox";
            this.enemiesListBox.Size = new System.Drawing.Size(120, 95);
            this.enemiesListBox.TabIndex = 2;
            // 
            // itemsListBox
            // 
            this.itemsListBox.FormattingEnabled = true;
            this.itemsListBox.Location = new System.Drawing.Point(13, 180);
            this.itemsListBox.Name = "itemsListBox";
            this.itemsListBox.Size = new System.Drawing.Size(120, 95);
            this.itemsListBox.TabIndex = 3;
            // 
            // commandTextBox
            // 
            this.commandTextBox.Location = new System.Drawing.Point(13, 290);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(259, 20);
            this.commandTextBox.TabIndex = 4;
            this.commandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandTextBox_KeyDown);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 321);
            this.Controls.Add(this.commandTextBox);
            this.Controls.Add(this.itemsListBox);
            this.Controls.Add(this.enemiesListBox);
            this.Controls.Add(this.roomDescriptionLabel);
            this.Controls.Add(this.roomNameLabel);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
