using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkLizardman
{
    class Input
    {
        public Dictionary<int, string> Kamus;
        public List<List<string>> DataNode;
        public int Node;
        public Input(string filename)
        {
            DataNode = FileInput(filename);
            Kamus = KamusData(DataNode);
            Node = Kamus.Count;
        }
        private List<List<string>> FileInput(string filename)
        {
            List<List<string>> bracket = new List<List<string>>();
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(filename);

            // Display the file contents by using a foreach loop.
            Console.WriteLine("Contents of WriteLines2.txt = ");
            int i = 0;
            foreach (string line in lines)
            {
                if (i == 0)
                {
                    i++;
                }
                else
                {
                    List<string> line2 = line.Split(' ').ToList();
                    bracket.Add(line2);
                    i++;
                }
                // Use a tab to indent each line of the file.
            }
            foreach (var line in bracket)
            {
                Console.Write(line[0]);
                Console.Write(line[1]);
                Console.WriteLine();
            }
            return bracket;
        }

        private Dictionary<int, string> KamusData(List<List<string>> bracket)
        {
            List<string> bahanbaku = new List<string>();
            foreach (var line in bracket)
            {
                if (!bahanbaku.Contains(line[0]))
                {
                    //Cek apakah elemen ke 0 dari tiap line sudah ada di bahanbaku
                    bahanbaku.Add(line[0]);
                }
                if (!bahanbaku.Contains(line[1]))
                {
                    //Cek apakah elemen ke 1 dari tiap line sudah ada di bahanbaku
                    bahanbaku.Add(line[1]);
                }
            }
            //Himpunan bahanbaku sudah siap
            //Masukkan jumlah node ke variabel static global node
            bahanbaku.Sort();

            /*Tahap Pembuatan Dictionary*/
            Dictionary<int, string> kamus = new Dictionary<int, string>();
            int i = 0;
            foreach (string elemen in bahanbaku)
            {
                kamus.Add(i, elemen);
                i++;
            }
            return kamus;
        }
    }
    class Graph
    {
        //create a form 
        System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        //create a viewer object 
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        //create a graph object 
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        private int V; // No. of vertices

        // Array of lists for
        // Adjacency List Representation
        public List<int>[] adj;
        private static int node = 0;

        // Constructor
        public Graph(int v)
        {
            V = v;
            adj = new List<int>[v];
            for (int i = 0; i < v; ++i)
                adj[i] = new List<int>();
        }

        // Function to Add an edge into the graph
        void AddEdge(int v, int w)
        {
            //JANGAN LUPA
            adj[v].Add(w); // Add w to v's list.
            adj[w].Add(v);
            /*bool found = false;
            foreach (Microsoft.Msagl.Drawing.Edge e in graph.Edges)
            {
                if (!found)
                {
                    if ((e.SourceNode == graph.FindNode(v.ToString()) && e.SourceNode == graph.FindNode(w.ToString())) ||
                    (e.SourceNode == graph.FindNode(w.ToString()) && e.SourceNode == graph.FindNode(v.ToString())))
                    {
                        found = true;
                    }
                }
            }
            if (found == false)
            {
                var edge = graph.AddEdge(v.ToString(), w.ToString());
                edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }*/
        }

        void AddEdgeSisa(int v, int w)
        {
            bool found = false;
            foreach (Microsoft.Msagl.Drawing.Edge e in graph.Edges)
            {
                if (!found)
                {
                    if ((e.SourceNode == graph.FindNode(v.ToString()) && e.SourceNode == graph.FindNode(w.ToString())) ||
                    (e.SourceNode == graph.FindNode(w.ToString()) && e.SourceNode == graph.FindNode(v.ToString())))
                    {
                        found = true;
                    }
                }
            }
            if (found == false)
            {
                var edge = graph.AddEdge(v.ToString(), w.ToString());
                edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }
        }

        void AddEdgeDFS(int v, int w)
        {
            bool found = false;
            /*
            foreach (Microsoft.Msagl.Drawing.Edge e in graph.Edges)
            {
                if (!found)
                {
                    if ((e.SourceNode == graph.FindNode(v.ToString()) && e.SourceNode == graph.FindNode(w.ToString())) ||
                    (e.SourceNode == graph.FindNode(w.ToString()) && e.SourceNode == graph.FindNode(v.ToString())))
                    {
                        found = true;
                    }
                }
            }
            */
            if(found == false)
            {
                    var edge2 = graph.AddEdge(v.ToString(), w.ToString());
                    edge2.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    edge2.Attr.LineWidth = 3;
                    edge2.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    edge2.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    graph.FindNode(v.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                    graph.FindNode(w.ToString()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
            }
        }

        // A function used by DFS
        public List<int> DFSUtil(List<int> AL, int v, List<int> visited, 
            int target, List<List<int>> road_used, int parent, int it, int node)
        {

            // Check if all th node is visited or not
            // and count unvisited nodes
            int c = visited.Count;

            // If all the node is visited return;
            if (c == node)
                return AL;

            // Mark the current node as visited
            // and print it
            visited.Add(v);
            if (!AL.Contains(v))
            {
                AL.Add(v);
            }
            //Console.Write(v + " ");
            road_used.Add(new List<int>() { parent, v });

            // Recur for all the vertices
            // adjacent to this vertex
            List<int> vList = adj[v];
            /*foreach (var n in vList)
            {
                if (!visited[n])
                    AL = DFSUtil(AL, n, visited);
            }*/
            if (AL.Contains(target))
            {
                return AL;
            }
            if (vList.Count > 0)
            {
                bool found = false;
                int i = 0;
                while (!found && i < vList.Count)
                {
                    if (!visited.Contains(vList.OrderBy(z => z).Skip(i).First()))
                    {
                        found = true;
                        AL = DFSUtil(AL, vList.OrderBy(z => z).Skip(i).First(), visited, target, road_used, v, it+1, node);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            else
            {
                for (int y = 0; y < road_used.Count; y++)
                {
                    if (road_used[y][1] == v)
                    {
                        AL.Remove(v);
                        DFSUtil(AL, road_used[y][0], visited, target, road_used, v, it + 1, node);
                    }
                }
            }
            return AL;
        }

        // The function to do DFS traversal.
        // It uses recursive DFSUtil()
        public List<int> DFS(List<int> AL, int v, int target, int node)
        {
            // Mark all the vertices as not visited
            // (set as false by default in c#)
            List<int> visited = new List<int>();

            List<List<int>> road_used = new List<List<int>>();

            // Call the recursive helper function
            // to print DFS traversal
            AL = DFSUtil(AL, v, visited, target, road_used, -1, 0, node);
            return AL;
        }
        public void InputGraph(List<List<string>> DataNode, Dictionary<int,string> Kamus)
        {
            //Console.Write(Kamus.ElementAt(1).Key);
            
            foreach(var line in DataNode)
            {
                AddEdge(Kamus.FirstOrDefault(x => x.Value == line[0]).Key, Kamus.FirstOrDefault(x => x.Value == line[1]).Key);
            }
            
        }
        public void InputSisa()
        {
            AddEdge(1, 2);
            AddEdge(1, 3);
            AddEdge(1, 4);
            AddEdge(2, 3);
            AddEdge(2, 5);
            AddEdge(2, 6);
            AddEdge(3, 6);
            AddEdge(3, 7);
            AddEdge(4, 6);
            AddEdge(6, 5);
            AddEdge(5, 8);
            AddEdge(2, 6);
            AddEdge(6, 8);
        }
        // Driver Code
        public void Output()
        {
            /*
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
            */
            Console.WriteLine(
                "Following is Depth First Traversal "
                + "(starting from vertex 2)");
        }
        
    }
    class MainProgram
    {
        public static void Main(String[] args)
        {
            string filename = @"..\..\graph.txt";
            Input input = new Input(filename);
            //create the graph content 
            Graph g = new Graph(input.Node);
            List<int> AL = new List<int>();
            /*foreach (var key in input.Kamus)
            {
                Console.Write(key.Key);
                Console.Write("\t" + key.Value);
                Console.WriteLine();
            }
            foreach (var line in input.DataNode)
            {
                Console.Write(line[0]);
                Console.Write("  -->  " + line[1]);
                Console.WriteLine();
            }
            */
            //g.Output();
            g.InputGraph(input.DataNode, input.Kamus);
            AL = g.DFS(AL, input.Kamus.FirstOrDefault(x => x.Value == "A").Key, 
                input.Kamus.FirstOrDefault(x => x.Value == "D").Key, input.Node);
            Console.Write("\n");
            Console.WriteLine(AL.Count);
            /*
            for (int i = 0; i < AL.Count - 1; i++)
            {
                g.AddEdgeDFS(AL[i], AL[i + 1]);

            }
            */
            for (int i = 0; i < g.adj.Count(); i++)
            {
                Console.Write(i + ": ");
                foreach (var num in g.adj[i])
                {
                    Console.Write(num);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
            //g.InputSisa();
            Console.Write("AL : ");
            for(int i = 0; i < AL.Count; i++)
            {
                Console.Write(AL[i]);
                Console.Write(" ");
            }
            
            Console.ReadKey();
        }
    }
}
