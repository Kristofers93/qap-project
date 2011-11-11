using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using Structures;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Result.xaml
    /// </summary>
    public partial class Result : Window
    {
        public Result(IParameters parameters, IntMatrix2D A, IntMatrix2D B)
        {
            InitializeComponent();
            LayoutRoot.DataContext = new ResultViewModel(parameters, A, B);
        }
    }
}
