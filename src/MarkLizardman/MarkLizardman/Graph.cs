using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkLizardman{
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
            if (found == false)
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

        public Dictionary<int, int> BFS(List<int> BL, int v, int target , int node)
        {
            Dictionary<int, int> prev = new Dictionary<int, int>();
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
                List<int> list = adj.ElementAt(s);

                foreach (var n in list)
                {
                    if (!visited.Contains(n) && list.Contains(n))
                    {
                        prev.Add(n, s);
                        visited.Add(n);
                        queue.Enqueue(n);
                    }
                }

                if (s == target)
                {
                    break;
                }
            }
            return prev;
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
            if (AL.Count == 0 && hapus == true)
            {
                Console.WriteLine("Keluar dari fungsi");
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
            if (!AL.Contains(v) && hapus == false)
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

        public string TranslatetoString(Dictionary<int, string> kamus, int x)
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
            for (int i = 0; i < V; i++)
            {
                Bucket.Add(new List<int>());
                Bucket.ElementAt(Bucket.Count - 1).Add(i);
                if (i != awal && !adj[i].Contains(awal))
                {
                    //Struktur:
                    //Huruf Rekomendasi: Mutual friend 1 - Mutual friend 2 - dll
                    for (int j = 0; j < V; j++)
                    {
                        if (j != awal && j != i)
                        {
                            if (adj[j].Contains(i) && adj[j].Contains(awal))
                            {
                                //Tambahkan friend recommendation
                                Bucket.ElementAt(Bucket.Count - 1).Add(j);
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
            List<List<int>> NewBucket = Bucket.OrderByDescending(x => x.Count).ThenBy(x => x[1]).ToList();
            return NewBucket;
        }
        public List<List<int>> RecommendBFS(int awal)
        {
            //Komparasi 2 array hasil dfs first degree
            //
            List<List<int>> Bucket = new List<List<int>>();
            
            Dictionary<int, int> previous = new Dictionary<int, int>();
            Dictionary<int, int> previous2 = new Dictionary<int, int>();
            for (int i = 0; i < V; i++)
            {
                if (i != awal && !adj[i].Contains(awal))
                {
                    List<int> arr1 = new List<int>();
                    List<int> arr2 = new List<int>();
                    Bucket.Add(new List<int>());
                    Bucket.ElementAt(Bucket.Count - 1).Add(i);
                    //Struktur:
                    //Huruf Rekomendasi: Mutual friend 1 - Mutual friend 2 - dll
                    previous = BFS(arr1, awal, i, V);
                    previous2 = BFS(arr2, i, awal, V);
                    List<int> List1 = previous.Where(x => x.Value == awal)
                      .Select(x => x.Key)
                      .ToList();
                    List<int> List2 = previous2.Where(x => x.Value == i)
                      .Select(x => x.Key)
                      .ToList();
                    IEnumerable<int> res = List1.AsQueryable().Intersect(List2);
                    foreach (var x in res)
                    {
                        if (x != awal && x != i)
                        {
                            Bucket.ElementAt(Bucket.Count - 1).Add(x);
                        }
                    }
                    if (Bucket.ElementAt(Bucket.Count - 1).Count == 1)
                    {
                        //Buang bracket di Count-1
                        Bucket.Remove(Bucket.ElementAt(Bucket.Count - 1));
                    }
                    List1.Clear();
                    List2.Clear();
                }
            }
            List<List<int>> NewBucket = Bucket.OrderByDescending(x => x.Count).ThenBy(x => x[1]).ToList();
            return NewBucket;
        }

        public List<int> ExploreDFS(int awal, int akhir)
        {
            List<int> bracket = new List<int>();
            DFS(bracket, awal, akhir, V);
            return bracket;
        }

        public List<int> ExploreBFS(int awal, int akhir)
        {
            Dictionary<int, int> prev = new Dictionary<int, int>();
            List<int> bracket = new List<int>();
            List<int> bracket2 = new List<int>();
            prev = BFS(bracket, awal, akhir, V);
            int dari = bracket[bracket.Count - 1];
            while (dari != awal)
            {
                bracket2.Add(dari);
                dari = prev.FirstOrDefault(x => x.Key == dari).Value;
            }
            bracket2.Add(awal);
            bracket2.Reverse();
            return bracket2;
        }

        public void InputGraph(List<List<string>> DataNode, Dictionary<int, string> Kamus)
        {
            //Console.Write(Kamus.ElementAt(1).Key);

            foreach (var line in DataNode)
            {
                AddEdge(Kamus.FirstOrDefault(x => x.Value == line[0]).Key, Kamus.FirstOrDefault(x => x.Value == line[1]).Key);
            }

            for (int i = 0; i < V; i++)
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
}