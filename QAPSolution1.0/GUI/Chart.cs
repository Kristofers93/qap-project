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
        private String algorithmName = "Algorytm";
        private int iterationNumber;
        private BackgroundWorker bw;
        private string finalPermutation;
        private int curIter=0;
        private int iterGap;

        public Chart(IAlgorithm algorithm, int iterations, String name,int itergap, String filename)
        {
            this.algorithm = algorithm;
            this.algorithmName = name;
            this.iterationNumber = iterations;
            this.iterGap = itergap;
            InitializeComponent();
            this.Text = "Wykres dla danych pobranych z pliku " + filename;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //typ wykresu
            chart1.Series["Koszt"].ChartType = SeriesChartType.Line;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.IsStartedFromZero=false;
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            //dodajemy do algorytmu by mógł notyfikować o zmianach
            algorithm.addBackgroundWorker(bw);
            bw.RunWorkerAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            algorithm.CancelAlgorithm();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //tu tylko puszczamy algorytm
            var worker = sender as BackgroundWorker;
            algorithm.RunAlgorithm();
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            else
            {
                worker.ReportProgress(algorithm.GetMinimalCost());
             }
          
            finalPermutation = string.Join(", ", algorithm.ReturnMinimalResult());
        }

        //algorytm notyfikuje o minimalnym koszcie po każdej iteracji
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if ((this.curIter % this.iterGap) == 0) chart1.Series["Koszt"].Points.AddXY(this.curIter, e.ProgressPercentage);
                label1.Text = "Aktualny koszt w iteracji: " + this.curIter + " : " + e.ProgressPercentage.ToString();
                curIter++;
            }
            catch (Exception)
            {
            }
        }

        //po zakończeniu pracy algorytmu
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                chart1.Series["Koszt"].Points.AddXY(this.curIter, algorithm.GetMinimalCost());
                label1.Text = "wynik : " + algorithm.GetMinimalCost().ToString() + ", permutacja: " + finalPermutation;
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

                //var sw = new StreamWriter(filename);

                //sw.Write(result);
                System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
                file.WriteLine(result);

                file.Close();
               
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}