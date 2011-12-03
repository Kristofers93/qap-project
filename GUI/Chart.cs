using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public partial class Chart : Form
    {
        public Chart()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //typ wykresu
            chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            
            //testowo wrzucone randomowe wartosci, trzeba bedzie przekazac dane fcji rysujacej
            Random random = new Random();
            for (int pointIndex = 0; pointIndex < 100; pointIndex++)
            {
                chart1.Series["Series1"].Points.AddY(random.Next(45, 95));
            }
        }
    }
}
