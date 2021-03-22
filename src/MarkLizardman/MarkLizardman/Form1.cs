using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkLizardman
{
    public partial class Form1 : Form
    {
        private Input input;
        private Graph g;
        private Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer;

        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void selectFile_pressed(object sender, EventArgs e){
            String filename;
            if(ofd.ShowDialog() == DialogResult.OK){
                filename = ofd.FileName;
                this.textBox1.Text = filename;
                // parse input
                input = new Input(filename);
                // initialize graph
                g = new Graph(input.Node);
                g.InputGraph(input.DataNode, input.Kamus);
                this.renderGraph();
                // clear combobox
                this.comboBox1.Items.Clear();
                this.comboBox2.Items.Clear();
                // add nodes to combobox
                this.comboBox1.Items.AddRange(input.Kamus.Values.ToArray());
                this.comboBox2.Items.AddRange(input.Kamus.Values.ToArray());
                this.comboBox2.Items.Add("");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs eventArgs){
            String from, to = null;
            if (this.comboBox1.SelectedItem == null || comboBox1.SelectedItem == "")
            {
                this.label6.Text = "Mohon Ambil --Choose Account-- terlebih dahulu agar bisa diproses";
                return;
            }
            from = this.comboBox1.SelectedItem.ToString();
            if(this.comboBox2.SelectedItem != null) to = this.comboBox2.SelectedItem.ToString();
            List<int> con = null;
            List<List<int>> recom = new List<List<int>>();
            if (this.radioButton1.Checked)
            {
                // dfs
                this.label6.Text = "";
                if (to != "" && to != null)
                {
                    //explore dfs
                    con = this.g.ExploreDFS(g.TranslatetoInt(this.input.Kamus, from), g.TranslatetoInt(this.input.Kamus, to));
                    this.label6.Text += "Eksplor DFS from " + from + " to " + to + "\n";
                    if (con.Count > 2)
                    {
                        label6.Text += "Derajat Koneksi: " + (con.Count - 2) + "\n";
                        for (int x = 0; x < con.Count; x++)
                        {
                            label6.Text += (g.TranslatetoString(input.Kamus, con[x]));
                            if (x != con.Count - 1)
                            {
                                label6.Text += " -> ";
                            }
                        }
                    }
                    else if (con.Count == 2)
                    {
                        this.label6.Text += "Sudah berteman, cari yang lain dong!!" + "\n";
                    }
                    else
                    {
                        this.label6.Text += "Tidak ada jalur koneksi yang tersedia! \nAnda harus memulai koneksi baru itu sendiri. " + "\n";
                    }
                    for (int i = 0; i < con.Count - 1; i++)
                    {
                        g.ColorEdge(con[i], con[i + 1], this.input.Kamus);
                    }
                }
                else
                {
                    //recomDFS
                    recom = this.g.RecommendDFS(g.TranslatetoInt(this.input.Kamus, from));
                    g.ColorNode(g.TranslatetoInt(this.input.Kamus, from), input.Kamus, "Green");
                    this.label6.Text += "Friend Recommendation DFS from " + from + " \n";
                    for (int x = 0; x < recom.Count; x++)
                    {
                        for (int y = 0; y < recom[x].Count; y++)
                        {
                            this.label6.Text += g.TranslatetoString(input.Kamus, recom[x][y]);
                            if (y == 0)
                            {
                                this.label6.Text += ":\n";
                                this.label6.Text += "     " + (recom[x].Count-1) + " Mutual friends: ";
                                g.ColorNode(recom[x][0], input.Kamus, "Orange");
                            }
                            if (y != 0 && y != recom[x].Count - 1 && recom[x].Count>2)
                            {
                                this.label6.Text += ", ";
                            }
                        }
                        this.label6.Text += "\n";
                    }
                }
            }
            else if (this.radioButton2.Checked)
            {
                // bfs
                this.label6.Text = "";
                if (to != null && to != "")
                {
                    con = this.g.ExploreBFS(this.g.TranslatetoInt(this.input.Kamus, from), g.TranslatetoInt(this.input.Kamus, to));
                    this.label6.Text += "Eksplore BFS from " + from + " to " + to + " \n";
                    if (con.Count > 2)
                    {
                        this.label6.Text += "Derajat Koneksi: " + (con.Count - 2) + "\n";
                        for (int x = 0; x < con.Count; x++)
                        {
                            this.label6.Text += g.TranslatetoString(input.Kamus, con[x]);
                            if (x != con.Count - 1)
                            {
                                this.label6.Text += " -> ";
                            }
                        }
                    }
                    else if (con.Count == 2)
                    {
                        this.label6.Text += "Sudah berteman, cari yang lain dong!!" + "\n";
                    }
                    else
                    {
                        this.label6.Text += "Tidak ada jalur koneksi yang tersedia! \nAnda harus memulai koneksi baru itu sendiri. " + "\n";
                    }
                    for (int i = 0; i < con.Count - 1; i++)
                    {
                        g.ColorEdge(con[i], con[i + 1], this.input.Kamus);
                    }
                }
                else
                {
                    this.label6.Text = "";
                    recom = this.g.RecommendBFS(g.TranslatetoInt(this.input.Kamus, from));
                    this.label6.Text += "Friend Recommendation BFS from " + from + "\n";
                    for (int a = 0; a < recom.Count; a++)
                    {
                        for (int b = 0; b < recom[a].Count; b++)
                        {
                            this.label6.Text+=(g.TranslatetoString(input.Kamus, recom[a][b]));
                            if (b == 0)
                            {
                                this.label6.Text += ":\n";
                                this.label6.Text += "     " + (recom[a].Count - 1) + " Mutual friends: ";
                            }
                            if (b != 0 && b != recom[a].Count - 1 && recom[a].Count > 2)
                            {
                                this.label6.Text += ", ";
                            }
                        }
                        this.label6.Text += "\n";
                    }
                }
            }
            else
            {
                // neither
                this.label6.Text = "Mohon Pilih Algoritma DFS atau BFS dulu!!!";
                return;
            }
            
            this.renderGraph();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void renderGraph(){
            // render graph
            // render using bitmap rendering, not realtime :(
            // TODO: attach GDIViewer to a panel?
            renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(g.GetGraph());
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            renderer.Render(b);
            pictureBox1.Image = b;
        }
    }
}
