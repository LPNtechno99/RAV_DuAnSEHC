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
using AssyChargeSEHC.DAO;
using System.Data;
using System.Windows.Controls.Primitives;
using AssyChargeSEHC.ModelEF;
using FileConvert;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;

namespace AssyChargeSEHC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort _port;
        DispatcherTimer timer = new DispatcherTimer();

        //Connect Excel
        Excel.Application _myExcel;
        Excel.Worksheet _myDataTemplateWorkSheet;
        int _CountDataInTemplate;

        string _strRecievieFromCOM = "";
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(Timer_Tick);

            _port = new SerialPort();
            _port.PortName = "COM3";
            _port.BaudRate = 9600;
            _port.Parity = Parity.None;
            _port.DataBits = 8;

            _port.DataReceived += new SerialDataReceivedEventHandler(_port_DataReceived);

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


            //Binding

            this.labelCurVoltage.DataContext = MeasurementValues.Instance();
            this.labelCurrent.DataContext = MeasurementValues.Instance();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _port.Close();
            int index = _strRecievieFromCOM.IndexOf("255");
            string[] arr1 = _strRecievieFromCOM.Split();
            MeasurementValues.Instance().Voltage = (float)(float.Parse(arr1[6].ToString()) / 10);
            if (MeasurementValues.Instance().Voltage < 12.0 || MeasurementValues.Instance().Voltage > 13.2)
            {
                MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.NG;
            }
            else
                MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;

            switch (arr1[8].ToString())
            {
                case "1":
                    MeasurementValues.Instance().Current = float.Parse(arr1[9].ToString()) / (float)1000 + (float)0.250;
                    break;
                case "2":
                    MeasurementValues.Instance().Current = float.Parse(arr1[9].ToString()) / (float)1000 + (float)0.500;
                    break;
                case "3":
                    MeasurementValues.Instance().Current = float.Parse(arr1[9].ToString()) / (float)1000 + (float)0.750;
                    if (MeasurementValues.Instance().Current < 0.900 || MeasurementValues.Instance().Current > 1.100)
                    {
                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.NG;
                    }
                    else
                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;
                    break;
                default:
                    break;
            }
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int count = _port.BytesToRead;
            byte[] bytearr = new byte[count];
            _port.Read(bytearr, 0, count);
            for (int i = 0; i < bytearr.Length - 1; i++)
            {
                _strRecievieFromCOM += bytearr[i] + ",";
            }
            timer.Start();
            //for (int i = 0; i < bytearr.Length; i++)
            //{
            //    str += bytearr[i] + ",";
            //}
            //this.richMessage.Dispatcher.Invoke(new Action(() => richMessage.AppendText(str + "\n")));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var dao = new UserDAO())
            {
                cbbModelList.ItemsSource = dao.GetModelList();
                dgResultList.ItemsSource = dao.GetResultList();

                List<ResultList> lst = dgResultList.ItemsSource as List<ResultList>;
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].Judge == "OK")
                    {

                    }
                }
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure Exit?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            if (tabControlMain.SelectedIndex == 0)
            {
                tabControlMain.SelectedIndex = 1;
            }
            else
            {
                tabControlMain.SelectedIndex = 0;
            }
        }

        void OpenExcelResultFile()
        {
            string currentDir = Environment.CurrentDirectory + "\\" + "TemplateExcel.xlsx";
            string currentDailyData = "D:\\Data\\" + DateTime.Now.ToString("dd/MM/yyyy") + "_DataCollect" + ".xlsx";
            if (!File.Exists(currentDailyData))
            {
                File.Copy(@currentDir, @currentDailyData);
            }

            if (File.Exists(currentDailyData))
            {
                _myExcel.Workbooks.Open(@currentDailyData);
                _myDataTemplateWorkSheet = _myExcel.ActiveWorkbook.Worksheets["Sheet1"];

                Excel.Range tempRange = _myDataTemplateWorkSheet.Range[_myDataTemplateWorkSheet.Cells[1, 1], _myDataTemplateWorkSheet.Cells[10000, 1]];
                tempRange = tempRange.Find("");
                _CountDataInTemplate = tempRange.Row - 1;
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            _port.Open();
            Thread.Sleep(200);
        }
    }
}
