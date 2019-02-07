using FCAA.Data.Lattice;
using Neo4jFCA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCA
{
    public partial class SearchNeo4jLattice : Form
    {
        public ConceptLattice Lattice { get; }
        public Neo4jDataProvider Neo4JProvider { get; }

        public SearchNeo4jLattice(ConceptLattice lattice, Neo4jDataProvider neo4JProvider)
        {
            InitializeComponent();
            Lattice = lattice;
            Neo4JProvider = neo4JProvider;

            // Auto suggestion
            var source = new AutoCompleteStringCollection();
            foreach (var attribute in Lattice.FormalContext.AttributesArray)
                source.Add(attribute.Name);
            searchTBox.AutoCompleteCustomSource = source;
            searchTBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            searchTBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            var res = Neo4JProvider.SearchForObjects(searchTBox.Text);
            if (res == null)
            {
                resultRTBox.Text = "No results!";
            }
            else
            {
                resultRTBox.Text = res.Replace(" ", "\n\n");
            }
        }
    }
}
