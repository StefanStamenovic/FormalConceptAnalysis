namespace FCA
{
    partial class SearchNeo4jLattice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchTBox = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.resultRTBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchTBox
            // 
            this.searchTBox.Location = new System.Drawing.Point(12, 12);
            this.searchTBox.Name = "searchTBox";
            this.searchTBox.Size = new System.Drawing.Size(390, 22);
            this.searchTBox.TabIndex = 0;
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(408, 11);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 1;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // resultRTBox
            // 
            this.resultRTBox.Location = new System.Drawing.Point(12, 62);
            this.resultRTBox.Name = "resultRTBox";
            this.resultRTBox.Size = new System.Drawing.Size(490, 376);
            this.resultRTBox.TabIndex = 2;
            this.resultRTBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Result";
            // 
            // SearchNeo4jLattice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resultRTBox);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.searchTBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SearchNeo4jLattice";
            this.Text = "Search Neo4j lattice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchTBox;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.RichTextBox resultRTBox;
        private System.Windows.Forms.Label label1;
    }
}