using AssyChargeSEHC.DAO;
using AssyChargeSEHC.ModelEF;
using Sres.Net.EEIP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Zebra.Sdk.Comm;
using ZedGraph;
using Excel = Microsoft.Office.Interop.Excel;

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

        //test
        float[] _arrfl = new float[18] { 8.5f, 4.3f, 3.3f, 2.0f, 1.0f, 1.7f, 8.5f, 4.4f, 3.3f, 2.2f, 0.9f, 1.6f, 8.5f, 4.3f, 3.1f, 2.3f, 1.1f, 1.6f };
        float realtime;

        //Khai bao chuoi luu thong tin hong ngoai
        string _strIRLeft = "";
        string _strIRCenter = "";
        string _strIRRight = "";

        //Khai bao mang byte Vol Cur
        byte[] _arrVolCur = new byte[50];

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += new EventHandler(Timer_Tick);

            //setup GraphLeft
            GraphPane paneLeft = graphIRLeft.GraphPane;
            paneLeft.Title.FontSpec.IsBold = true;
            paneLeft.Title.FontSpec.FontColor = System.Drawing.Color.Blue;
            paneLeft.Title.FontSpec.Size = 30;
            paneLeft.Title.Text = "IR Left";
            paneLeft.XAxis.Title.Text = "Time (ms)";
            paneLeft.YAxis.Title.Text = "Value";
            paneLeft.XAxis.Scale.Min = 0;
            paneLeft.XAxis.Scale.Max = 66;
            paneLeft.XAxis.Scale.MinorStep = 0.5;
            paneLeft.XAxis.Scale.MajorStep = 5;
            paneLeft.YAxis.Scale.Min = -0.2;
            paneLeft.YAxis.Scale.Max = 1.2;
            RollingPointPairList list_left = new RollingPointPairList(60000);
            LineItem curve_left = paneLeft.AddCurve("Pulse", list_left, System.Drawing.Color.Green, SymbolType.None);
            //graphIRLeft.AxisChange();

            //setup GraphCenter
            GraphPane paneCenter = graphIRCenter.GraphPane;
            paneCenter.Title.FontSpec.IsBold = true;
            paneCenter.Title.FontSpec.FontColor = System.Drawing.Color.Blue;
            paneCenter.Title.FontSpec.Size = 30;
            paneCenter.Title.Text = "IR Center";
            paneCenter.XAxis.Title.Text = "Time (ms)";
            paneCenter.YAxis.Title.Text = "Value";
            paneCenter.XAxis.Scale.Min = 0;
            paneCenter.XAxis.Scale.Max = 66;
            paneCenter.XAxis.Scale.MinorStep = 0.5;
            paneCenter.XAxis.Scale.MajorStep = 5;
            paneCenter.YAxis.Scale.Min = -0.2;
            paneCenter.YAxis.Scale.Max = 1.2;
            RollingPointPairList list_center = new RollingPointPairList(60000);
            LineItem curve_center = paneCenter.AddCurve("Pulse", list_center, System.Drawing.Color.Green, SymbolType.None);
            //graphIRCenter.AxisChange();

            //setup Graphright
            GraphPane paneRight = graphIRRight.GraphPane;
            paneRight.Title.FontSpec.IsBold = true;
            paneRight.Title.FontSpec.FontColor = System.Drawing.Color.Blue;
            paneRight.Title.FontSpec.Size = 30;
            paneRight.Title.Text = "IR Right";
            paneRight.XAxis.Title.Text = "Time (ms)";
            paneRight.YAxis.Title.Text = "Value";
            paneRight.XAxis.Scale.Min = 0;
            paneRight.XAxis.Scale.Max = 66;
            paneRight.XAxis.Scale.MinorStep = 0.5;
            paneRight.XAxis.Scale.MajorStep = 5;
            paneRight.YAxis.Scale.Min = -0.2;
            paneRight.YAxis.Scale.Max = 1.2;
            RollingPointPairList list_right = new RollingPointPairList(60000);
            LineItem curve_right = paneRight.AddCurve("Pulse", list_right, System.Drawing.Color.Green, SymbolType.None);
            //graphIRRight.AxisChange();


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

            this.labelOK.DataContext = Common.Instance();
            this.labelNG.DataContext = Common.Instance();
            this.labelTotal.DataContext = Common.Instance();

            this.lbStVolMin.DataContext = DefaultValues.Instance();
            this.lbStVolMax.DataContext = DefaultValues.Instance();
            this.lbChVolMin.DataContext = DefaultValues.Instance();
            this.lbChVolMax.DataContext = DefaultValues.Instance();
            this.lbChCurMin.DataContext = DefaultValues.Instance();
            this.lbChCurMax.DataContext = DefaultValues.Instance();

            StartAppExcel();
            InitializeCOM_PLC();
        }
        void DrawGraph(float[] arr, ZedGraphControl zedGraphControl)
        {
            if (zedGraphControl.GraphPane.CurveList.Count <= 0)
                return;

            LineItem curve = zedGraphControl.GraphPane.CurveList[0] as LineItem;

            if (curve == null)
                return;

            IPointListEdit list = curve.Points as IPointListEdit;

            if (list == null)
                return;
            list.Add(realtime, 0.0f);
            for (int i = 0; i < arr.Length; i++)
            {
                realtime = (realtime + arr[i]) - 0.5f;
                list.Add(realtime, 0.0f);
                list.Add(realtime, 1.0f);
                list.Add(realtime + 0.5f, 1.0f);
                list.Add(realtime + 0.5f, 0.0f);
                realtime += 0.5f;
            }
            list.Add(realtime + 8.5f, 0.0f);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
            zedGraphControl.Refresh();

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
            if (!_flag)
            {
                switch (_StartProgram)
                {
                    case 1:
                        MeasurementValues.Instance().VoltageStandby = (float)7.5;
                        MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.OK;
                        break;
                    case 2:
                        MeasurementValues.Instance().Voltage = (float)25.2;
                        MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;

                        MeasurementValues.Instance().Current = (float)0.998;
                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;

                        break;
                    case 3:
                        MeasurementValues.Instance().IRLeft = "L011X1";
                        MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.OK;
                        MeasurementValues.Instance().IRCenter = "L111XX";
                        MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.OK;
                        MeasurementValues.Instance().IRRight = "L0111X";
                        MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.OK;

                        if (MeasurementValues.Instance().FinalJudgement())
                        {
                            MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.OK;
                            Common.Instance().CountOK += 1;
                        }
                        Common.Instance().CountTotal += 1;
                        Uri fileUri = new Uri(Environment.CurrentDirectory + "\\MyQRCode.png");
                        imgQRCode.Source = new BitmapImage(fileUri);
                        if (_myDataTemplateWorkSheet != null)
                        {
                            _CountDataInTemplate = 1;
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
                        MeasurementValues.Instance().Voltage = (float)25.0;
                        MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;

                        MeasurementValues.Instance().Current = (float)1.032;
                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;
                        break;
                    case 3:
                        MeasurementValues.Instance().IRLeft = "L010X1";
                        MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.NG;
                        MeasurementValues.Instance().IRCenter = "L111XX";
                        MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.OK;
                        MeasurementValues.Instance().IRRight = "L0101X";
                        MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.NG;

                        if (!MeasurementValues.Instance().FinalJudgement())
                        {
                            MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.NG;
                            Common.Instance().CountNG += 1;
                        }
                        Common.Instance().CountTotal += 1;
                        Uri fileUri = new Uri(Environment.CurrentDirectory + "\\MyQRCode.png");
                        imgQRCode.Source = new BitmapImage(fileUri);
                        if (_myDataTemplateWorkSheet != null)
                        {
                            _CountDataInTemplate = 1;
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
                        if (_flagStartRead)
                        {
                            timer.Stop();
                            int count1 = COM_MeasureVolCur.BytesToRead;
                            byte[] bytearr1 = new byte[count1];
                            COM_MeasureVolCur.Read(bytearr1, 0, count1);
                            COM_MeasureVolCur.Close();
                            for (int i = 0; i < bytearr1.Length; i++)
                            {
                                if (bytearr1[i] == 255)
                                {
                                    int j = i + 36;
                                    // Đánh giá OK NG
                                    MeasurementValues.Instance().VoltageStandby = (float)bytearr1[j + 6] / 10f;
                                    if (MeasurementValues.Instance().VoltageStandby > float.Parse(DefaultValues.Instance().StandbyVoltageMin)
                                        && MeasurementValues.Instance().VoltageStandby < float.Parse(DefaultValues.Instance().StandbyVoltageMax))
                                        MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.OK;
                                    else
                                        MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.NG;
                                    _flagStartRead = false;
                                }
                            }
                            //Kích hoạt chế độ đo điện áp và dòng lúc sạc
                            COM_IR.Open();
                            COM_MeasureVolCur.Open();
                            if (COM_IR.IsOpen) COM_IR.Write("1");
                            _currentProgram = 2;
                            timer.Start();
                        }

                        break;


                    // Measure Charging Voltage and Charging Current
                    // Đo điện áp và dòng lúc sạc, xử lý dữ liệu và hiển thị. Đánh giá OK NG
                    // Đóng kết nối COM đo điện áp và dòng.
                    // Gửi tín hiệu để PLC rút đầu đo lên trên (đặt giá trị thanh ghi là 2)
                    // Kích hoạt chế độ thu hồng ngoại
                    case 2:
                        //Đo điện áp, dòng khi sạc và hiển thị
                        if(_flagStartRead)
                        {
                        int count2 = COM_MeasureVolCur.BytesToRead;
                        byte[] bytearr2 = new byte[count2];
                        COM_MeasureVolCur.Read(bytearr2, 0, count2);
                            for (int i = 0; i < bytearr2.Length; i++)
                            {
                                if (bytearr2[i] == 255)
                                {
                                    int j = i + 36;
                                    MeasurementValues.Instance().Voltage = (float)bytearr2[j + 6] / 10f;
                                    if (MeasurementValues.Instance().Voltage > float.Parse(DefaultValues.Instance().ChargingVoltageMin)
                                        && MeasurementValues.Instance().Voltage < float.Parse(DefaultValues.Instance().ChargingVoltageMax))
                                        MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;
                                    else
                                        MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.NG;

                                    switch (bytearr2[j + 8])
                                    {
                                        case 1:
                                            MeasurementValues.Instance().Current = (float)bytearr2[j + 9] / 1000f + 0.25f;
                                            break;
                                        case 2:
                                            MeasurementValues.Instance().Current = (float)bytearr2[j + 9] / 1000f + 0.5f;
                                            break;
                                        case 3:
                                            MeasurementValues.Instance().Current = (float)bytearr2[j + 9] / 1000f + 0.75f;
                                            break;
                                        case 4:
                                            MeasurementValues.Instance().Current = (float)bytearr2[j + 9] / 1000f + 1.0f;
                                            break;
                                        default:
                                            break;
                                    }
                                    if (MeasurementValues.Instance().Current < float.Parse(DefaultValues.Instance().ChargingCurrentMin)
                                        || MeasurementValues.Instance().Current > float.Parse(DefaultValues.Instance().ChargingCurrentMax))
                                    {
                                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.NG;
                                    }
                                    else
                                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;
                                }
                            }
                        }

                        //Đóng kết nói COM đo điện áp và dòng
                        COM_MeasureVolCur.Close();
                        timer.Stop();

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
                        _currentProgram = _StartProgram;
                        break;


                    // Nhận và xử lý tín hiệu hồng ngoại, vẽ đồ thị sóng của IR Left, IR Center, IR Right
                    // Đóng COM IR
                    // Đánh giá OK NG kết quả
                    case 3:
                        if (_strReceiveCOM_IR == "") continue;
                        // Nhận và xử lý tín hiệu hồng ngoại
                        int index1 = _strReceiveCOM_IR.IndexOf("IR");
                        int index2 = _strReceiveCOM_IR.IndexOf("IC");
                        int index3 = _strReceiveCOM_IR.IndexOf("IL");
                        int index4 = _strReceiveCOM_IR.IndexOf("IE");

                        string temp1 = _strReceiveCOM_IR.Substring(index1 + 3, index2 - 4);
                        _strIRRight = temp1.Substring(temp1.IndexOf("85"), temp1.LastIndexOf("85") - temp1.IndexOf("85") - 1);
                        string temp2 = _strReceiveCOM_IR.Substring(index2 + 3, index3 - index2 - 4);
                        _strIRCenter = temp2.Substring(temp2.IndexOf("85"), temp2.LastIndexOf("85") - temp2.IndexOf("85") - 1);
                        string temp3 = _strReceiveCOM_IR.Substring(index3 + 3, index4 - index3 - 4);
                        _strIRLeft = temp3.Substring(temp3.IndexOf("85"), temp3.LastIndexOf("85") - temp3.IndexOf("85") - 1);

                        string[] arr1 = _strIRRight.Split(',');
                        string[] arr2 = _strIRCenter.Split(',');
                        string[] arr3 = _strIRLeft.Split(',');

                        float[] _arrIRRight = new float[arr1.Length];
                        float[] _arrIRCenter = new float[arr2.Length];
                        float[] _arrIRleft = new float[arr3.Length];

                        AddValuesToArrayIR(arr1, _arrIRRight);
                        AddValuesToArrayIR(arr2, _arrIRCenter);
                        AddValuesToArrayIR(arr3, _arrIRleft);

                        // Đánh giá kết quả OK NG
                        CheckDataIR_OKNG(_arrIRRight, _arrIRCenter, _arrIRleft);

                        // Vẽ đồ thị sóng 
                        DrawGraph(_arrIRRight, graphIRRight);
                        realtime = 0;
                        DrawGraph(_arrIRCenter, graphIRCenter);
                        realtime = 0;
                        DrawGraph(_arrIRleft, graphIRLeft);
                        realtime = 0;

                        COM_IR.Close();
                        // Gửi tín hiệu cho PLC nhấc đầu đo lên
                        try
                        {
                            this.Dispatcher.Invoke(new EventHandler((obj, evt) =>
                            {
                                eeipClient.AssemblyObject.setInstance(100, new byte[] { 4 }); // Đặt giá trị thanh ghi PLC là 2
                            }));
                        }
                        catch (Exception)
                        {

                        }
                        _currentProgram = 4;
                        break;
                    case 4:
                        // Đánh giá kết quả cuối cùng
                        Common.Instance().CountTotal += 1;
                        if (MeasurementValues.Instance().FinalJudgement())
                        {
                            MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.OK;
                            Common.Instance().CountOK += 1;
                        }
                        else
                        {
                            MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.NG;
                            Common.Instance().CountNG += 1;
                        }
                        // Gửi dữ liệu cho máy in QRCode
                        SendZplOverTcp(PrinterIPAddress, Common.Instance().QRCodeString(DefaultValues.Instance().IRLeft, DefaultValues.Instance().IRCenter, DefaultValues.Instance().IRRight,
                            MeasurementValues.Instance().VoltageStandby.ToString(), MeasurementValues.Instance().Voltage.ToString(), MeasurementValues.Instance().Current.ToString()));
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

        bool _flagStartRead;
        private void Timer_Tick(object sender, EventArgs e)
        {
            _flagStartRead = true;
        }

        private void COM_MeasureVolCur_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            timer.Start();
            //for (int i = 0; i < bytearr.Length; i++)
            //{
            //    str += bytearr[i] + ",";
            //}
            //this.richMessage.Dispatcher.Invoke(new Action(() => richMessage.AppendText(str + "\n")));
        }
        private void COM_IR_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string tempRecieveString = "IR,44,32,14,8,9,16,85,43,33,14,7,10,16,85,43,32,22,10,16,85,44,32,23,9,16,85,43,33,22,10,16,85,43,32,22,10,16,85,43,32,22,10,16,85,43,32,23,9,16,85,IC,44,10,11,10,21,10,16,85,43,10,11,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,11,10,21,10,16,85,0,44,10,11,10,21,10,16,85,43,IL,32,13,19,17,85,44,32,13,19,16,85,44,32,13,19,17,85,44,32,13,19,16,0,85,44,32,14,18,16,85,44,32,13,19,17,85,44,32,13,19,16,85,44,32,13,18,17,85,44,32,IE";

            string tempRecieveString = "";
            tempRecieveString = COM_IR.ReadTo("IE");
            _strReceiveCOM_IR = tempRecieveString;

            
        }
        bool CheckDataIR_OKNG(float[] arrRight, float[] arrCenter, float[] arrLeft)
        {
            int countR = 0, countC = 0, countL = 0;
            for (int i = 0; i < arrRight.Length; i++)
            {
                if (arrRight[i] == 8.5f)
                {
                    if (arrRight[i + 4] >= 0.8f && arrRight[i + 4] <= 1.4f)
                    {
                        countR++;
                    }
                    if (countR >= 3)
                    {
                        MeasurementValues.Instance().IRRight = DefaultValues.Instance().IRRight;
                        MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.OK;
                    }
                    else
                    {
                        MeasurementValues.Instance().IRRight = "Null";
                        MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.NG;
                    }
                }
            }
            for (int i = 0; i < arrCenter.Length; i++)
            {
                if (arrCenter[i] == 8.5f)
                {
                    if (arrCenter[i + 2] >= 0.8 && arrCenter[i + 3] >= 0.8 && arrCenter[i + 4] >= 0.8 && arrCenter[i + 2] <= 1.4 && arrCenter[i + 3] <= 1.4 && arrCenter[i + 4] <= 1.4)
                    {
                        countC++;
                    }
                    if (countC >= 3)
                    {
                        MeasurementValues.Instance().IRCenter = DefaultValues.Instance().IRCenter;
                        MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.OK;
                    }
                    else
                    {
                        MeasurementValues.Instance().IRCenter = "Null";
                        MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.NG;
                    }
                }
            }
            for (int i = 0; i < arrLeft.Length; i++)
            {
                if (arrLeft[i] == 8.5f)
                {
                    if (arrLeft[i + 3] >= 0.8f && arrLeft[i + 3] <= 1.4f)
                    {
                        countL++;
                    }
                    if (countL >= 3)
                    {
                        MeasurementValues.Instance().IRLeft = DefaultValues.Instance().IRLeft;
                        MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.OK;

                    }
                    else
                    {
                        MeasurementValues.Instance().IRLeft = "Null";
                        MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.NG;
                    }
                }
            }
            if (MeasurementValues.Instance().JudgeIRRight == MeasurementValues.Judge.OK && MeasurementValues.Instance().JudgeIRCenter == MeasurementValues.Judge.OK &&
                MeasurementValues.Instance().JudgeIRLeft == MeasurementValues.Judge.OK)
                return true;
            else
                return false;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            using (var dao = new UserDAO())
            {
                cbbModelList.ItemsSource = dao.GetModelList();
                dgResultList.ItemsSource = dao.GetResultList();

                cbbModelList.SelectedIndex = 0;

                var _s = dao.GetDefaultValues(cbbModelList.SelectedItem.ToString());
                DefaultValues.Instance().ModelName = _s[0].ModelName;
                DefaultValues.Instance().StandbyVoltageMin = _s[0].StandbyVoltageMin;
                DefaultValues.Instance().StandbyVoltageMax = _s[0].StandbyVoltageMax;
                DefaultValues.Instance().ChargingVoltageMin = _s[0].ChargingVoltageMin;
                DefaultValues.Instance().ChargingVoltageMax = _s[0].ChargingVoltageMax;
                DefaultValues.Instance().ChargingCurrentMin = _s[0].ChargingCurrentMin;
                DefaultValues.Instance().ChargingCurrentMax = _s[0].ChargingCurrentMax;

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
                tempRange.Value2 = DefaultValues.Instance().ID;
                tempRange = tempRange.Offset[0, 1];
                // Ngay Thang
                tempRange.Value2 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                tempRange = tempRange.Offset[0, 1];
                // Standby VolMin
                tempRange.Value2 = DefaultValues.Instance().StandbyVoltageMin;
                tempRange = tempRange.Offset[0, 1];
                // Standby VolMax
                tempRange.Value2 = DefaultValues.Instance().StandbyVoltageMax;
                tempRange = tempRange.Offset[0, 1];
                // Charging VolMin
                tempRange.Value2 = DefaultValues.Instance().ChargingVoltageMin;
                tempRange = tempRange.Offset[0, 1];
                // Charging VolMax
                tempRange.Value2 = DefaultValues.Instance().ChargingVoltageMax;
                tempRange = tempRange.Offset[0, 1];
                // Charging CurMin
                tempRange.Value2 = DefaultValues.Instance().ChargingCurrentMin;
                tempRange = tempRange.Offset[0, 1];
                // Charging CurMax
                tempRange.Value2 = DefaultValues.Instance().ChargingCurrentMax;
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
                tempRange.Value2 = MeasurementValues.Instance().Voltage;
                tempRange = tempRange.Offset[0, 1];
                // Current Measurement Value
                tempRange.Value2 = MeasurementValues.Instance().Current;
                tempRange = tempRange.Offset[0, 1];
                // IRLeft Measurement Value
                tempRange.Value2 = MeasurementValues.Instance().IRLeft;
                tempRange = tempRange.Offset[0, 1];
                // IRCenter Measurement Value
                tempRange.Value2 = MeasurementValues.Instance().IRCenter;
                tempRange = tempRange.Offset[0, 1];
                // IRRight Measurement Value
                tempRange.Value2 = MeasurementValues.Instance().IRRight;
                tempRange = tempRange.Offset[0, 1];
                // Judge
                tempRange.Value2 = MeasurementValues.Instance().JudgeFinal;
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


            string tempRecieveString = "IR,44,32,14,8,9,16,85,43,33,14,10,16,85,43,32,22,10,16,85,44,32,23,9,16,85,43,33,22,10,16,85,43,32,22,10,16,85,43,32,22,10,16,85,43,32,23,9,16,85,IC,44,10,11,10,21,10,16,85,43,10,11,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,11,10,21,10,16,85,0,44,10,11,10,21,10,16,85,43,IL,32,13,19,17,85,44,32,13,19,16,85,44,32,13,19,17,85,44,32,13,19,16,0,85,44,32,14,18,16,85,44,32,13,19,17,85,44,32,13,19,16,85,44,32,13,18,17,85,44,32,IE";
            int index1 = tempRecieveString.IndexOf("IR");
            int index2 = tempRecieveString.IndexOf("IC");
            int index3 = tempRecieveString.IndexOf("IL");
            int index4 = tempRecieveString.IndexOf("IE");

            string temp1 = tempRecieveString.Substring(index1 + 3, index2 - 4);
            _strIRRight = temp1.Substring(temp1.IndexOf("85"), temp1.LastIndexOf("85") - temp1.IndexOf("85") - 1);
            string temp2 = tempRecieveString.Substring(index2 + 3, index3 - index2 - 4);
            _strIRCenter = temp2.Substring(temp2.IndexOf("85"), temp2.LastIndexOf("85") - temp2.IndexOf("85") - 1);
            string temp3 = tempRecieveString.Substring(index3 + 3, index4 - index3 - 4);
            _strIRLeft = temp3.Substring(temp3.IndexOf("85"), temp3.LastIndexOf("85") - temp3.IndexOf("85") - 1);

            string[] arr1 = _strIRRight.Split(',');
            string[] arr2 = _strIRCenter.Split(',');
            string[] arr3 = _strIRLeft.Split(',');

            float[] _arrIRRight = new float[arr1.Length];
            float[] _arrIRCenter = new float[arr2.Length];
            float[] _arrIRleft = new float[arr3.Length];

            AddValuesToArrayIR(arr1, _arrIRRight);
            AddValuesToArrayIR(arr2, _arrIRCenter);
            AddValuesToArrayIR(arr3, _arrIRleft);

            CheckDataIR_OKNG(_arrIRRight, _arrIRCenter, _arrIRleft);

            DrawGraph(_arrIRRight, graphIRRight);
            realtime = 0;
            DrawGraph(_arrIRCenter, graphIRCenter);
            realtime = 0;
            DrawGraph(_arrIRleft, graphIRLeft);
            realtime = 0;

            //_StartProgram++;
            //if (_StartProgram > 4)
            //    _StartProgram = 0;
            //Fake_Run();

        }
        void AddValuesToArrayIR(string[] arr1, float[] arr2)
        {
            for (int i = 0; i < arr1.Length; i++)
            {
                arr2[i] = float.Parse(arr1[i]) / 10f;
            }
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

        private void mnuAddEdit_Click(object sender, RoutedEventArgs e)
        {
            var x = sender as MenuItem;
            if (x.Tag.ToString() == "1")
            {
                wdAddModel wd = new wdAddModel();
                wd.lbAddEdit.Content = "Add New Model";
                wd._Mode = wdAddModel.Mode.Add;
                wd.ShowDialog();
            }
            else if (x.Tag.ToString() == "2")
            {
                wdAddModel wd = new wdAddModel();
                wd.EvAddEditDone += Wd_EvAddEditDone;
                wd.lbAddEdit.Content = "Edit Model";
                wd._Mode = wdAddModel.Mode.Edit;
                wd.ShowDialog();
            }
        }

        private void Wd_EvAddEditDone()
        {
            using (var dao = new UserDAO())
            {

                cbbModelList.ItemsSource = dao.GetModelList();
                //var _s = dao.GetDefaultValues(cbbModelList.SelectedItem.ToString());
                //DefaultValues.Instance().ModelName = _s[0].ModelName;
                //DefaultValues.Instance().StandbyVoltageMin = _s[0].StandbyVoltageMin;
                //DefaultValues.Instance().StandbyVoltageMax = _s[0].StandbyVoltageMax;
                //DefaultValues.Instance().ChargingVoltageMin = _s[0].ChargingVoltageMin;
                //DefaultValues.Instance().ChargingVoltageMax = _s[0].ChargingVoltageMax;
                //DefaultValues.Instance().ChargingCurrentMin = _s[0].ChargingCurrentMin;
                //DefaultValues.Instance().ChargingCurrentMax = _s[0].ChargingCurrentMax;
            }
        }
        private void cbbModelList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            using (var dao = new UserDAO())
            {
                cbbModelList.ItemsSource = dao.GetModelList();
            }
        }

        private void Event_PushF3(object sender, ExecutedRoutedEventArgs e)
        {
            wdCheckQRCode wd = new wdCheckQRCode();
            wd.ShowDialog();
        }

        private void Event_PushF4(object sender, ExecutedRoutedEventArgs e)
        {
            mnuAddEdit_Click(null, null);
        }
        private void Event_PushF5(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void cbbModelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var dao = new UserDAO())
            {
                var lst = dao.GetDefaultValues(cbbModelList.SelectedItem.ToString());
                lbModelInfo.Content = lst[0].ModelName + "/" + lst[0].StandbyVoltageMin + "/" + lst[0].StandbyVoltageMax + "/" + lst[0].ChargingVoltageMin
                     + "/" + lst[0].ChargingVoltageMax + "/" + lst[0].ChargingCurrentMin + "/" + lst[0].ChargingCurrentMax;

                var _s = dao.GetDefaultValues(cbbModelList.SelectedItem.ToString());
                DefaultValues.Instance().ModelName = _s[0].ModelName;
                DefaultValues.Instance().StandbyVoltageMin = _s[0].StandbyVoltageMin;
                DefaultValues.Instance().StandbyVoltageMax = _s[0].StandbyVoltageMax;
                DefaultValues.Instance().ChargingVoltageMin = _s[0].ChargingVoltageMin;
                DefaultValues.Instance().ChargingVoltageMax = _s[0].ChargingVoltageMax;
                DefaultValues.Instance().ChargingCurrentMin = _s[0].ChargingCurrentMin;
                DefaultValues.Instance().ChargingCurrentMax = _s[0].ChargingCurrentMax;
            }
        }

        private void mnuEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
