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
            this.importFileContextGBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.importFileFormatCbox = new System.Windows.Forms.ComboBox();
            this.preprocessimportFileAttributesCBox = new System.Windows.Forms.CheckBox();
            this.importContextBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.outputFileNameTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.formalConceptBtn = new System.Windows.Forms.Button();
            this.conceptsAlgorithmGBox = new System.Windows.Forms.GroupBox();
            this.algorithmTime = new System.Windows.Forms.Label();
            this.formalConceptsCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.alogrithmCBox = new System.Windows.Forms.ComboBox();
            this.lblNextClosure = new System.Windows.Forms.Label();
            this.cancelAsyncBtn = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.formalContextGBox = new System.Windows.Forms.GroupBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.context_name = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.context_preprocessed = new System.Windows.Forms.Label();
            this.context_attributesCount = new System.Windows.Forms.Label();
            this.context_objectsCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.exportContextFileGBox = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.exportLatticeNeo4jBtn = new System.Windows.Forms.Button();
            this.conceptLatticeGBox = new System.Windows.Forms.GroupBox();
            this.lattice_hight = new System.Windows.Forms.Label();
            this.lattice_computed = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.computeLatticeBtn = new System.Windows.Forms.Button();
            this.exportLatticeNeo4jGBox = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.neo4jPassword = new System.Windows.Forms.TextBox();
            this.neo4jUsername = new System.Windows.Forms.TextBox();
            this.neo4jConnectionString = new System.Windows.Forms.TextBox();
            this.exportLattice = new System.Windows.Forms.Button();
            this.exportLatticeFolderPath = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.exportLatticeFileName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.exportLatticeFileGBox = new System.Windows.Forms.GroupBox();
            this.searchNeo4JLatticeBtn = new System.Windows.Forms.Button();
            this.importFileContextGBox.SuspendLayout();
            this.conceptsAlgorithmGBox.SuspendLayout();
            this.formalContextGBox.SuspendLayout();
            this.exportContextFileGBox.SuspendLayout();
            this.conceptLatticeGBox.SuspendLayout();
            this.exportLatticeNeo4jGBox.SuspendLayout();
            this.exportLatticeFileGBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputFileTxt
            // 
            this.inputFileTxt.BackColor = System.Drawing.SystemColors.Control;
            this.inputFileTxt.Location = new System.Drawing.Point(11, 40);
            this.inputFileTxt.Margin = new System.Windows.Forms.Padding(4);
            this.inputFileTxt.Name = "inputFileTxt";
            this.inputFileTxt.ReadOnly = true;
            this.inputFileTxt.Size = new System.Drawing.Size(470, 22);
            this.inputFileTxt.TabIndex = 0;
            // 
            // outputFolderTxt
            // 
            this.outputFolderTxt.Enabled = false;
            this.outputFolderTxt.Location = new System.Drawing.Point(7, 39);
            this.outputFolderTxt.Margin = new System.Windows.Forms.Padding(4);
            this.outputFolderTxt.Name = "outputFolderTxt";
            this.outputFolderTxt.ReadOnly = true;
            this.outputFolderTxt.Size = new System.Drawing.Size(286, 22);
            this.outputFolderTxt.TabIndex = 1;
            // 
            // selectInputBtn
            // 
            this.selectInputBtn.Location = new System.Drawing.Point(489, 38);
            this.selectInputBtn.Margin = new System.Windows.Forms.Padding(4);
            this.selectInputBtn.Name = "selectInputBtn";
            this.selectInputBtn.Size = new System.Drawing.Size(30, 30);
            this.selectInputBtn.TabIndex = 2;
            this.selectInputBtn.Text = "...";
            this.selectInputBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.selectInputBtn.UseVisualStyleBackColor = true;
            this.selectInputBtn.Click += new System.EventHandler(this.selectInputBtn_Click);
            // 
            // selectOutputBtn
            // 
            this.selectOutputBtn.Enabled = false;
            this.selectOutputBtn.Location = new System.Drawing.Point(301, 35);
            this.selectOutputBtn.Margin = new System.Windows.Forms.Padding(4);
            this.selectOutputBtn.Name = "selectOutputBtn";
            this.selectOutputBtn.Size = new System.Drawing.Size(30, 30);
            this.selectOutputBtn.TabIndex = 3;
            this.selectOutputBtn.Text = "...";
            this.selectOutputBtn.UseVisualStyleBackColor = true;
            // 
            // inputFileDialog
            // 
            this.inputFileDialog.FileName = "inputFileDialog";
            // 
            // importFileContextGBox
            // 
            this.importFileContextGBox.AccessibleName = "";
            this.importFileContextGBox.Controls.Add(this.label8);
            this.importFileContextGBox.Controls.Add(this.importFileFormatCbox);
            this.importFileContextGBox.Controls.Add(this.preprocessimportFileAttributesCBox);
            this.importFileContextGBox.Controls.Add(this.importContextBtn);
            this.importFileContextGBox.Controls.Add(this.label1);
            this.importFileContextGBox.Controls.Add(this.inputFileTxt);
            this.importFileContextGBox.Controls.Add(this.selectInputBtn);
            this.importFileContextGBox.Location = new System.Drawing.Point(16, 15);
            this.importFileContextGBox.Margin = new System.Windows.Forms.Padding(4);
            this.importFileContextGBox.Name = "importFileContextGBox";
            this.importFileContextGBox.Padding = new System.Windows.Forms.Padding(4);
            this.importFileContextGBox.Size = new System.Drawing.Size(527, 164);
            this.importFileContextGBox.TabIndex = 4;
            this.importFileContextGBox.TabStop = false;
            this.importFileContextGBox.Text = "Import context from file";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Import data format";
            // 
            // importFileFormatCbox
            // 
            this.importFileFormatCbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.importFileFormatCbox.Location = new System.Drawing.Point(12, 86);
            this.importFileFormatCbox.Name = "importFileFormatCbox";
            this.importFileFormatCbox.Size = new System.Drawing.Size(508, 24);
            this.importFileFormatCbox.TabIndex = 15;
            this.importFileFormatCbox.SelectedIndexChanged += new System.EventHandler(this.importFileFormatCbox_SelectedIndexChanged);
            // 
            // preprocessimportFileAttributesCBox
            // 
            this.preprocessimportFileAttributesCBox.AutoSize = true;
            this.preprocessimportFileAttributesCBox.Location = new System.Drawing.Point(12, 125);
            this.preprocessimportFileAttributesCBox.Name = "preprocessimportFileAttributesCBox";
            this.preprocessimportFileAttributesCBox.Size = new System.Drawing.Size(165, 21);
            this.preprocessimportFileAttributesCBox.TabIndex = 14;
            this.preprocessimportFileAttributesCBox.Text = "Preprocess attributes";
            this.preprocessimportFileAttributesCBox.UseVisualStyleBackColor = true;
            // 
            // importContextBtn
            // 
            this.importContextBtn.Location = new System.Drawing.Point(355, 125);
            this.importContextBtn.Name = "importContextBtn";
            this.importContextBtn.Size = new System.Drawing.Size(165, 32);
            this.importContextBtn.TabIndex = 13;
            this.importContextBtn.Text = "Import context";
            this.importContextBtn.UseVisualStyleBackColor = true;
            this.importContextBtn.Click += new System.EventHandler(this.importContextBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Input file :";
            // 
            // outputFileNameTxt
            // 
            this.outputFileNameTxt.Enabled = false;
            this.outputFileNameTxt.Location = new System.Drawing.Point(131, 66);
            this.outputFileNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.outputFileNameTxt.Name = "outputFileNameTxt";
            this.outputFileNameTxt.Size = new System.Drawing.Size(162, 22);
            this.outputFileNameTxt.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(7, 69);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output file name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(7, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output folder:";
            // 
            // formalConceptBtn
            // 
            this.formalConceptBtn.Location = new System.Drawing.Point(333, 70);
            this.formalConceptBtn.Margin = new System.Windows.Forms.Padding(4);
            this.formalConceptBtn.Name = "formalConceptBtn";
            this.formalConceptBtn.Size = new System.Drawing.Size(195, 27);
            this.formalConceptBtn.TabIndex = 6;
            this.formalConceptBtn.Text = "Generate Formal Concepts";
            this.formalConceptBtn.UseVisualStyleBackColor = true;
            this.formalConceptBtn.Click += new System.EventHandler(this.formalConceptBtn_Click);
            // 
            // conceptsAlgorithmGBox
            // 
            this.conceptsAlgorithmGBox.Controls.Add(this.algorithmTime);
            this.conceptsAlgorithmGBox.Controls.Add(this.formalConceptsCount);
            this.conceptsAlgorithmGBox.Controls.Add(this.label11);
            this.conceptsAlgorithmGBox.Controls.Add(this.label6);
            this.conceptsAlgorithmGBox.Controls.Add(this.alogrithmCBox);
            this.conceptsAlgorithmGBox.Controls.Add(this.lblNextClosure);
            this.conceptsAlgorithmGBox.Controls.Add(this.formalConceptBtn);
            this.conceptsAlgorithmGBox.Controls.Add(this.cancelAsyncBtn);
            this.conceptsAlgorithmGBox.Location = new System.Drawing.Point(551, 15);
            this.conceptsAlgorithmGBox.Margin = new System.Windows.Forms.Padding(4);
            this.conceptsAlgorithmGBox.Name = "conceptsAlgorithmGBox";
            this.conceptsAlgorithmGBox.Padding = new System.Windows.Forms.Padding(4);
            this.conceptsAlgorithmGBox.Size = new System.Drawing.Size(536, 164);
            this.conceptsAlgorithmGBox.TabIndex = 8;
            this.conceptsAlgorithmGBox.TabStop = false;
            this.conceptsAlgorithmGBox.Text = "Concepts algorithm";
            // 
            // algorithmTime
            // 
            this.algorithmTime.AutoSize = true;
            this.algorithmTime.Location = new System.Drawing.Point(170, 104);
            this.algorithmTime.Name = "algorithmTime";
            this.algorithmTime.Size = new System.Drawing.Size(13, 17);
            this.algorithmTime.TabIndex = 11;
            this.algorithmTime.Text = "-";
            // 
            // formalConceptsCount
            // 
            this.formalConceptsCount.AutoSize = true;
            this.formalConceptsCount.Location = new System.Drawing.Point(171, 126);
            this.formalConceptsCount.Name = "formalConceptsCount";
            this.formalConceptsCount.Size = new System.Drawing.Size(13, 17);
            this.formalConceptsCount.TabIndex = 10;
            this.formalConceptsCount.Text = "-";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 17);
            this.label11.TabIndex = 9;
            this.label11.Text = "Formal concepts count:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Algorithm";
            // 
            // alogrithmCBox
            // 
            this.alogrithmCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alogrithmCBox.FormattingEnabled = true;
            this.alogrithmCBox.Location = new System.Drawing.Point(7, 39);
            this.alogrithmCBox.Name = "alogrithmCBox";
            this.alogrithmCBox.Size = new System.Drawing.Size(521, 24);
            this.alogrithmCBox.TabIndex = 7;
            // 
            // lblNextClosure
            // 
            this.lblNextClosure.AutoSize = true;
            this.lblNextClosure.Location = new System.Drawing.Point(8, 104);
            this.lblNextClosure.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNextClosure.Name = "lblNextClosure";
            this.lblNextClosure.Size = new System.Drawing.Size(155, 17);
            this.lblNextClosure.TabIndex = 0;
            this.lblNextClosure.Text = "Algorithm time elapsed:";
            // 
            // cancelAsyncBtn
            // 
            this.cancelAsyncBtn.Location = new System.Drawing.Point(225, 71);
            this.cancelAsyncBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cancelAsyncBtn.Name = "cancelAsyncBtn";
            this.cancelAsyncBtn.Size = new System.Drawing.Size(100, 26);
            this.cancelAsyncBtn.TabIndex = 8;
            this.cancelAsyncBtn.Text = "Cancel";
            this.cancelAsyncBtn.UseVisualStyleBackColor = true;
            this.cancelAsyncBtn.Click += new System.EventHandler(this.cancelAsyncBtn_Click);
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
            // formalContextGBox
            // 
            this.formalContextGBox.Controls.Add(this.clearBtn);
            this.formalContextGBox.Controls.Add(this.context_name);
            this.formalContextGBox.Controls.Add(this.label10);
            this.formalContextGBox.Controls.Add(this.context_preprocessed);
            this.formalContextGBox.Controls.Add(this.context_attributesCount);
            this.formalContextGBox.Controls.Add(this.context_objectsCount);
            this.formalContextGBox.Controls.Add(this.label9);
            this.formalContextGBox.Controls.Add(this.label5);
            this.formalContextGBox.Controls.Add(this.label4);
            this.formalContextGBox.Location = new System.Drawing.Point(16, 186);
            this.formalContextGBox.Name = "formalContextGBox";
            this.formalContextGBox.Size = new System.Drawing.Size(527, 98);
            this.formalContextGBox.TabIndex = 12;
            this.formalContextGBox.TabStop = false;
            this.formalContextGBox.Text = "Formal context";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(446, 69);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 8;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // context_name
            // 
            this.context_name.AutoEllipsis = true;
            this.context_name.AutoSize = true;
            this.context_name.Location = new System.Drawing.Point(126, 25);
            this.context_name.MaximumSize = new System.Drawing.Size(320, 20);
            this.context_name.Name = "context_name";
            this.context_name.Size = new System.Drawing.Size(13, 17);
            this.context_name.TabIndex = 7;
            this.context_name.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "Name:";
            // 
            // context_preprocessed
            // 
            this.context_preprocessed.AutoSize = true;
            this.context_preprocessed.Location = new System.Drawing.Point(390, 47);
            this.context_preprocessed.Name = "context_preprocessed";
            this.context_preprocessed.Size = new System.Drawing.Size(13, 17);
            this.context_preprocessed.TabIndex = 5;
            this.context_preprocessed.Text = "-";
            // 
            // context_attributesCount
            // 
            this.context_attributesCount.AutoSize = true;
            this.context_attributesCount.Location = new System.Drawing.Point(126, 67);
            this.context_attributesCount.Name = "context_attributesCount";
            this.context_attributesCount.Size = new System.Drawing.Size(13, 17);
            this.context_attributesCount.TabIndex = 4;
            this.context_attributesCount.Text = "-";
            // 
            // context_objectsCount
            // 
            this.context_objectsCount.AutoSize = true;
            this.context_objectsCount.Location = new System.Drawing.Point(126, 47);
            this.context_objectsCount.Name = "context_objectsCount";
            this.context_objectsCount.Size = new System.Drawing.Size(13, 17);
            this.context_objectsCount.TabIndex = 3;
            this.context_objectsCount.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(273, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "Preprocesed:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Attributes count:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Objects count:";
            // 
            // exportContextFileGBox
            // 
            this.exportContextFileGBox.Controls.Add(this.button3);
            this.exportContextFileGBox.Controls.Add(this.label2);
            this.exportContextFileGBox.Controls.Add(this.outputFolderTxt);
            this.exportContextFileGBox.Controls.Add(this.label3);
            this.exportContextFileGBox.Controls.Add(this.outputFileNameTxt);
            this.exportContextFileGBox.Controls.Add(this.selectOutputBtn);
            this.exportContextFileGBox.Location = new System.Drawing.Point(16, 290);
            this.exportContextFileGBox.Name = "exportContextFileGBox";
            this.exportContextFileGBox.Size = new System.Drawing.Size(527, 102);
            this.exportContextFileGBox.TabIndex = 11;
            this.exportContextFileGBox.TabStop = false;
            this.exportContextFileGBox.Text = "Export context to file";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(357, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(163, 30);
            this.button3.TabIndex = 11;
            this.button3.Text = "Export";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // exportLatticeNeo4jBtn
            // 
            this.exportLatticeNeo4jBtn.Location = new System.Drawing.Point(359, 38);
            this.exportLatticeNeo4jBtn.Name = "exportLatticeNeo4jBtn";
            this.exportLatticeNeo4jBtn.Size = new System.Drawing.Size(169, 25);
            this.exportLatticeNeo4jBtn.TabIndex = 13;
            this.exportLatticeNeo4jBtn.Text = "Export lattice to Neo4j";
            this.exportLatticeNeo4jBtn.UseVisualStyleBackColor = true;
            this.exportLatticeNeo4jBtn.Click += new System.EventHandler(this.exportLatticeNeo4jBtn_Click);
            // 
            // conceptLatticeGBox
            // 
            this.conceptLatticeGBox.Controls.Add(this.lattice_hight);
            this.conceptLatticeGBox.Controls.Add(this.lattice_computed);
            this.conceptLatticeGBox.Controls.Add(this.label13);
            this.conceptLatticeGBox.Controls.Add(this.label12);
            this.conceptLatticeGBox.Controls.Add(this.computeLatticeBtn);
            this.conceptLatticeGBox.Location = new System.Drawing.Point(551, 186);
            this.conceptLatticeGBox.Name = "conceptLatticeGBox";
            this.conceptLatticeGBox.Size = new System.Drawing.Size(536, 72);
            this.conceptLatticeGBox.TabIndex = 14;
            this.conceptLatticeGBox.TabStop = false;
            this.conceptLatticeGBox.Text = "Concepts lattice";
            // 
            // lattice_hight
            // 
            this.lattice_hight.AutoSize = true;
            this.lattice_hight.Location = new System.Drawing.Point(264, 47);
            this.lattice_hight.Name = "lattice_hight";
            this.lattice_hight.Size = new System.Drawing.Size(13, 17);
            this.lattice_hight.TabIndex = 4;
            this.lattice_hight.Text = "-";
            // 
            // lattice_computed
            // 
            this.lattice_computed.AutoSize = true;
            this.lattice_computed.Location = new System.Drawing.Point(264, 21);
            this.lattice_computed.Name = "lattice_computed";
            this.lattice_computed.Size = new System.Drawing.Size(13, 17);
            this.lattice_computed.TabIndex = 3;
            this.lattice_computed.Text = "-";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(173, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Lattice hight:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(173, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 17);
            this.label12.TabIndex = 1;
            this.label12.Text = "Computed:";
            // 
            // computeLatticeBtn
            // 
            this.computeLatticeBtn.Location = new System.Drawing.Point(6, 21);
            this.computeLatticeBtn.Name = "computeLatticeBtn";
            this.computeLatticeBtn.Size = new System.Drawing.Size(117, 43);
            this.computeLatticeBtn.TabIndex = 0;
            this.computeLatticeBtn.Text = "Compute lattice";
            this.computeLatticeBtn.UseVisualStyleBackColor = true;
            this.computeLatticeBtn.Click += new System.EventHandler(this.computeLatticeBtn_Click);
            // 
            // exportLatticeNeo4jGBox
            // 
            this.exportLatticeNeo4jGBox.Controls.Add(this.searchNeo4JLatticeBtn);
            this.exportLatticeNeo4jGBox.Controls.Add(this.label17);
            this.exportLatticeNeo4jGBox.Controls.Add(this.label16);
            this.exportLatticeNeo4jGBox.Controls.Add(this.label15);
            this.exportLatticeNeo4jGBox.Controls.Add(this.exportLatticeNeo4jBtn);
            this.exportLatticeNeo4jGBox.Controls.Add(this.neo4jPassword);
            this.exportLatticeNeo4jGBox.Controls.Add(this.neo4jUsername);
            this.exportLatticeNeo4jGBox.Controls.Add(this.neo4jConnectionString);
            this.exportLatticeNeo4jGBox.Location = new System.Drawing.Point(551, 264);
            this.exportLatticeNeo4jGBox.Name = "exportLatticeNeo4jGBox";
            this.exportLatticeNeo4jGBox.Size = new System.Drawing.Size(534, 135);
            this.exportLatticeNeo4jGBox.TabIndex = 15;
            this.exportLatticeNeo4jGBox.TabStop = false;
            this.exportLatticeNeo4jGBox.Text = "Export lattice";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 102);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 17);
            this.label17.TabIndex = 16;
            this.label17.Text = "Password:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 17);
            this.label16.TabIndex = 15;
            this.label16.Text = "Username :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(157, 17);
            this.label15.TabIndex = 14;
            this.label15.Text = "Neo4j connection string";
            // 
            // neo4jPassword
            // 
            this.neo4jPassword.Location = new System.Drawing.Point(95, 99);
            this.neo4jPassword.Margin = new System.Windows.Forms.Padding(4);
            this.neo4jPassword.Name = "neo4jPassword";
            this.neo4jPassword.Size = new System.Drawing.Size(250, 22);
            this.neo4jPassword.TabIndex = 7;
            // 
            // neo4jUsername
            // 
            this.neo4jUsername.Location = new System.Drawing.Point(95, 69);
            this.neo4jUsername.Margin = new System.Windows.Forms.Padding(4);
            this.neo4jUsername.Name = "neo4jUsername";
            this.neo4jUsername.Size = new System.Drawing.Size(250, 22);
            this.neo4jUsername.TabIndex = 7;
            // 
            // neo4jConnectionString
            // 
            this.neo4jConnectionString.Location = new System.Drawing.Point(10, 39);
            this.neo4jConnectionString.Margin = new System.Windows.Forms.Padding(4);
            this.neo4jConnectionString.Name = "neo4jConnectionString";
            this.neo4jConnectionString.Size = new System.Drawing.Size(335, 22);
            this.neo4jConnectionString.TabIndex = 7;
            // 
            // exportLattice
            // 
            this.exportLattice.Enabled = false;
            this.exportLattice.Location = new System.Drawing.Point(365, 32);
            this.exportLattice.Name = "exportLattice";
            this.exportLattice.Size = new System.Drawing.Size(163, 30);
            this.exportLattice.TabIndex = 11;
            this.exportLattice.Text = "Export";
            this.exportLattice.UseVisualStyleBackColor = true;
            // 
            // exportLatticeFolderPath
            // 
            this.exportLatticeFolderPath.Enabled = false;
            this.exportLatticeFolderPath.Location = new System.Drawing.Point(10, 36);
            this.exportLatticeFolderPath.Margin = new System.Windows.Forms.Padding(4);
            this.exportLatticeFolderPath.Name = "exportLatticeFolderPath";
            this.exportLatticeFolderPath.ReadOnly = true;
            this.exportLatticeFolderPath.Size = new System.Drawing.Size(286, 22);
            this.exportLatticeFolderPath.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(301, 32);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(30, 30);
            this.button5.TabIndex = 3;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(7, 66);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Output file name:";
            // 
            // exportLatticeFileName
            // 
            this.exportLatticeFileName.Enabled = false;
            this.exportLatticeFileName.Location = new System.Drawing.Point(131, 63);
            this.exportLatticeFileName.Margin = new System.Windows.Forms.Padding(4);
            this.exportLatticeFileName.Name = "exportLatticeFileName";
            this.exportLatticeFileName.Size = new System.Drawing.Size(162, 22);
            this.exportLatticeFileName.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Enabled = false;
            this.label14.Location = new System.Drawing.Point(7, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 17);
            this.label14.TabIndex = 4;
            this.label14.Text = "Output folder:";
            // 
            // exportLatticeFileGBox
            // 
            this.exportLatticeFileGBox.Controls.Add(this.exportLatticeFolderPath);
            this.exportLatticeFileGBox.Controls.Add(this.button5);
            this.exportLatticeFileGBox.Controls.Add(this.label7);
            this.exportLatticeFileGBox.Controls.Add(this.exportLatticeFileName);
            this.exportLatticeFileGBox.Controls.Add(this.exportLattice);
            this.exportLatticeFileGBox.Controls.Add(this.label14);
            this.exportLatticeFileGBox.Enabled = false;
            this.exportLatticeFileGBox.Location = new System.Drawing.Point(551, 405);
            this.exportLatticeFileGBox.Name = "exportLatticeFileGBox";
            this.exportLatticeFileGBox.Size = new System.Drawing.Size(534, 95);
            this.exportLatticeFileGBox.TabIndex = 16;
            this.exportLatticeFileGBox.TabStop = false;
            this.exportLatticeFileGBox.Text = "Export lattice file";
            // 
            // searchNeo4JLatticeBtn
            // 
            this.searchNeo4JLatticeBtn.Location = new System.Drawing.Point(359, 72);
            this.searchNeo4JLatticeBtn.Name = "searchNeo4JLatticeBtn";
            this.searchNeo4JLatticeBtn.Size = new System.Drawing.Size(169, 23);
            this.searchNeo4JLatticeBtn.TabIndex = 17;
            this.searchNeo4JLatticeBtn.Text = "SearchNeo4JLattice";
            this.searchNeo4JLatticeBtn.UseVisualStyleBackColor = true;
            this.searchNeo4JLatticeBtn.Click += new System.EventHandler(this.searchNeo4JLatticeBtn_Click);
            // 
            // FormFCA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 512);
            this.Controls.Add(this.exportLatticeFileGBox);
            this.Controls.Add(this.exportLatticeNeo4jGBox);
            this.Controls.Add(this.conceptLatticeGBox);
            this.Controls.Add(this.exportContextFileGBox);
            this.Controls.Add(this.formalContextGBox);
            this.Controls.Add(this.conceptsAlgorithmGBox);
            this.Controls.Add(this.importFileContextGBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormFCA";
            this.Text = "FCA";
            this.importFileContextGBox.ResumeLayout(false);
            this.importFileContextGBox.PerformLayout();
            this.conceptsAlgorithmGBox.ResumeLayout(false);
            this.conceptsAlgorithmGBox.PerformLayout();
            this.formalContextGBox.ResumeLayout(false);
            this.formalContextGBox.PerformLayout();
            this.exportContextFileGBox.ResumeLayout(false);
            this.exportContextFileGBox.PerformLayout();
            this.conceptLatticeGBox.ResumeLayout(false);
            this.conceptLatticeGBox.PerformLayout();
            this.exportLatticeNeo4jGBox.ResumeLayout(false);
            this.exportLatticeNeo4jGBox.PerformLayout();
            this.exportLatticeFileGBox.ResumeLayout(false);
            this.exportLatticeFileGBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox inputFileTxt;
        private System.Windows.Forms.TextBox outputFolderTxt;
        private System.Windows.Forms.Button selectInputBtn;
        private System.Windows.Forms.Button selectOutputBtn;
        private System.Windows.Forms.OpenFileDialog inputFileDialog;
        private System.Windows.Forms.FolderBrowserDialog outputFolderDialog;
        private System.Windows.Forms.GroupBox importFileContextGBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputFileNameTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button formalConceptBtn;
        private System.Windows.Forms.GroupBox conceptsAlgorithmGBox;
        private System.Windows.Forms.Label lblNextClosure;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cancelAsyncBtn;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button importContextBtn;
        private System.Windows.Forms.GroupBox formalContextGBox;
        private System.Windows.Forms.GroupBox exportContextFileGBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button exportLatticeNeo4jBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox alogrithmCBox;
        private System.Windows.Forms.GroupBox conceptLatticeGBox;
        private System.Windows.Forms.Button computeLatticeBtn;
        private System.Windows.Forms.GroupBox exportLatticeNeo4jGBox;
        private System.Windows.Forms.Button exportLattice;
        private System.Windows.Forms.TextBox exportLatticeFolderPath;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox exportLatticeFileName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox importFileFormatCbox;
        private System.Windows.Forms.CheckBox preprocessimportFileAttributesCBox;
        private System.Windows.Forms.Label context_preprocessed;
        private System.Windows.Forms.Label context_attributesCount;
        private System.Windows.Forms.Label context_objectsCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label context_name;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Label algorithmTime;
        private System.Windows.Forms.Label formalConceptsCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lattice_hight;
        private System.Windows.Forms.Label lattice_computed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox neo4jConnectionString;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox neo4jPassword;
        private System.Windows.Forms.TextBox neo4jUsername;
        private System.Windows.Forms.GroupBox exportLatticeFileGBox;
        private System.Windows.Forms.Button searchNeo4JLatticeBtn;
    }
}

