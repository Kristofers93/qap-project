using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Model;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace GUI
{
    public partial class Chart : Form
    {
        private readonly IAlgorithm algorithm;
        // Trzeba spowodowac aby w vector byly wyniki z obliczania algorytmu i wtedy mozna 
        //wolac rysowanie i zapisywanie
        private String algorithmName;

        private BackgroundWorker bw;
        private string finalPermutation;

        public Chart(IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //typ wykresu
            chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            //blok testowy
            //testowo wrzucone randomowe wartosci, trzeba bedzie przekazac dane fcji rysujacej
            var random = new Random();
            for (int pointIndex = 0; pointIndex < 10; pointIndex++)
            {
                chart1.Series["Series1"].Points.AddXY(pointIndex, random.Next(45, 95));
            }
            //----koniec bloku testowego
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            bw.RunWorkerAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bw.CancelAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            algorithm.runAlgorithm();

            for (int i = 1; (i <= 10); i++) //TODO Pętla w której będą odbierane cząstkowe wyniki
            {
                // TODO Tu będzie oczekiwanie na pojawienie się kolejnych danych cząstkowych

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    // TODO Tu będzie przykazywanie danych cząstkowych do rysowania wykresu

                    worker.ReportProgress(algorithm.GetMinimalCost());
                }
            }
            finalPermutation = string.Join(", ", algorithm.ReturnMinimalResult());
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                chart1.Series["Series1"].Points.AddXY(15, e.ProgressPercentage);
                label1.Text = "Najniższy uzyskany koszt: " + e.ProgressPercentage.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                label1.Text += ", permutacja: " + finalPermutation;
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveResult(algorithmName);
        }

        private void saveResult(String algorithmName)
        {
            //--------------defaultowa nazwa
            String result = finalPermutation;

            DateTime CurrTime = DateTime.Now;

            var builderString = new StringBuilder();

            builderString.Append(algorithmName);
            builderString.Append(CurrTime.Hour);
            builderString.Append(CurrTime.Minute);
            builderString.Append(CurrTime.Second);

            String fileName = builderString.ToString();
            //-----------------------------okno zapisu
            var sfd = new SaveFileDialog();

            sfd.DefaultExt = ".rf"; // result  file
            sfd.FileName = fileName;
            sfd.Filter = "Result files (.rf)|*.rf";

            // Show save file dialog box
            bool? resultDialog = sfd.ShowDialog();

            // Process save file dialog box results
            if (resultDialog == true)
            {
                // Save document
                string filename = sfd.FileName;

                var sw = new StreamWriter(filename);

                sw.Write(result);
            }
        }
    }
}