using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NextClosureAlgorithm;
using System.Diagnostics;
using Neo4jFCA;
using System.DirectoryServices.ActiveDirectory;

namespace FCA
{
    public partial class FormFCA : Form
    {
        /// <summary>
        /// formalni kontekst za koji se kreira latica
        /// </summary>
        FormalContext context;
        NextClosureAlgorithm.Domain.FormalContext newContext;
        /// <summary>
        /// putanja do tekstualnog fajla iz kog se parsira formalni kontekst
        /// </summary>
        string inputFile;
        /// <summary>
        /// putanja do foldera u kom se smesta rezultat
        /// </summary>
        string outputFolder;
        /// <summary>
        /// naziv rezultujuceg fajla
        /// </summary>
        string outputFile;
        /// <summary>
        /// meri vreme potrebno za kreiranje latice od formalnog konteksta
        /// </summary>
        private readonly Stopwatch nextClosureStop = new Stopwatch();
        public bool writeFormalContext { get; set; }
        private Neo4jDataProvider provider;
        
        public FormFCA()
        {
            InitializeComponent();
            backgroundWorker1.WorkerSupportsCancellation = true;
            cancelAsyncBtn.Enabled = false;
            provider = new Neo4jDataProvider();
        }

        private void selectInputBtn_Click(object sender, EventArgs e)
        {
            if (inputFileDialog.ShowDialog() == DialogResult.OK)
            {

                this.inputFile = inputFileDialog.FileName;
                String fileName = Path.GetFileNameWithoutExtension(inputFile);

                Size txtSize = TextRenderer.MeasureText(fileName, inputFileTxt.Font);
                inputFileTxt.Width = txtSize.Width;
                inputFileTxt.Height = txtSize.Height;
                inputFileTxt.Text = fileName;
            }

        }
        private void selectOutputBtn_Click(object sender, EventArgs e)
        {
            if (outputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                this.outputFolder = outputFolderDialog.SelectedPath;

                Size txtSize = TextRenderer.MeasureText(outputFolder, outputFolderTxt.Font);
                outputFolderTxt.Width = txtSize.Width;
                outputFolderTxt.Height = txtSize.Height;
                outputFolderTxt.Text = outputFolder;
            }
        }
        private async void formalConceptBtn_Click(object sender, EventArgs e)
        {
            lblNextClosure.Text = "Parsing formal context from file...";
            if (this.inputFile == "")
            {
                MessageBox.Show("Choose input file");
                return;
            }
            //this.context = await Appendix.ParseFormalContextAsync(this.inputFile);
            this.outputFolder = outputFolderTxt.Text;
            this.outputFile = outputFileNameTxt.Text;
            this.outputFile = this.outputFolder + "\\" + this.outputFile + ".txt";
            if (this.outputFolder == "" || this.outputFile == "")
            {
                MessageBox.Show("Choose output folder and file name");
                return;
            }
            this.writeFormalContext = writeFCChBox.Checked;
            RunWorkerAsync();
        }
        private void cancelAsyncBtn_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
                backgroundWorker1.CancelAsync();
        }
        private void testBasicBtn_Click(object sender, EventArgs e)
        {
            this.context = Test.basicFormalContext();
            string dateExt = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.outputFile = "../../../Test/output/test_Basic_" + dateExt + ".txt";

            RunWorkerAsync();
        }
        private void testBiosphereBtn_Click(object sender, EventArgs e)
        {
            this.context = Test.biosphereFormalContext();
            string dateExt = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.outputFile = "../../../Test/output/test_Biosphere_" + dateExt + ".txt";

            RunWorkerAsync();

        }
        /// <summary>
        /// pokrece se proces u pozadini (kreiranje latice i upis rezultata u fajl) 
        /// pri cemu se meri vreme potrebno za kreiranje latice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            nextClosureStop.Restart();
            e.Result = Test.performAlgorithm(this.context, this.outputFile, this.writeFormalContext,worker, e);
            if (e.Result == null)
                return;
            nextClosureStop.Stop();
        }
        /// <summary>
        /// poziva se po zavrsetku posla (cancel / error / complete)
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Formal concepts computation has been canceled.", "Canceled");
                
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                MessageBox.Show("Formal Concepts computation completed");
            }
            timer1.Stop();
            toggleBtns(true);
            clearTextBoxes();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblNextClosure.Text = "Next Closure time elapsed:\n"
                + nextClosureStop.Elapsed.ToString(@"hh\:mm\:ss\.f");
        }
        void RunWorkerAsync()
        {
            if (backgroundWorker1.IsBusy != true)
            {
                toggleBtns(false);
                timer1.Start();
                backgroundWorker1.RunWorkerAsync();
            }
        }
        void RunWorker2Async()
        {
            if (backgroundWorker2.IsBusy != true)
            {
                toggleBtns(false);
                timer1.Start();
                backgroundWorker2.RunWorkerAsync();
            }
        }
        void clearTextBoxes() {
            inputFileTxt.Text = "";
            outputFolderTxt.Text = "";
            outputFileNameTxt.Text = "";
        }
        void toggleBtns(bool enable) {
            groupBoxIO.Enabled = enable;
            formalConceptBtn.Enabled = enable;
            testBiosphereBtn.Enabled = enable;
            testBasicBtn.Enabled = enable;
            cancelAsyncBtn.Enabled = !enable;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            lblNextClosure.Text = "Parsing formal context from file...";
            if (this.inputFile == "")
            {
                MessageBox.Show("Choose input file");
                return;
            }
            this.newContext = await Appendix.ParseFormalContextAsync(this.inputFile);
            this.outputFolder = outputFolderTxt.Text;
            this.outputFile = outputFileNameTxt.Text;
            this.outputFile = this.outputFolder + "\\" + this.outputFile + ".txt";
            if (this.outputFolder == "" || this.outputFile == "")
            {
                MessageBox.Show("Choose output folder and file name");
                return;
            }
            this.writeFormalContext = writeFCChBox.Checked;
            RunWorker2Async();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            nextClosureStop.Restart();
            e.Result = Test.newperformAlgorithm(this.newContext, worker, e);
            provider.ClearDatabase();
            provider.ImportFCALattice((NextClosureAlgorithm.Domain.ConceptLattice)e.Result);

            if (e.Result == null)
                return;
            nextClosureStop.Stop();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Formal concepts computation has been canceled.", "Canceled");

            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                MessageBox.Show("Formal Concepts computation completed");
            }
            timer1.Stop();
            toggleBtns(true);
            clearTextBoxes();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var res = provider.SearchForObjects(textBoxQuery.Text);
            if(res==null)
            {
                richTextBoxResult.Text = "No results!";
            }
            else
            {
                richTextBoxResult.Text = res.Replace(",", "\n\n");
            }

        }
    }
}
