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
        ViewModel viewModel = new ViewModel();
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void startAssemble_click(object sender, EventArgs e)
        {
            //pictureBox1.Image = adapter.StartAssemblingRectangles();
            //czesc

            viewModel.StartCuttingSheet();
            pictureBox1.Image = viewModel.GoThroughAllSteps(0);
        }

        private void generateJson_Click(object sender, EventArgs e)
        {
            //adapter.GenerateJson(adapter.GenerateRandomRectanglesAndPositionThem());
        }
        int counter = 0;
        private void prevStep_Click(object sender, EventArgs e)
        {
            counter--;
            pictureBox1.Image = viewModel.GoThroughAllSteps(counter);
        }

        private void nextStep_Click(object sender, EventArgs e)
        {
            counter++;
            pictureBox1.Image = viewModel.GoThroughAllSteps(counter);
        }
    }
}
