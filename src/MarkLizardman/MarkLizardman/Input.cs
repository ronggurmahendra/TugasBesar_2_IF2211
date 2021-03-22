using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkLizardman{
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
}