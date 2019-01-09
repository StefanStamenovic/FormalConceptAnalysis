namespace FCA
{
    partial class FormFCA
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
            this.components = new System.ComponentModel.Container();
            this.inputFileTxt = new System.Windows.Forms.TextBox();
            this.outputFolderTxt = new System.Windows.Forms.TextBox();
            this.selectInputBtn = new System.Windows.Forms.Button();
            this.selectOutputBtn = new System.Windows.Forms.Button();
            this.inputFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.outputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxIO = new System.Windows.Forms.GroupBox();
            this.outputFileNameTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.formalConceptBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.testBasicBtn = new System.Windows.Forms.Button();
            this.testBiosphereBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblNextClosure = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cancelAsyncBtn = new System.Windows.Forms.Button();
            this.writeFCChBox = new System.Windows.Forms.CheckBox();
            this.groupBoxIO.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputFileTxt
            // 
            this.inputFileTxt.Location = new System.Drawing.Point(6, 62);
            this.inputFileTxt.Name = "inputFileTxt";
            this.inputFileTxt.ReadOnly = true;
            this.inputFileTxt.Size = new System.Drawing.Size(131, 20);
            this.inputFileTxt.TabIndex = 0;
            // 
            // outputFolderTxt
            // 
            this.outputFolderTxt.Location = new System.Drawing.Point(6, 144);
            this.outputFolderTxt.Name = "outputFolderTxt";
            this.outputFolderTxt.ReadOnly = true;
            this.outputFolderTxt.Size = new System.Drawing.Size(131, 20);
            this.outputFolderTxt.TabIndex = 1;
            // 
            // selectInputBtn
            // 
            this.selectInputBtn.Location = new System.Drawing.Point(6, 19);
            this.selectInputBtn.Name = "selectInputBtn";
            this.selectInputBtn.Size = new System.Drawing.Size(111, 23);
            this.selectInputBtn.TabIndex = 2;
            this.selectInputBtn.Text = "choose input file";
            this.selectInputBtn.UseVisualStyleBackColor = true;
            this.selectInputBtn.Click += new System.EventHandler(this.selectInputBtn_Click);
            // 
            // selectOutputBtn
            // 
            this.selectOutputBtn.Location = new System.Drawing.Point(6, 99);
            this.selectOutputBtn.Name = "selectOutputBtn";
            this.selectOutputBtn.Size = new System.Drawing.Size(125, 24);
            this.selectOutputBtn.TabIndex = 3;
            this.selectOutputBtn.Text = "choose output folder";
            this.selectOutputBtn.UseVisualStyleBackColor = true;
            this.selectOutputBtn.Click += new System.EventHandler(this.selectOutputBtn_Click);
            // 
            // inputFileDialog
            // 
            this.inputFileDialog.FileName = "inputFileDialog";
            // 
            // groupBoxIO
            // 
            this.groupBoxIO.Controls.Add(this.writeFCChBox);
            this.groupBoxIO.Controls.Add(this.outputFileNameTxt);
            this.groupBoxIO.Controls.Add(this.label1);
            this.groupBoxIO.Controls.Add(this.label3);
            this.groupBoxIO.Controls.Add(this.inputFileTxt);
            this.groupBoxIO.Controls.Add(this.label2);
            this.groupBoxIO.Controls.Add(this.selectInputBtn);
            this.groupBoxIO.Controls.Add(this.outputFolderTxt);
            this.groupBoxIO.Controls.Add(this.selectOutputBtn);
            this.groupBoxIO.Location = new System.Drawing.Point(12, 12);
            this.groupBoxIO.Name = "groupBoxIO";
            this.groupBoxIO.Size = new System.Drawing.Size(395, 217);
            this.groupBoxIO.TabIndex = 4;
            this.groupBoxIO.TabStop = false;
            this.groupBoxIO.Text = "Input and Output";
            // 
            // outputFileNameTxt
            // 
            this.outputFileNameTxt.Location = new System.Drawing.Point(6, 183);
            this.outputFileNameTxt.Name = "outputFileNameTxt";
            this.outputFileNameTxt.Size = new System.Drawing.Size(131, 20);
            this.outputFileNameTxt.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "input file path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "output file name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "output folder:";
            // 
            // formalConceptBtn
            // 
            this.formalConceptBtn.Location = new System.Drawing.Point(12, 246);
            this.formalConceptBtn.Name = "formalConceptBtn";
            this.formalConceptBtn.Size = new System.Drawing.Size(131, 39);
            this.formalConceptBtn.TabIndex = 6;
            this.formalConceptBtn.Text = "Generate Formal Concepts";
            this.formalConceptBtn.UseVisualStyleBackColor = true;
            this.formalConceptBtn.Click += new System.EventHandler(this.formalConceptBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.testBasicBtn);
            this.groupBox3.Controls.Add(this.testBiosphereBtn);
            this.groupBox3.Location = new System.Drawing.Point(12, 332);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(241, 110);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test";
            // 
            // testBasicBtn
            // 
            this.testBasicBtn.Location = new System.Drawing.Point(6, 30);
            this.testBasicBtn.Name = "testBasicBtn";
            this.testBasicBtn.Size = new System.Drawing.Size(157, 23);
            this.testBasicBtn.TabIndex = 1;
            this.testBasicBtn.Text = "Basic Formal Context";
            this.testBasicBtn.UseVisualStyleBackColor = true;
            this.testBasicBtn.Click += new System.EventHandler(this.testBasicBtn_Click);
            // 
            // testBiosphereBtn
            // 
            this.testBiosphereBtn.Location = new System.Drawing.Point(6, 59);
            this.testBiosphereBtn.Name = "testBiosphereBtn";
            this.testBiosphereBtn.Size = new System.Drawing.Size(157, 23);
            this.testBiosphereBtn.TabIndex = 0;
            this.testBiosphereBtn.Text = "Biosphere Formal Context";
            this.testBiosphereBtn.UseVisualStyleBackColor = true;
            this.testBiosphereBtn.Click += new System.EventHandler(this.testBiosphereBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblNextClosure);
            this.groupBox2.Location = new System.Drawing.Point(462, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(393, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time Measurements";
            // 
            // lblNextClosure
            // 
            this.lblNextClosure.AutoSize = true;
            this.lblNextClosure.Location = new System.Drawing.Point(3, 16);
            this.lblNextClosure.Name = "lblNextClosure";
            this.lblNextClosure.Size = new System.Drawing.Size(132, 13);
            this.lblNextClosure.TabIndex = 0;
            this.lblNextClosure.Text = "Next Closure time elapsed:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cancelAsyncBtn
            // 
            this.cancelAsyncBtn.Location = new System.Drawing.Point(12, 291);
            this.cancelAsyncBtn.Name = "cancelAsyncBtn";
            this.cancelAsyncBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelAsyncBtn.TabIndex = 8;
            this.cancelAsyncBtn.Text = "Cancel";
            this.cancelAsyncBtn.UseVisualStyleBackColor = true;
            this.cancelAsyncBtn.Click += new System.EventHandler(this.cancelAsyncBtn_Click);
            // 
            // writeFCChBox
            // 
            this.writeFCChBox.AutoSize = true;
            this.writeFCChBox.Location = new System.Drawing.Point(254, 186);
            this.writeFCChBox.Name = "writeFCChBox";
            this.writeFCChBox.Size = new System.Drawing.Size(124, 17);
            this.writeFCChBox.TabIndex = 10;
            this.writeFCChBox.Text = "Write Formal Context";
            this.writeFCChBox.UseVisualStyleBackColor = true;
            // 
            // FormFCA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 466);
            this.Controls.Add(this.cancelAsyncBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.formalConceptBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxIO);
            this.Name = "FormFCA";
            this.Text = "FCA";
            this.groupBoxIO.ResumeLayout(false);
            this.groupBoxIO.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox inputFileTxt;
        private System.Windows.Forms.TextBox outputFolderTxt;
        private System.Windows.Forms.Button selectInputBtn;
        private System.Windows.Forms.Button selectOutputBtn;
        private System.Windows.Forms.OpenFileDialog inputFileDialog;
        private System.Windows.Forms.FolderBrowserDialog outputFolderDialog;
        private System.Windows.Forms.GroupBox groupBoxIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputFileNameTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button formalConceptBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button testBiosphereBtn;
        private System.Windows.Forms.Button testBasicBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblNextClosure;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cancelAsyncBtn;
        private System.Windows.Forms.CheckBox writeFCChBox;
    }
}

