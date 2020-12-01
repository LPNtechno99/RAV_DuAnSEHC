using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using ZedGraph;

namespace AssyChargeSEHC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //setup GraphLeft
            GraphPane paneLeft = graphIRLeft.GraphPane;
            paneLeft.Title.FontSpec.IsBold = true;
            paneLeft.Title.FontSpec.FontColor = System.Drawing.Color.Blue;
            paneLeft.Title.FontSpec.Size = 30;
            paneLeft.Title.Text = "IR Left";
            paneLeft.XAxis.Title.Text = "Time (ms)";
            paneLeft.YAxis.Title.Text = "Value";

            //setup GraphCenter
            GraphPane paneCenter = graphIRCenter.GraphPane;
            paneCenter.Title.FontSpec.IsBold = true;
            paneCenter.Title.FontSpec.FontColor = System.Drawing.Color.Blue;
            paneCenter.Title.FontSpec.Size = 30;
            paneCenter.Title.Text = "IR Center";
            paneCenter.XAxis.Title.Text = "Time (ms)";
            paneCenter.YAxis.Title.Text = "Value";

            //setup Graphright
            GraphPane paneRight = graphIRRight.GraphPane;
            paneRight.Title.FontSpec.IsBold = true;
            paneRight.Title.FontSpec.FontColor = System.Drawing.Color.Blue;
            paneRight.Title.FontSpec.Size = 30;
            paneRight.Title.Text = "IR Right";
            paneRight.XAxis.Title.Text = "Time (ms)";
            paneRight.YAxis.Title.Text = "Value";

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
