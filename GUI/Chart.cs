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


        private void Form1_Load(object sender, EventArgs e)
        {
            //typ wykresu
            chart1.Series["Series1"].ChartType = SeriesChartType.Line;

            //testowo wrzucone randomowe wartosci, trzeba bedzie przekazac dane fcji rysujacej
            Random random = new Random();
            for (int pointIndex = 0; pointIndex < 10; pointIndex++)
            {
                chart1.Series["Series1"].Points.AddXY(pointIndex, random.Next(45, 95));
            } 

            BackgroundWorker bw = new BackgroundWorker(); 
            bw.WorkerReportsProgress = true;
            
            bw.DoWork += new DoWorkEventHandler(bw_DoWork); 
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            bw.RunWorkerAsync();
   
            
        }
        string finalPermutation;
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 1; (i <= 10); i++)
            {

                // Perform a time consuming operation and report progress.
                System.Threading.Thread.Sleep(500);

                worker.ReportProgress((i));

            }
            finalPermutation = "1, 2, 3";
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.chart1.Series["Series1"].Points.AddXY(e.ProgressPercentage, 15);
            this.label1.Text = "Najniższy uzyskany koszt: " + e.ProgressPercentage.ToString();
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.label1.Text += ", Permutacja: " + finalPermutation;
        }

    }
}
