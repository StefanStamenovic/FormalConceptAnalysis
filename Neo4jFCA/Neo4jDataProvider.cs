using Neo4jClient;
using Neo4jClient.Cypher;
using NextClosureAlgorithm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jFCA
{
    public class Neo4jDataProvider
    {
        private GraphClient client;
        private Uri db_adres = new Uri("http://localhost:7474/db/data");
        private static string user_name = "neo4j";
        private static string password = "root";

        public Neo4jDataProvider()
        {
            client = new GraphClient(db_adres, user_name, password);
            try
            {
                client.Connect();
            }
            catch (Exception e)
            {
                throw new Exception("Database failed to connect with folowing message : " + e.Message);
            }
        }

        public void CreateNode()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("name", "test");

            CypherQuery query = new CypherQuery("CREATE (node: Node{ name: {name}})",
                       dictionary, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteCypher(query);
        }

        public void ClearDatabase()
        {
            CypherQuery query = new CypherQuery("MATCH ()-[r]-() DELETE r", null, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteCypher(query);

            CypherQuery query2 = new CypherQuery("MATCH (n) DELETE n", null, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteCypher(query2);
        }

        public void ImportFCALattice(ConceptLattice lattice)
        {
            var latticeNodes = lattice.LatticeFormalConcepts;
            foreach(var node in latticeNodes)
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("id", node.Id);
                var attributesAggregate = "";
                if(node.Attributes.Any())
                    node.Attributes.Select(a => a.Name).Aggregate((i, j) => i + "," + j);
                dictionary.Add("attributes", attributesAggregate);
                var objectsAggregate = "";
                if (node.Objects.Any())
                   objectsAggregate = node.Objects.Select(o => o.Name).Aggregate((i, j) => i + "," + j);
                dictionary.Add("objects", objectsAggregate);

                CypherQuery query = new CypherQuery("CREATE (node: Node{ id: {id}, attributes: {attributes}, objects: {objects}})",
                           dictionary, CypherResultMode.Set);

                ((IRawGraphClient)client).ExecuteCypher(query);
            }

            foreach(var node in latticeNodes)
            {
                foreach(var subnode in node.Subconcepts)
                {
                    var dictionary = new Dictionary<string, object>();
                    dictionary.Add("node_id", node.Id);
                    dictionary.Add("subnode_id", subnode.Id);
                    var query = new CypherQuery("MATCH (node:Node{id: {node_id}}),(subnode:Node{id: {subnode_id}}) CREATE (node)-[relation:rel]->(subnode)",
                               dictionary, CypherResultMode.Set);
                    ((IRawGraphClient)client).ExecuteCypher(query);
                }
            }
        }
    }
}
