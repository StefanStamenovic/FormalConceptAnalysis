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
using System.Diagnostics;
using Neo4jFCA;
using System.DirectoryServices.ActiveDirectory;
using FCAA.Data.Lattice;
using FCAA.FormalConceptAlgorithms;
using FCAA.Data;
using FCAA.DataImport;
using FCAA.DataImport.ContextFileImporters;

namespace FCA
{
    public partial class FormFCA : Form
    {


        #region Import context file data       

        private bool ContextImported = false;
        /// <summary>
        /// putanja do tekstualnog fajla iz kog se parsira formalni kontekst
        /// </summary>
        private string FormalContextImportFilePath;
        private IContextFileImporter ContextFileImporter;

        #endregion

        #region Formal context data

        /// <summary>
        /// formalni kontekst za koji se kreira latica
        /// </summary>
        private FormalContext Context;

        #endregion

        #region Formal context file export data

        private string ExportFormalContextFolderPath;

        #endregion

        #region Formal concepts algorithm data

        private IFormalConceptAlgorithm FormalConceptAlgorithm;
        private ICollection<FormalConcept> FormalConcepts;
        /// <summary>
        /// meri vreme potrebno za racnunanje foramalnih koncepta
        /// </summary>
        private readonly Stopwatch algorithmStopWatch = new Stopwatch();

        #endregion

        #region Concept lattice data

        private ConceptLattice ConceptLattice;

        #endregion

        #region Neo4j data

        private bool Neo4jDefaultInitialized = false;
        private string DefaultNeo4jConnectionString = "http://localhost:11002/db/data";
        private string DefaultNeo4jUsername = "neo4j";
        private string DefaultNeo4jPassword = "root";

        private Neo4jDataProvider Neo4JProvider;

        #endregion

        public FormFCA()
        {
            InitializeComponent();
            InitializeForm();

            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        #region Import context file

        private void selectInputBtn_Click(object sender, EventArgs e)
        {
            if (inputFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.FormalContextImportFilePath = inputFileDialog.FileName;
                inputFileTxt.Text = FormalContextImportFilePath;
            }
        }

        private void importFileFormatCbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var importer = (ContextFileImporters)((dynamic)comboBox.SelectedItem).Id;
            this.ContextFileImporter = ContextFileImporterFactory.ProduceImporter(importer);
        }

        private async void importContextBtn_Click(object sender, EventArgs e)
        {
            if (ContextImported || string.IsNullOrEmpty(FormalContextImportFilePath))
            {
                MessageBox.Show("Choose input file");
                return;
            }
            // Using selected file format format
            try
            {
                this.Context = await ContextFileImporter.ImportContextAsync(FormalContextImportFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to import context from file");
                return;
            }
            UnlockUIOnImportContext();
        }
        private void UnlockUIOnImportContext()
        {
            this.importFileContextGBox.Enabled = false;
            UpdateFormalContextGroupBox();
            this.conceptsAlgorithmGBox.Enabled = true;
        }

        #endregion

        #region Formal context

        private void clearBtn_Click(object sender, EventArgs e)
        {
            InitializeForm();
        }

        #endregion

        #region Export context file (not implemented)

        //private void selectOutputBtn_Click(object sender, EventArgs e)
        //{
        //    if (outputFolderDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        this.ExportFormalContextFolderPath = outputFolderDialog.SelectedPath;
        //        outputFolderTxt.Text = ExportFormalContextFolderPath;
        //    }
        //}

        #endregion

        #region Concept algorithm

        private void formalConceptBtn_Click(object sender, EventArgs e)
        {
            LockUIOnFormalConceptsComputation();

            var selectedAlgorithm = (FormalConceptAlgorithms)((dynamic)alogrithmCBox.SelectedItem).Id;
            FormalConceptAlgorithm = FormalConceptAlgorithmFactory.ProduceAlgorithm(selectedAlgorithm, this.Context);
            RunWorkerAsync();
        }
        private void LockUIOnFormalConceptsComputation()
        {
            this.formalContextGBox.Enabled = false;
            this.conceptLatticeGBox.Enabled = false;
            ResetConceptLatticeGroupBox();
            this.exportLatticeNeo4jGBox.Enabled = false;
            ResetExportConceptLatticeNeo4jGroupBox();

            this.formalConceptBtn.Enabled = false;
            this.cancelAsyncBtn.Enabled = true;
        }

        private void cancelAsyncBtn_Click(object sender, EventArgs e)
        {
            // Canceling and reinitializing background worker
            var arg = new RunWorkerCompletedEventArgs(null, null, true);
            backgroundWorker1_RunWorkerCompleted(backgroundWorker1, arg);

            backgroundWorker1.Dispose();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        #endregion

        #region Algorithm computation background worker

        void RunWorkerAsync()
        {
            timer1.Start();
            backgroundWorker1.RunWorkerAsync();
        }
        /// <summary>
        /// pokrece se proces u pozadini (kreiranje latice i upis rezultata u fajl) 
        /// pri cemu se meri vreme potrebno za kreiranje latice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            algorithmStopWatch.Restart();

            // Performing the algorithm
            this.FormalConcepts = FormalConceptAlgorithm.FormalConcepts().ToList();

            algorithmStopWatch.Stop();
        }

        /// <summary>
        /// poziva se po zavrsetku posla (cancel / error / complete)
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Stop();
            algorithmTime.Text = algorithmStopWatch.Elapsed.ToString(@"hh\:mm\:ss\.f");
            if (e.Cancelled == true || e.Error != null)
            {
                // UI reset on error
                UnlockUIOnFailedFormalConceptsComputation();
                if (e.Cancelled == true)
                    MessageBox.Show("Formal concepts computation has been canceled.", "Canceled");
                else
                    MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                // UI unlock on compleated computation
                UnlockUIOnSuccessfullFormalConceptsComputation();

                MessageBox.Show("Formal Concepts computation completed");
            }
        }
        private void UnlockUIOnFailedFormalConceptsComputation()
        {
            ResetConceptsAlgorithmGroupBox();
            this.formalContextGBox.Enabled = true;
            this.conceptLatticeGBox.Enabled = false;
            ResetConceptLatticeGroupBox();
            this.exportLatticeNeo4jGBox.Enabled = false;
            ResetExportConceptLatticeNeo4jGroupBox();

            this.formalConceptBtn.Enabled = true;
            this.cancelAsyncBtn.Enabled = false;
        }
        private void UnlockUIOnSuccessfullFormalConceptsComputation()
        {
            this.formalConceptsCount.Text = this.FormalConcepts.Count.ToString();
            this.formalContextGBox.Enabled = true;
            this.conceptLatticeGBox.Enabled = true;
            this.ConceptLattice = null;
            UpdateConceptLatticeGroupBox();
            this.exportLatticeNeo4jGBox.Enabled = false;
            ResetExportConceptLatticeNeo4jGroupBox();

            this.formalConceptBtn.Enabled = true;
            this.cancelAsyncBtn.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            algorithmTime.Text = algorithmStopWatch.Elapsed.ToString(@"hh\:mm\:ss\.f");
        }

        #endregion

        #region Concept lattice

        private void computeLatticeBtn_Click(object sender, EventArgs e)
        {
            LockUIOnLatticeCompute();

            this.ConceptLattice = new ConceptLattice(this.FormalConcepts, this.Context);
            
            UnlockUIOnLatticeCompute();
        }
        private void LockUIOnLatticeCompute()
        {
            this.formalContextGBox.Enabled = false;
            this.conceptsAlgorithmGBox.Enabled = false;
            this.exportLatticeNeo4jGBox.Enabled = false;
            ResetExportConceptLatticeNeo4jGroupBox();
        }
        private void UnlockUIOnLatticeCompute()
        {
            UpdateConceptLatticeGroupBox();
            this.formalContextGBox.Enabled = true;
            this.conceptsAlgorithmGBox.Enabled = true;
            this.exportLatticeNeo4jGBox.Enabled = true;
            ResetExportConceptLatticeNeo4jGroupBox();
        }

        #endregion

        #region Export concept lattice Neo4j

        private void ConnectNeo4JProvider()
        {
            var connectionString = neo4jConnectionString.Text;
            var username = neo4jUsername.Text;
            var password = neo4jPassword.Text;
            try
            {
                Neo4JProvider = new Neo4jDataProvider(connectionString, username, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to connect Neo4J database.");
                return;
            }
        }

        private void exportLatticeNeo4jBtn_Click(object sender, EventArgs e)
        {
            LockUIOnNeo4jLatticeExport();
            if(Neo4JProvider == null)
                ConnectNeo4JProvider();
            try
            {
                Neo4JProvider.ClearDatabase();
                Neo4JProvider.ImportFCALatticeLikeCSV(this.ConceptLattice);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to export concept lattice to Neo4J databse.");
                UnlockUIOnNeo4jLatticeExport();
                return;
            }
            MessageBox.Show("Concept lattice exported to Neo4J database.");

            UnlockUIOnNeo4jLatticeExport();
        }
        private void LockUIOnNeo4jLatticeExport()
        {
            this.neo4jConnectionString.Enabled = false;
            this.neo4jUsername.Enabled = false;
            this.neo4jPassword.Enabled = false;
            this.formalContextGBox.Enabled = false;
            this.exportContextFileGBox.Enabled = false;
            this.conceptsAlgorithmGBox.Enabled = false;
            this.conceptLatticeGBox.Enabled = false;
            this.exportLatticeFileGBox.Enabled = false;
        }
        private void UnlockUIOnNeo4jLatticeExport()
        {
            this.formalContextGBox.Enabled = true;
            this.exportContextFileGBox.Enabled = false;
            this.conceptsAlgorithmGBox.Enabled = true;
            this.conceptLatticeGBox.Enabled = true;
            this.neo4jConnectionString.Enabled = true;
            this.neo4jUsername.Enabled = true;
            this.neo4jPassword.Enabled = true;
            this.searchNeo4JLatticeBtn.Enabled = true;
            this.exportLatticeFileGBox.Enabled = false;
        }

        private void searchNeo4JLatticeBtn_Click(object sender, EventArgs e)
        {
            SearchNeo4jLattice methodForm = new SearchNeo4jLattice(this.ConceptLattice, this.Neo4JProvider);
            methodForm.Owner = this;
            DialogResult result = methodForm.ShowDialog();
            if (result == DialogResult.Cancel || result == DialogResult.OK)
            {

            }
        }

        #endregion

        #region Export concept lattice file (not implemented)

        #endregion

        #region UI helpers

        private void InitializeForm()
        {

            this.Context = null;
            this.ContextImported = false;
            this.FormalContextImportFilePath = String.Empty;

            this.importFileContextGBox.Enabled = true;
            ResetContextFileImportGroupBox();

            this.formalContextGBox.Enabled = false;
            ResetFormalContextGroupBox();

            this.exportContextFileGBox.Enabled = false;
            ResetExportContextFileGroupBox();

            this.FormalConcepts = null;
            this.conceptsAlgorithmGBox.Enabled = false;
            ResetConceptsAlgorithmGroupBox();

            this.conceptLatticeGBox.Enabled = false;
            this.ConceptLattice = null;
            ResetConceptLatticeGroupBox();

            this.exportLatticeNeo4jGBox.Enabled = false;
            ResetExportConceptLatticeNeo4jGroupBox();

            this.exportLatticeFileGBox.Enabled = false;
            ResetExportLatticeFileGroupBox();
        }

        private void ResetContextFileImportGroupBox()
        {
            this.inputFileTxt.Text = String.Empty;
            this.preprocessimportFileAttributesCBox.Checked = false;
            // Context file importers
            this.importFileFormatCbox.DisplayMember = "Description";
            this.importFileFormatCbox.ValueMember = "Id";
            this.importFileFormatCbox.Items.Clear();
            foreach (ContextFileImporters importer in Enum.GetValues(typeof(ContextFileImporters)))
            {
                var tmp_importer = ContextFileImporterFactory.ProduceImporter(importer);
                this.importFileFormatCbox.Items.Add(new { Id = (int)importer, Description = tmp_importer.Description });
            }
            this.importFileFormatCbox.SelectedIndex = 1;
            this.ContextFileImporter = ContextFileImporterFactory.ProduceImporter(ContextFileImporters.Json_id_tags_Importer);
            this.preprocessimportFileAttributesCBox.Checked = false;
        }

        private void ResetFormalContextGroupBox()
        {
            this.context_name.Text = "-";
            this.context_objectsCount.Text = "-";
            this.context_attributesCount.Text = "-";
            this.context_preprocessed.Text = "-";
            this.clearBtn.Visible = false;
        }
        private void UpdateFormalContextGroupBox()
        {
            this.formalContextGBox.Enabled = true;
            this.context_name.Text = Path.GetFileNameWithoutExtension(FormalContextImportFilePath);
            this.context_objectsCount.Text = Context.ObjectsArray.Count().ToString();
            this.context_attributesCount.Text = Context.AttributesArray.Count().ToString();
            this.context_preprocessed.Text = (this.preprocessimportFileAttributesCBox.Checked) ? "YES" : "NO";
            this.clearBtn.Visible = true;
        }

        private void ResetExportContextFileGroupBox()
        {
            this.outputFolderTxt.Text = String.Empty;
            this.outputFileNameTxt.Text = String.Empty;
        }

        private void ResetConceptsAlgorithmGroupBox()
        {
            // Context file importers
            this.alogrithmCBox.DisplayMember = "Name";
            this.alogrithmCBox.ValueMember = "Id";
            this.alogrithmCBox.Items.Clear();
            foreach (FormalConceptAlgorithms algorithm in Enum.GetValues(typeof(FormalConceptAlgorithms)))
            {
                var objects = new List<FCAA.Data.Object>();
                var attributes = new List<FCAA.Data.Attribute>();
                var objectAttributes = new Dictionary<FCAA.Data.Object, HashSet<FCAA.Data.Attribute>>();
                var attributeObjects = new Dictionary<FCAA.Data.Attribute, HashSet<FCAA.Data.Object>>();
                var tmp_context = new FormalContext(objects, attributes, objectAttributes, attributeObjects);
                var tmp_algorithm = FormalConceptAlgorithmFactory.ProduceAlgorithm(algorithm, tmp_context);
                this.alogrithmCBox.Items.Add(new { Id = (int)algorithm, Name = tmp_algorithm.Name });
            }
            this.alogrithmCBox.SelectedIndex = 0;
            this.algorithmTime.Text = "-";
            this.formalConceptsCount.Text = "-";
            this.formalConceptBtn.Enabled = true;
            this.cancelAsyncBtn.Enabled = false;
        }

        private void ResetConceptLatticeGroupBox()
        {
            this.lattice_computed.Text = "-";
            this.lattice_hight.Text = "-";
        }
        private void UpdateConceptLatticeGroupBox()
        {
            if (ConceptLattice != null)
            {
                this.lattice_computed.Text = "YES";
                this.lattice_hight.Text = ConceptLattice.Height.ToString();
            }
            else
            {
                this.lattice_computed.Text = "NO";
                this.lattice_hight.Text = "-";
            }
        }

        private void ResetExportConceptLatticeNeo4jGroupBox()
        {
            if (!Neo4jDefaultInitialized)
            {
                this.neo4jConnectionString.Text = DefaultNeo4jConnectionString;
                this.neo4jUsername.Text = DefaultNeo4jUsername;
                this.neo4jPassword.Text = DefaultNeo4jPassword;
                Neo4jDefaultInitialized = true;
            }
            this.searchNeo4JLatticeBtn.Enabled = false;
        }

        private void ResetExportLatticeFileGroupBox()
        {
            this.exportLatticeFolderPath.Text = String.Empty;
            this.exportLatticeFileName.Text = String.Empty;
        }

        #endregion

        /*private void sparqlbutton_Click(object sender, EventArgs e)
        {
            //Set the endpoint
            QueryClient queryClient = new QueryClient("http://dbpedia.org/sparql");

            var query = "SELECT * WHERE {<http://dbpedia.org/resource/Stealing_Beauty> <http://purl.org/dc/terms/subject> ?categories}";
            Table table = queryClient.Query(query);
        }*/
    }
}
