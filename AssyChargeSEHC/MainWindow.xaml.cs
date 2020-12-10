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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using IronBarCode;
using Sres.Net.EEIP;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Microsoft.Win32;

namespace AssyChargeSEHC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort COM_MeasureVolCur;
        SerialPort COM_IR;
        EEIPClient eeipClient = null;
        Thread _threadPLC;
        Thread _threadProcess;
        DispatcherTimer timer = new DispatcherTimer();

        //Connect Excel
        Excel.Application _myExcel;
        Excel.Worksheet _myDataTemplateWorkSheet;
        int _CountDataInTemplate;

        string _strReceiveCOM_MeasureVolCur = "";
        string _strReceiveCOM_IR = "";
        const string PrinterIPAddress = "192.168.0.5";
        const string PLCIPAddress = "192.168.0.10";

        uint _StartProgram;
        uint _currentProgram = 0;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler(Timer_Tick);

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

            this.labelVoltageStandby.DataContext = MeasurementValues.Instance();
            this.labelIRLeft.DataContext = MeasurementValues.Instance();
            this.labelIRCenter.DataContext = MeasurementValues.Instance();
            this.labelIRRight.DataContext = MeasurementValues.Instance();
            this.labelVoltage.DataContext = MeasurementValues.Instance();
            this.labelCurrent.DataContext = MeasurementValues.Instance();

            this.labelJudgeVoltageStandby.DataContext = MeasurementValues.Instance();
            this.labelJudgeIRLeft.DataContext = MeasurementValues.Instance();
            this.labelJudgeIRCenter.DataContext = MeasurementValues.Instance();
            this.labelJudgeIRRight.DataContext = MeasurementValues.Instance();
            this.labelJudgeVoltage.DataContext = MeasurementValues.Instance();
            this.labelJudgeCurrent.DataContext = MeasurementValues.Instance();

            this.labelFinalJudgement.DataContext = MeasurementValues.Instance();

            this.labelPass.DataContext = Common.Instance();
            this.labelNG.DataContext = Common.Instance();
            this.labelTotal.DataContext = Common.Instance();

            StartAppExcel();
            //InitializeCOM_PLC();
        }
        bool _flag;
        void Reset()
        {
            MeasurementValues.Instance().VoltageStandby = (float)0.0;
            MeasurementValues.Instance().Voltage = (float)0.0;
            MeasurementValues.Instance().Current = (float)0.00;
            MeasurementValues.Instance().IRLeft = "Null";
            MeasurementValues.Instance().IRCenter = "Null";
            MeasurementValues.Instance().IRRight = "Null";

            MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.None;
            MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.None;
            MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.None;
            MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.None;
            MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.None;
            MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.None;

            MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.None;

            imgQRCode.Source = null;
        }
        void Fake_Run()
        {
            if(!_flag)
            {
                switch (_StartProgram)
                {
                    case 1:
                        MeasurementValues.Instance().VoltageStandby = (float)7.5;
                        MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.OK;
                        break;
                    case 2:
                        MeasurementValues.Instance().IRLeft = "L011X1";
                        MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.OK;
                        MeasurementValues.Instance().IRCenter = "L111XX";
                        MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.OK;
                        MeasurementValues.Instance().IRRight = "L0111X";
                        MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.OK;
                        break;
                    case 3:
                        MeasurementValues.Instance().Voltage = (float)25.2;
                        MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;

                        MeasurementValues.Instance().Current = (float)0.998;
                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;

                        if (MeasurementValues.Instance().FinalJudgement())
                        {
                            MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.OK;
                            Common.Instance().CountPass = Common.Instance().CountPass + 1;
                        }
                        Common.Instance().CountTotal = Common.Instance().CountTotal + 1;
                        Uri fileUri = new Uri(Environment.CurrentDirectory + "\\MyQRCode.png");
                        imgQRCode.Source = new BitmapImage(fileUri);
                        if (_myDataTemplateWorkSheet != null)
                        {
                            _CountDataInTemplate += 1;
                            var tempRange = (Excel.Range)_myDataTemplateWorkSheet.Cells[_CountDataInTemplate, 1];
                            ExcelTemplateInput(tempRange);
                        }
                        _flag = true;
                        break;
                    case 4:
                        Reset();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (_StartProgram)
                {
                    case 1:
                        MeasurementValues.Instance().VoltageStandby = (float)7.5;
                        MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.OK;
                        break;
                    case 2:
                        MeasurementValues.Instance().IRLeft = "L010X1";
                        MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.NG;
                        MeasurementValues.Instance().IRCenter = "L111XX";
                        MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.OK;
                        MeasurementValues.Instance().IRRight = "L0101X";
                        MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.NG;
                        break;
                    case 3:
                        MeasurementValues.Instance().Voltage = (float)25.0;
                        MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;

                        MeasurementValues.Instance().Current = (float)1.018;
                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;

                        if (MeasurementValues.Instance().FinalJudgement())
                        {
                            MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.OK;
                            Common.Instance().CountPass += 1;
                        }
                        else
                        {
                            MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.NG;
                            Common.Instance().CountNG += 1;
                        }
                        Common.Instance().CountTotal = Common.Instance().CountTotal + 1;
                        Uri fileUri = new Uri(Environment.CurrentDirectory + "\\MyQRCode.png");
                        imgQRCode.Source = new BitmapImage(fileUri);
                        if (_myDataTemplateWorkSheet != null)
                        {
                            _CountDataInTemplate += 1;
                            var tempRange = (Excel.Range)_myDataTemplateWorkSheet.Cells[_CountDataInTemplate, 1];
                            ExcelTemplateInput(tempRange);
                        }
                        _flag = false;
                        break;
                    case 4:
                        Reset();
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Qua trinh hoat dong
        /// </summary>
        void ProcessOperation()
        {
            while (true)
            {
                if (_StartProgram == 1 && _currentProgram == 0)
                    _currentProgram = _StartProgram;
                switch (_currentProgram)
                {
                    // Start Run and Measure Standby Voltage
                    // Mở kết nối COM đo điện áp và dòng điện cơ bản
                    // Hiển thị và đánh giá OK NG
                    // Kích hoạt chế độ đo điện áp và dòng lúc sạc
                    case 1:
                        COM_MeasureVolCur.Open();
                        // Đo điện áp standby


                        // Đánh giá OK NG

                        //Kích hoạt chế độ đo điện áp và dòng lúc sạc
                        COM_IR.Open();
                        if (COM_IR.IsOpen) COM_IR.Write("1");
                        _currentProgram = 2;
                        break;


                    // Measure Charging Voltage and Charging Current
                    // Đo điện áp và dòng lúc sạc, xử lý dữ liệu và hiển thị. Đánh giá OK NG
                    // Đóng kết nối COM đo điện áp và dòng.
                    // Gửi tín hiệu để PLC rút đầu đo lên trên (đặt giá trị thanh ghi là 2)
                    // Kích hoạt chế độ thu hồng ngoại
                    case 2:
                        //Đo điện áp, dòng khi sạc và hiển thị



                        //Đóng kết nói COM đo điện áp và dòng
                        COM_MeasureVolCur.Close();


                        // Gửi tín hiệu cho PLC nhấc đầu đo lên
                        try
                        {
                            this.Dispatcher.Invoke(new EventHandler((obj, evt) =>
                            {
                                eeipClient.AssemblyObject.setInstance(100, new byte[] { 2 }); // Đặt giá trị thanh ghi PLC là 2
                            }));
                        }
                        catch (Exception)
                        {

                        }
                        //Kích hoạt chế độ thu hồng ngoại
                        if (COM_IR.IsOpen) COM_IR.Write("2");
                        _currentProgram = 3;
                        break;


                    // Nhận và xử lý tín hiệu hồng ngoại, vẽ đồ thị sóng của IR Left, IR Center, IR Right
                    // Đóng COM IR
                    // Đánh giá OK NG kết quả
                    case 3:
                        // Nhận và xử lý tín hiệu hồng ngoại


                        // Vẽ đồ thị sóng 


                        // Đánh giá kết quả OK NG

                        COM_IR.Close();
                        _currentProgram = 4;
                        break;
                    case 4:
                        // Đánh giá kết quả cuối cùng

                        // Gửi dữ liệu cho máy in QRCode

                        // Gửi dữ liệu cho PLC đưa cơ cấu lại vị trí bắt đầu, (đặt giá trị thanh ghi về O)

                        _currentProgram = 0;
                        break;
                    default:
                        break;
                }
            }
        }
        void GetSetDataPLC()
        {
            while (true)
            {
                //Read PLC Keyence
                try
                {
                    this.Dispatcher.Invoke(new EventHandler((obj, evt) =>
                    {
                        byte[] result = eeipClient.AssemblyObject.getInstance(100);
                        _StartProgram = EEIPClient.ToUint(result);
                        //label1.Text = string.Format("{0}", EEIPClient.ToUshort(result));
                    }));
                }
                catch (Exception)
                {

                }
                Thread.Sleep(5);
            }
        }
        void InitializeCOM_PLC()
        {
            //Initialize COM measure Voltage, Current
            COM_MeasureVolCur = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            COM_MeasureVolCur.ReadTimeout = 2000;
            COM_MeasureVolCur.WriteTimeout = 2000;
            COM_MeasureVolCur.DataReceived += new SerialDataReceivedEventHandler(COM_MeasureVolCur_DataReceived);

            //Initialize COM check IR
            COM_IR = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            COM_IR.ReadTimeout = 2000;
            COM_IR.WriteTimeout = 2000;
            COM_IR.DataReceived += COM_IR_DataReceived;

            //Initialize eeip connect PLC Keyence
            eeipClient = new EEIPClient();
            eeipClient.IPAddress = PLCIPAddress;
            eeipClient.RegisterSession();


            //Chạy thread đọc dữ liệu PLC
            _threadPLC = new Thread(GetSetDataPLC);
            _threadPLC.IsBackground = false;
            _threadPLC.Start();

            //Chạy thread chu trình chạy
            _threadProcess = new Thread(ProcessOperation);
            _threadProcess.IsBackground = false;
            _threadProcess.Start();
        }

        /// <summary>
        /// Send string print
        /// </summary>
        /// <param name="theIpAddress"></param>
        /// <param name="strPrint"></param>
        private void SendZplOverTcp(string theIpAddress, string strPrint)
        {
            // Instantiate connection for ZPL TCP port at given address
            Connection thePrinterConn = new TcpConnection(theIpAddress, TcpConnection.DEFAULT_ZPL_TCP_PORT);

            try
            {
                // Open the connection - physical connection is established here.
                thePrinterConn.Open();

                // This example prints "This is a ZPL test." near the top of the label.
                string zplData = "^XA^FO20,20^A0N,25,25^FD" + strPrint + "^FS^XZ";

                // Send the data to printer as a byte array.
                thePrinterConn.Write(Encoding.UTF8.GetBytes(zplData));
            }
            catch (ConnectionException e)
            {
                // Handle communications error here.
                MessageBox.Show(e.ToString());
            }
            finally
            {
                // Close the connection to release resources.
                thePrinterConn.Close();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //COM_MeasureVolCur.Close();
            //int index = _strRecievieFromCOM.IndexOf("255");
            //string[] arr1 = _strRecievieFromCOM.Split();
            //MeasurementValues.Instance().Voltage = (float)(float.Parse(arr1[6].ToString()) / 10);
            //if (MeasurementValues.Instance().Voltage < 12.0 || MeasurementValues.Instance().Voltage > 13.2)
            //{
            //    MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.NG;
            //}
            //else
            //    MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;

            //switch (arr1[8].ToString())
            //{
            //    case "1":
            //        MeasurementValues.Instance().Current = float.Parse(arr1[9].ToString()) / (float)1000 + (float)0.250;
            //        break;
            //    case "2":
            //        MeasurementValues.Instance().Current = float.Parse(arr1[9].ToString()) / (float)1000 + (float)0.500;
            //        break;
            //    case "3":
            //        MeasurementValues.Instance().Current = float.Parse(arr1[9].ToString()) / (float)1000 + (float)0.750;
            //        if (MeasurementValues.Instance().Current < 0.900 || MeasurementValues.Instance().Current > 1.100)
            //        {
            //            MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.NG;
            //        }
            //        else
            //            MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;
            //        break;
            //    default:
            //        break;
            //}
        }

        private void COM_MeasureVolCur_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string tempStringReceive = "";
            tempStringReceive = COM_MeasureVolCur.ReadExisting();

            //int count = COM_MeasureVolCur.BytesToRead;
            //byte[] bytearr = new byte[count];
            //COM_MeasureVolCur.Read(bytearr, 0, count);
            //for (int i = 0; i < bytearr.Length - 1; i++)
            //{
            //    _strRecievieFromCOM += bytearr[i] + ",";
            //}
            //timer.Start();
            //for (int i = 0; i < bytearr.Length; i++)
            //{
            //    str += bytearr[i] + ",";
            //}
            //this.richMessage.Dispatcher.Invoke(new Action(() => richMessage.AppendText(str + "\n")));
        }
        private void COM_IR_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string tempRecieveString = "";
            tempRecieveString = COM_IR.ReadTo("IE");
            string temp1 = tempRecieveString.Substring(tempRecieveString.IndexOf("IR"), tempRecieveString.IndexOf("IC"));
            string temp2 = tempRecieveString.Substring(tempRecieveString.IndexOf("IC"), tempRecieveString.IndexOf("IL"));
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
            if (System.Windows.MessageBox.Show("Are you sure Exit?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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
            string currentDir = Environment.CurrentDirectory + "\\" + "ExcelTemplate.xlsx";
            string currentDailyData = "D:\\Data\\ExcelFile\\" + DateTime.Now.ToString("dd-MM-yyyy") + "_DataCollect" + ".xlsx";
            if (!File.Exists(currentDailyData))
            {
                File.Copy(@currentDir, @currentDailyData);
            }

            if (File.Exists(@currentDailyData))
            {
                _myExcel.Workbooks.Open(@currentDailyData);
                _myDataTemplateWorkSheet = _myExcel.ActiveWorkbook.Worksheets["Sheet1"];

                Excel.Range tempRange = _myDataTemplateWorkSheet.Range[_myDataTemplateWorkSheet.Cells[1, 1], _myDataTemplateWorkSheet.Cells[10000, 1]];
                tempRange = tempRange.Find("");
                _CountDataInTemplate = tempRange.Row - 1;
            }
        }
        /// <summary>
        /// Kill Excel
        /// </summary>
        void KillAppExcel()
        {
            foreach (var process in Process.GetProcessesByName("EXCEL"))
            {
                process.Kill();
            }
        }
        void StartAppExcel()
        {
            KillAppExcel();
            _myExcel = new Excel.Application();

            OpenExcelResultFile();
        }

        private void ExcelTemplateInput(Excel.Range tempRange)
        {
            Dispatcher.Invoke(() =>
            {
                // ID
                tempRange.Value2 = "1";
                tempRange = tempRange.Offset[0, 1];
                // Ngay Thang
                tempRange.Value2 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                tempRange = tempRange.Offset[0, 1];
                // Standby VolMin
                tempRange.Value2 = "7";
                tempRange = tempRange.Offset[0, 1];
                // Standby VolMax
                tempRange.Value2 = "9";
                tempRange = tempRange.Offset[0, 1];
                // Charging VolMin
                tempRange.Value2 = "24.0";
                tempRange = tempRange.Offset[0, 1];
                // Charging VolMax
                tempRange.Value2 = "25.2";
                tempRange = tempRange.Offset[0, 1];
                // Charging CurMin
                tempRange.Value2 = "1.45";
                tempRange = tempRange.Offset[0, 1];
                // Charging CurMax
                tempRange.Value2 = "1.55";
                tempRange = tempRange.Offset[0, 1];
                //Standby IRLeft
                tempRange.Value2 = "L011X1";
                tempRange = tempRange.Offset[0, 1];
                //Standby IR Center
                tempRange.Value2 = "L111XX";
                tempRange = tempRange.Offset[0, 1];
                //Standby IR Right
                tempRange.Value2 = "L0111X";
                tempRange = tempRange.Offset[0, 1];
                // Voltage Measurement Value
                tempRange.Value2 = "24.3";
                tempRange = tempRange.Offset[0, 1];
                // Current Measurement Value
                tempRange.Value2 = "1.52";
                tempRange = tempRange.Offset[0, 1];
                // IRLeft Measurement Value
                tempRange.Value2 = "L011X1";
                tempRange = tempRange.Offset[0, 1];
                // IRCenter Measurement Value
                tempRange.Value2 = "L111XX";
                tempRange = tempRange.Offset[0, 1];
                // IRRight Measurement Value
                tempRange.Value2 = "L0111X";
                tempRange = tempRange.Offset[0, 1];
                // Judge
                tempRange.Value2 = "OK";
                tempRange = tempRange.Offset[0, 1];
            });
        }
        
        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            //Open COM read Voltage and Current
            //_port.Open();
            //Thread.Sleep(200);

            //Read PLC Keyence
            //try
            //{
            //    this.Dispatcher.Invoke(new EventHandler((obj, evt) =>
            //    {
            //        byte[] result = eeipClient.AssemblyObject.getInstance(100);
            //        //label1.Text = string.Format("{0}", EEIPClient.ToUshort(result));
            //        result = eeipClient.AssemblyObject.getInstance(101);
            //        //label2.Text = string.Format("{0}", EEIPClient.ToUshort(result));
            //    }));
            //}
            //catch (Exception)
            //{

            //}
            //SendZplOverTcp("192.168.0.5", "Catilenguyen08052020");
            //if (_myDataTemplateWorkSheet != null)
            //{
            //    _CountDataInTemplate += 1;
            //    var tempRange = (Excel.Range)_myDataTemplateWorkSheet.Cells[_CountDataInTemplate, 1];
            //    ExcelTemplateInput(tempRange);
            //}
            //QRCodeWriter.CreateQrCode("Abc-1234,cde678,0074741740140140401,74981749174", 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("MyQRCode.png");


            //string temp = "IR,44,32,14,8,9,16,85,43,33,14,7,10,16,85,43,32,22,10,16,85,44,32,23,9,16,85,43,33,22,10,16,85,43,32,22,10,16,85,43,32,22,10,16,85,43,32,23,9,16,85,IC,44,10,11,10,21,10,16,85,43,10,11,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,11,10,21,10,16,85,0,44,10,11,10,21,10,16,85,43,IL,32,13,19,17,85,44,32,13,19,16,85,44,32,13,19,17,85,44,32,13,19,16,0,85,44,32,14,18,16,85,44,32,13,19,17,85,44,32,13,19,16,85,44,32,13,18,17,85,44,32,IE";
            //string temp1 = temp.Substring(temp.IndexOf("IR") + 3, temp.IndexOf("IC") - 3);
            //string temp2 = temp.Substring(temp.IndexOf("IC") + 3, temp.IndexOf("IL") - 3);
            //string temp3 = temp.Substring(temp.IndexOf("IL") + 3, temp.IndexOf("IE") - 3);
            //MessageBox.Show(temp1 + "\n" + temp2 + "\n" + temp3);
            _StartProgram++;
            if (_StartProgram > 4)
                _StartProgram = 0;
                Fake_Run();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            try
            {
                var temp = _myExcel.Workbooks.Count;
                _myExcel.ActiveWorkbook.Save();
                switch (temp)
                {
                    case 1:
                        _myExcel.Workbooks[1].Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                        break;
                    case 2:
                        _myExcel.Workbooks[1].Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                        _myExcel.ActiveWorkbook.Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                        break;
                    default:
                        _myExcel.ActiveWorkbook.Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                        _myExcel.Workbooks[2].Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                        _myExcel.Workbooks[1].Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                        _myExcel.Quit();
                        break;
                }
            }
            catch { }
        }

        private void Event_PushF2(object sender, ExecutedRoutedEventArgs e)
        {
            Process.Start("Explorer.exe", "D:\\Data\\ExcelFile");
        }

        private void Event_PushF3(object sender, ExecutedRoutedEventArgs e)
        {
            wdCheckQRCode wd = new wdCheckQRCode();
            wd.ShowDialog();
        }

        private void Event_PushF4(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
