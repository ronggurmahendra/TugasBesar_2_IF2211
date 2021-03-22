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
            if(this.comboBox1.SelectedItem == null) return;
            from = this.comboBox1.SelectedItem.ToString();
            if(this.comboBox2.SelectedItem != null) to = this.comboBox2.SelectedItem.ToString();
            List<int> con = null;
            List<List<int>> recom = new List<List<int>>();
            if(this.radioButton1.Checked){
                // dfs
                this.label6.Text = "";
                if (to != null)
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
                        this.label6.Text+= "Sudah berteman, cari yang lain dong!!" + "\n";
                    }
                    else
                    {
                        this.label6.Text += "Tidak ada jalur koneksi yang tersedia! \nAnda harus memulai koneksi baru itu sendiri. " + "\n";
                    }
            }
            else if(this.radioButton2.Checked){
                // bfs
                this.label6.Text = "";
                if (to != null)
                    con = this.g.ExploreBFS(this.g.TranslatetoInt(this.input.Kamus, from), g.TranslatetoInt(this.input.Kamus, to));
                    this.label6.Text += "Eksplore BFS from " + from + " to " + to + "\n";
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

            }
            else {
                // neither
                return;
            }
            for(int i = 0; i < con.Count-1; i++){
                g.ColorEdge(con[i], con[i+1], this.input.Kamus);
            }
            this.renderGraph();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //label6.Text = "aaa";
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
