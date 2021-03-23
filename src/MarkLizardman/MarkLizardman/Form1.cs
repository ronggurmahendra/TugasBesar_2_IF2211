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
        private Button currentButton;
        private Input input;
        private Graph g;
        // private Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer;
        private Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        private string filename;
        private string algorithm = "null";

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
                button1.Enabled = true;
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

        private void button1_Click(object sender, EventArgs eventArgs){
            String from, to = null, warna = "";
            filename = ofd.FileName;
            this.textBox1.Text = filename;
            // parse input
            input = new Input(filename);
            // initialize graph
            g = new Graph(input.Node);
            g.InputGraph(input.DataNode, input.Kamus);
            this.renderGraph();
            if (this.comboBox1.SelectedItem == null || comboBox1.SelectedItem.ToString() == "")
            {
                this.label6.Text = "Mohon Ambil --Choose Account-- agar bisa diproses";
                return;
            }
            from = this.comboBox1.SelectedItem.ToString();
            if(this.comboBox2.SelectedItem != null) to = this.comboBox2.SelectedItem.ToString();
            List<int> con = null;
            List<List<int>> recom = new List<List<int>>();
            if (this.algorithm == "DFS")
            {
                // dfs
                this.label6.Text = "";
                if (to != "" && to != null)
                {
                    //explore dfs
                    con = this.g.ExploreDFS(g.TranslatetoInt(this.input.Kamus, from), g.TranslatetoInt(this.input.Kamus, to));
                    this.label6.Text += "Eksplore DFS from " + from + " to " + to + "\n";
                    for (int i = 0; i < con.Count - 1; i++)
                    {
                        g.ColorEdgeDFS(con[i], con[i + 1], this.input.Kamus);
                    }
                }
                else
                {
                    //recomDFS
                    recom = this.g.RecommendDFS(g.TranslatetoInt(this.input.Kamus, from));
                    g.ColorNode(g.TranslatetoInt(this.input.Kamus, from), input.Kamus, "Green");
                    warna = "Orange";
                    if (recom.Count > 0)
                    {
                        this.label6.Text += "Friend Recommendation DFS from " + from + "\n";
                    }
                    else
                    {
                        this.label6.Text = from + " harus memperluas koneksi agar rekomendasi muncul \n";
                    }
                }
            }
            else if (this.algorithm == "BFS")
            {
                // bfs
                this.label6.Text = "";
                if (to != null && to != "")
                {
                    //eksplor BFS
                    con = this.g.ExploreBFS(this.g.TranslatetoInt(this.input.Kamus, from), g.TranslatetoInt(this.input.Kamus, to));
                    this.label6.Text += "Eksplore BFS from " + from + " to " + to + " \n";
                    for (int i = 0; i < con.Count - 1; i++)
                    {
                        g.ColorEdgeBFS(con[i], con[i + 1], this.input.Kamus);
                    }
                }
                else
                {
                    //recom BFS
                    this.label6.Text = "";
                    recom = this.g.RecommendBFS(g.TranslatetoInt(this.input.Kamus, from));
                    g.ColorNode(g.TranslatetoInt(this.input.Kamus, from), this.input.Kamus, "YellowGreen");
                    warna = "LightBlue";
                    if (recom.Count > 0)
                    {
                        this.label6.Text += "Friend Recommendation BFS from " + from + "\n";
                    }
                    else
                    {
                        this.label6.Text = from + " harus memperluas koneksi agar rekomendasi muncul \n";
                    }
                }
            }
            else
            {
                // neither
                this.label6.Text = "Mohon Pilih Algoritma DFS atau BFS dulu!!!";
                return;
            }
            if(this.comboBox2.SelectedItem == this.comboBox1.SelectedItem)
            {
                this.label6.Text = "Jangan Pilih diri sendiri dong!!";
                return;
            }
            if(con != null){
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
                
            }
            if(recom != null){
                for (int x = 0; x < recom.Count; x++)
                {
                    for (int y = 0; y < recom[x].Count; y++)
                    {
                        this.label6.Text += g.TranslatetoString(input.Kamus, recom[x][y]);
                        if (y == 0)
                        {
                            this.label6.Text += ":\n";
                            this.label6.Text += "     " + (recom[x].Count-1) + " Mutual friends: ";
                            g.ColorNode(recom[x][0], input.Kamus, warna);
                        }
                        if (y != 0 && y != recom[x].Count - 1 && recom[x].Count>2)
                        {
                            this.label6.Text += ", ";
                        }
                    }
                    this.label6.Text += "\n";
                }
            }
            this.renderGraph();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);
        }

        private void renderGraph(){
            // re-attach the graph to viewer
            // essentially, "update" the view with latest graph
            viewer.Graph = g.GetGraph();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            this.algorithm = "BFS";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            this.algorithm = "DFS";
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = Color.RoyalBlue;
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    /*panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    btnCloseChildForm.Visible = true;*/
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panel2.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.MidnightBlue;
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
    }
}
