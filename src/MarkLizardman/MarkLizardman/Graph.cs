using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkLizardman
{
    class Graph
    {
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

        public void ColorEdge(int v, int w, Dictionary<int, String> Kamus)
        {
            Microsoft.Msagl.Drawing.Node nv = graph.FindNode(Kamus[v]);
            Microsoft.Msagl.Drawing.Node nw = graph.FindNode(Kamus[w]);
            if (nv == null || nw == null) return;
            foreach (Microsoft.Msagl.Drawing.Edge e in nv.Edges)
            {
                if ((e.SourceNode == nv && e.TargetNode == nw) ||
                   (e.SourceNode == nw && e.TargetNode == nv))
                {
                    nv.Attr.Color = Microsoft.Msagl.Drawing.Color.Orange;
                    nw.Attr.Color = Microsoft.Msagl.Drawing.Color.Orange;
                    e.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    break;
                }
            }
        }

        public void ColorNode(int v, Dictionary<int, String> Kamus, String Warna)
        {
            Microsoft.Msagl.Drawing.Node nv = graph.FindNode(Kamus[v]);
            if (nv == null) return;
            if (Warna == "Orange")
            {
                nv.Attr.Color = Microsoft.Msagl.Drawing.Color.Orange;
            }
            else if (Warna == "Green")
            {
                nv.Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            }
        }

        public Microsoft.Msagl.Drawing.Graph GetGraph()
        {
            return this.graph;
        }

        // Function to Add an edge into the graph
        void AddEdge(int v, int w, Dictionary<int, String> Kamus)
        {
            //JANGAN LUPA
            adj[v].Add(w); // Add w to v's list.
            adj[w].Add(v);
            Microsoft.Msagl.Drawing.Edge e = graph.AddEdge(Kamus[v], Kamus[w]);
            e.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
            e.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
        }

        public Dictionary<int, int> BFS(List<int> BL, int v, int target, int node)
        {
            bool ketemu = false;
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
                if (queue.Peek() == target)
                {
                    ketemu = true;
                    break;
                }
                // Dequeue a vertex from queue 
                // and print it
                int s = queue.Dequeue();

                if (!BL.Contains(s))
                {
                    BL.Add(s);
                }

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
            }
            if (!BL.Contains(target) && ketemu)
            {
                BL.Add(target);
            }
            else
            {
                BL.Clear();
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
                return;
            }

            // Mark the current node as visited
            // and print it
            if (!visited.Contains(v))
            {
                visited.Add(v);
            }
            road_used.Add(new List<int>() { parent, v });
            if (!AL.Contains(v) && hapus == false)
            {
                AL.Add(v);
            }

            // Recur for all the vertices
            // adjacent to this vertex
            List<int> vList = adj.ElementAt(v);
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
        public Dictionary<int, List<int>> DFS(List<int> AL, int v, int target, int node)
        {
            // Mark all the vertices as not visited
            // (set as false by default in c#)
            Dictionary<int, List<int>> child = new Dictionary<int, List<int>>();
            List<int> visited = new List<int>();

            List<List<int>> road_used = new List<List<int>>();
            bool hapus = false;
            // Call the recursive helper function
            // to print DFS traversal
            DFSUtil(AL, v, visited, target, road_used, -1, 0, node, hapus);
            //AL.Add(target);
            foreach (var value in AL)
            {
                List<int> masuk = new List<int> { value };
                foreach (var x in adj[value])
                {
                    masuk.Add(x);
                }
                child.Add(value, masuk);
            }
            return child;
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
            List<int> AL1 = new List<int>();
            List<List<int>> Bucket = new List<List<int>>();
            for (int i = 0; i < V; i++)
            {
                Bucket.Add(new List<int> { i });
                if (i != awal && !adj[awal].Contains(i))
                {
                    Dictionary<int, List<int>> hasil = DFS(AL1, awal, i, V);
                    if (hasil.Count > 0)
                    {
                        List<int> child = new List<int>();
                        child = hasil.First(x => x.Key == awal).Value;
                        List<int> child2 = hasil.FirstOrDefault(x => x.Key == i).Value;
                        IEnumerable<int> res = child.AsQueryable().Intersect(child2);
                        //Tambahkan friend recommendation
                        foreach (var masukkin in res)
                        {
                            Bucket.ElementAt(Bucket.Count - 1).Add(masukkin);
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
            if (!adj[awal].Contains(akhir))
            {
                _ = DFS(bracket, awal, akhir, V);
            }
            else
            {
                bracket.Add(awal);
                bracket.Add(akhir);
            }
            return bracket;
        }


        public List<int> ExploreBFS(int awal, int akhir)
        {
            Dictionary<int, int> prev = new Dictionary<int, int>();
            List<int> bracket = new List<int>();
            List<int> bracket2 = new List<int>();
            prev = BFS(bracket, awal, akhir, V);
            if (bracket.Count > 0)
            {
                int dari = bracket[bracket.Count - 1];
                while (dari != awal)
                {
                    bracket2.Add(dari);
                    dari = prev.FirstOrDefault(x => x.Key == dari).Value;
                }
                bracket2.Add(awal);
                bracket2.Reverse();
            }
            return bracket2;
        }

        public void InputGraph(List<List<string>> DataNode, Dictionary<int, string> Kamus)
        {
            foreach (var line in DataNode)
            {
                AddEdge(Kamus.FirstOrDefault(x => x.Value == line[0]).Key, Kamus.FirstOrDefault(x => x.Value == line[1]).Key, Kamus);
            }

            for (int i = 0; i < V; i++)
            {
                adj[i].Sort();
            }

        }
    }
}