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

        public void BFS(List<int> BL, int v, int target, int node)
        {

            // Mark all the vertices as not
            // visited(By default set as false) 
            List<int> visited = new List<int>();

            // Create a queue for BFS 
            Queue<int> queue = new Queue<int>();

            // Mark the current node as 
            // visited and enqueue it 
            visited.Add(v);
            if (!BL.Contains(v))
            {
                BL.Add(v);
            }
            queue.Enqueue(v);

            while (queue.Any())
            {
                // Dequeue a vertex from queue 
                // and print it
                int s = queue.Dequeue();
                // Console.Write(s + " ");

                if (!BL.Contains(s))
                {
                    BL.Add(s);
                }
                //BL.Add(s);

                // Get all adjacent vertices of the 
                // dequeued vertex s. If a adjacent
                // has not been visited, then mark it 
                // visited and enqueue it 
                List<int> list = adj[s];

                for (int i = 0; i < node; i++)
                {
                    if (!visited.Contains(i) && list.Contains(i))
                    {
                        visited.Add(i);
                        queue.Enqueue(i);
                    }
                }
                
                if (s == target)
                {
                    break;
                }
            }
        }
        // A function used by DFS
        public void DFSUtil(List<int> AL, int v, List<int> visited, 
            int target, List<List<int>> road_used, int parent, int it, int node, bool hapus)
        {
            // Check if all th node is visited or not
            // and count unvisited nodes
            int c = visited.Distinct().Count();

            // If all the node is visited return;
            if (c == node || AL.Contains(target))
            {
                return;
            }
            if (AL.Count==0 && hapus == true )
            {
                return;
            }

            // Mark the current node as visited
            // and print it
            if (!visited.Contains(v))
            {
                visited.Add(v);
            }
            road_used.Add(new List<int>() { parent, v });
            //AL.Add(v);
            if (!AL.Contains(v) && hapus==false)
            {
                AL.Add(v);
            }

            //Console.Write(v + " ");

            // Recur for all the vertices
            // adjacent to this vertex
            List<int> vList = adj.ElementAt(v);
            /*foreach (var n in vList)
            {
                if (!visited[n])
                    AL = DFSUtil(AL, n, visited);
            }*/
            for (int x = 0; x < node; x++)
            {
                // call the DFs function if not visited
                if (!visited.Contains(x) && vList.Contains(x))
                {
                    hapus = false;
                    DFSUtil(AL, x, visited, target, road_used, v, it + 1, node, hapus);
                }
            }
            if (c == node || AL.Contains(target))
            {
                return;
            }
            // Backtrack through the last
            // visited nodes
            for (int y = 0; y < road_used.Count; y++)
            {
                if (road_used[y][1] == v)
                {
                    hapus = true;
                    if (AL.Contains(v) && hapus == true)
                    {
                        AL.Remove(v);
                    }
                    DFSUtil(AL, road_used[y][0], visited, target, road_used, v, it + 1, node, hapus);
                }
            }
        }

        // The function to do DFS traversal.
        // It uses recursive DFSUtil()
        public void DFS(List<int> AL, int v, int target, int node)
        {
            // Mark all the vertices as not visited
            // (set as false by default in c#)
            List<int> visited = new List<int>();

            List<List<int>> road_used = new List<List<int>>();
            bool hapus = false;
            // Call the recursive helper function
            // to print DFS traversal
            DFSUtil(AL, v, visited, target, road_used, -1, 0, node, hapus);
            //AL.Add(target);
        }

        public string TranslatetoString(Dictionary<int,string> kamus, int x)
        {
            return kamus.ElementAt(x).Value;
        }
        public int TranslatetoInt(Dictionary<int, string> kamus, string y)
        {
            return kamus.FirstOrDefault(x => x.Value == y).Key;
        }
        public List<List<int>> RecommendDFS(int awal)
        {
            List<List<int>> Bucket = new List<List<int>>();
            for(int i = 0; i < V; i++)
            {
                Bucket.Add(new List<int>());
                Bucket.ElementAt(Bucket.Count - 1).Add(i);
                if (i != awal && !adj[i].Contains(awal))
                {
                    //Struktur:
                    //Huruf Rekomendasi: Mutual friend 1 - Mutual friend 2 - dll
                    for(int j = 0; j < V; j++)
                    {
                        if (j != awal && j != i)
                        {
                            if(adj[j].Contains(i) && adj[j].Contains(awal))
                            {
                                //Tambahkan friend recommendation
                                Bucket.ElementAt(Bucket.Count-1).Add(j);
                            }                           
                            
                        }
                    }
                }
                if (Bucket.ElementAt(Bucket.Count - 1).Count == 1)
                {
                    //Buang bracket di Count-1
                    Bucket.Remove(Bucket.ElementAt(Bucket.Count - 1));
                }
            }
            Bucket.OrderBy(lst => lst.Count());
            return Bucket;
        }
        public List<List<int>> RecommendBFS(int awal)
        {
            List<List<int>> Bucket = new List<List<int>>();
            for (int i = 0; i < V; i++)
            {
                if (i != awal && !adj[i].Contains(awal))
                {
                    Bucket.Add(new List<int>());
                    Bucket.ElementAt(Bucket.Count - 1).Add(i);
                    //Struktur:
                    //Huruf Rekomendasi: Mutual friend 1 - Mutual friend 2 - dll
                    BFS(Bucket[i], awal, i, V);
                }
                if (Bucket.ElementAt(Bucket.Count - 1).Count == 1)
                {
                    //Buang bracket di Count-1
                    Bucket.Remove(Bucket.ElementAt(Bucket.Count - 1));
                }
            }
            Bucket.OrderBy(lst => lst.Count());
            return Bucket;
        }

        public List<int> ExploreDFS (int awal, int akhir)
        {
            List<int> bracket = new List<int>();
            DFS(bracket, awal, akhir, V);
            return bracket;
        }
        public void InputGraph(List<List<string>> DataNode, Dictionary<int,string> Kamus)
        {
            //Console.Write(Kamus.ElementAt(1).Key);
            
            foreach(var line in DataNode)
            {
                AddEdge(Kamus.FirstOrDefault(x => x.Value == line[0]).Key, Kamus.FirstOrDefault(x => x.Value == line[1]).Key);
            }

            for (int i=0; i < V; i++)
            {
                adj[i].Sort();
            }

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
            /*Console.WriteLine(
                "Following is Depth First Traversal "
                + "(starting from vertex 2)");
            */
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
            List<int> BL = new List<int>();
            List<List<int>> RecomDFS = new List<List<int>>();
            List<List<int>> RecomBFS = new List<List<int>>();
            List<int> ExploreDFS = new List<int>();
            List<int> ExploreBFS = new List<int>();
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
            g.Output();
            g.InputGraph(input.DataNode, input.Kamus);
            /*g.DFS(AL, input.Kamus.FirstOrDefault(x => x.Value == "A").Key, 
                input.Kamus.FirstOrDefault(x => x.Value == "H").Key, input.Node);
            */g.BFS(BL, input.Kamus.FirstOrDefault(x => x.Value == "A").Key,
                input.Kamus.FirstOrDefault(x => x.Value == "H").Key, input.Node);
            RecomDFS = g.RecommendDFS(8);
            RecomBFS = g.RecommendDFS(8);
            ExploreDFS = g.ExploreDFS(8,9);
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
                Console.Write(input.Kamus.ElementAt(i).Value + ": ");
                foreach (var num in g.adj[i])
                {
                    Console.Write(input.Kamus.ElementAt(num).Value);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }

            Console.Write("AL : ");           
            for(int i = 0; i < AL.Count; i++)
            {
                Console.Write(input.Kamus.ElementAt(AL[i]).Value);
                //Console.Write(AL[i]);
                Console.Write(" ");
            }
            Console.WriteLine();

            Console.Write("BL : ");
            for (int i = 0; i < BL.Count; i++)
            {
                Console.Write(input.Kamus.ElementAt(BL[i]).Value);
                //Console.Write(BL[i]);
                Console.Write(" ");
            }

            Console.WriteLine();

            Console.WriteLine("Friend Recommendation DFS from " + g.TranslatetoString(input.Kamus, 7));
            for (int x=0; x<RecomDFS.Count; x++)
            {
                for (int y = 0; y<RecomDFS[x].Count; y++)
                {
                    Console.Write(g.TranslatetoString(input.Kamus, RecomDFS[x][y]));
                    if (y == 0)
                    {
                        Console.Write(":\n");
                        Console.Write(RecomDFS[x].Count-1 + " Mutual friends:");
                    }
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
            Console.WriteLine();

            Console.WriteLine("Friend Recommendation BFS from " + g.TranslatetoString(input.Kamus, 0));
            for (int a = 0; a < RecomBFS.Count; a++)
            {
                for (int b = 0; b < RecomBFS[a].Count; b++)
                {
                    Console.Write(g.TranslatetoString(input.Kamus, RecomBFS[a][b]));
                    if (b == 0)
                    {
                        Console.Write(":\n");
                        Console.Write(RecomBFS[a].Count - 1 + " Mutual friends:");
                    }
                    Console.Write(" ");
                }
                Console.Write("\n");
            }

            Console.WriteLine();

            Console.WriteLine("Eksplore BFS from " + g.TranslatetoString(input.Kamus, 0) + " to " + g.TranslatetoString(input.Kamus, 7));
            Console.WriteLine("Banyak Koneksi derajat: " + (ExploreDFS.Count-2));
            if (ExploreDFS.Count > 2)
            {
                for (int x = 1; x < ExploreDFS.Count - 1; x++)
                {
                    Console.Write(g.TranslatetoString(input.Kamus, ExploreDFS[x]));
                    Console.Write(" ");
                }
            }
            else if (ExploreDFS.Count == 2)
            {
                Console.Write("Sudah berteman, cari yang lain dong!!");
            }
            else
            {
                Console.Write("Tidak ada jalur koneksi yang tersedia! \n Anda harus memulai koneksi baru itu sendiri. ");
            }
            
            Console.ReadKey();
        }
    }
}
