using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SheetCutting
{
    public partial class Form1 : Form
    {
        Adapter adapter = new Adapter();
        public Form1()
        {
            InitializeComponent();
        }

        private void startAssemble_click(object sender, EventArgs e)
        {
            pictureBox1.Image = adapter.StartAssemblingRectangles();
        }

        private void generateJson_Click(object sender, EventArgs e)
        {
            adapter.GenerateJson();
        }
    }
}
