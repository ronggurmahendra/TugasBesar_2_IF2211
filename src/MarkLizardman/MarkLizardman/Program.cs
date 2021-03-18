using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkLizardman
{
    class MainProgram
    {
        public static void Main(String[] args)
        {
            string filename = @"D:\git\TugasBesar_2_IF2211\test\graph.txt";
            Input input = new Input(filename);
            //create the graph content 
            Graph g = new Graph(input.Node);
            List<int> AL = new List<int>();
            Dictionary<int, int> prev = new Dictionary<int, int>();
            List<int> BL = new List<int>();
            List<List<int>> RecomDFS = new List<List<int>>();
            List<List<int>> RecomBFS = new List<List<int>>();
            List<int> ExploreDFS = new List<int>();
            List<int> ExploreBFS = new List<int>();
            int contoh = g.TranslatetoInt(input.Kamus, "Leonard");
            int contoh2 = g.TranslatetoInt(input.Kamus, "Hera");
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
            g.DFS(AL, contoh, contoh2, input.Node);
            g.BFS(BL, contoh, contoh2, prev, input.Node);


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
            for (int i = 0; i < AL.Count; i++)
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
            RecomDFS = g.RecommendDFS(contoh);

            Console.WriteLine("Friend Recommendation DFS from " + g.TranslatetoString(input.Kamus, contoh));
            for (int x = 0; x < RecomDFS.Count; x++)
            {
                for (int y = 0; y < RecomDFS[x].Count; y++)
                {
                    Console.Write(g.TranslatetoString(input.Kamus, RecomDFS[x][y]));
                    if (y == 0)
                    {
                        Console.Write(":\n");
                        Console.Write(RecomDFS[x].Count - 1 + " Mutual friends:");
                    }
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
            Console.WriteLine();
            RecomBFS = g.RecommendDFS(contoh);

            Console.WriteLine("Friend Recommendation BFS from " + g.TranslatetoString(input.Kamus, contoh));
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
            ExploreDFS = g.ExploreDFS(contoh, contoh2);
            Console.WriteLine("Eksplore DFS from " + g.TranslatetoString(input.Kamus, contoh) + " to " + g.TranslatetoString(input.Kamus, contoh2));
            Console.WriteLine("Banyak Koneksi derajat: " + (ExploreDFS.Count - 2));
            if (ExploreDFS.Count > 2)
            {
                for (int x = 0; x < ExploreDFS.Count; x++)
                {
                    Console.Write(g.TranslatetoString(input.Kamus, ExploreDFS[x]));
                    if (x != ExploreDFS.Count - 1)
                    {
                        Console.Write(" -> ");
                    }
                }
            }
            else if (ExploreDFS.Count == 2)
            {
                Console.WriteLine("Sudah berteman, cari yang lain dong!!");
            }
            else
            {
                Console.WriteLine("Tidak ada jalur koneksi yang tersedia! \nAnda harus memulai koneksi baru itu sendiri. ");
            }

            Console.WriteLine();
            Console.WriteLine();

            ExploreBFS = g.ExploreBFS(contoh, contoh2);

            Console.WriteLine("Eksplore BFS from " + g.TranslatetoString(input.Kamus, contoh) + " to " + g.TranslatetoString(input.Kamus, contoh2));
            Console.WriteLine("Banyak Koneksi derajat: " + (ExploreBFS.Count - 2));
            if (ExploreBFS.Count > 2)
            {
                for (int x = 0; x < ExploreBFS.Count; x++)
                {
                    Console.Write(g.TranslatetoString(input.Kamus, ExploreBFS[x]));
                    if (x != ExploreBFS.Count - 1)
                    {
                        Console.Write(" -> ");
                    }
                }
            }
            else if (ExploreBFS.Count == 2)
            {
                Console.WriteLine("Sudah berteman, cari yang lain dong!!");
            }
            else
            {
                Console.WriteLine("Tidak ada jalur koneksi yang tersedia! \nAnda harus memulai koneksi baru itu sendiri. ");
            }
            Console.ReadKey();
        }
    }
}