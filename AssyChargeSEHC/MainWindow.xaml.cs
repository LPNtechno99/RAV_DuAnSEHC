using AssyChargeSEHC.DAO;
using AssyChargeSEHC.ModelEF;
using IronBarCode;
using Sres.Net.EEIP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public static SerialPort COM_MeasureVolCur;
        public static SerialPort COM_IR;
        EEIPClient eeipClient = null;
        Thread _threadPLC;
        Thread _threadProcess;
        //DispatcherTimer timer = new DispatcherTimer();
        //DispatcherTimer timerPLC = new DispatcherTimer();

        //Connect Excel
        Excel.Application _myExcel;
        Excel.Worksheet _myDataTemplateWorkSheet;
        int _CountDataInTemplate;

        string _strReceiveCOM_IR = "";
        const string PrinterIPAddress = "192.168.254.254";
        const string PLCIPAddress = "192.168.254.10";

        ushort _StartProgram;
        ushort _currentProgram = 0;
        float realtime;

        //Khai bao chuoi luu thong tin hong ngoai

        byte[] _commandONOFF = new byte[10] { 255, 85, 17, 2, 50, 0, 0, 0, 0, 1 };

        int countTime;

        bool _flagSetIni;
        bool _flagStartRead;
        public MainWindow()
        {
            InitializeComponent();

            //timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            //timer.Tick += new EventHandler(Timer_Tick);

            InitialGraph();
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
            Initial_Get_CounterAmount();

        }
        /// <summary>
        /// Khoi tao hoac lay du lieu counter
        /// </summary>
        void Initial_Get_CounterAmount()
        {
            using (var dao = new UserDAO())
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                if (dao.CheckExist(date))
                {
                    int[] arr = dao.GetCounterAmount(date);
                    Common.Instance().CountOK = arr[0];
                    Common.Instance().CountNG = arr[1];
                    Common.Instance().CountTotal = arr[2];
                }
                else
                {
                    dao.AddCounterAmount(date, 0, 0, 0);
                }
            }
        }
        void InitialGraph()
        {

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
        void ResetBackDefault()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                graphIRLeft.GraphPane.CurveList.Clear();
                graphIRLeft.GraphPane.GraphObjList.Clear();
                graphIRLeft.AxisChange();
                graphIRLeft.Refresh();

                graphIRCenter.GraphPane.CurveList.Clear();
                graphIRCenter.GraphPane.GraphObjList.Clear();
                graphIRCenter.AxisChange();
                graphIRCenter.Refresh();

                graphIRRight.GraphPane.CurveList.Clear();
                graphIRRight.GraphPane.GraphObjList.Clear();
                graphIRRight.AxisChange();
                graphIRRight.Refresh();

                InitialGraph();

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
                imgQRCode.InvalidateVisual();

                _flagStartRead = false;
                _flagIR = false;
                _Done1 = false;
                _Done2 = false;
                _flagSetIni = false;
                txtMessage.Text = "";

                COM_MeasureVolCur.Close();
            }));
        }
        //void Fake_Run()
        //{
        //    if (!_flag)
        //    {
        //        switch (_StartProgram)
        //        {
        //            case 1:
        //                MeasurementValues.Instance().VoltageStandby = (float)7.5;
        //                MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.OK;
        //                break;
        //            case 2:
        //                MeasurementValues.Instance().Voltage = (float)25.2;
        //                MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;

        //                MeasurementValues.Instance().Current = (float)0.998;
        //                MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;

        //                break;
        //            case 3:
        //                MeasurementValues.Instance().IRLeft = "L011X1";
        //                MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.OK;
        //                MeasurementValues.Instance().IRCenter = "L111XX";
        //                MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.OK;
        //                MeasurementValues.Instance().IRRight = "L0111X";
        //                MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.OK;

        //                if (MeasurementValues.Instance().FinalJudgement())
        //                {
        //                    MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.OK;
        //                    Common.Instance().CountOK += 1;
        //                }
        //                Common.Instance().CountTotal += 1;
        //                Uri fileUri = new Uri(Environment.CurrentDirectory + "\\MyQRCode.png");
        //                imgQRCode.Source = new BitmapImage(fileUri);
        //                if (_myDataTemplateWorkSheet != null)
        //                {
        //                    _CountDataInTemplate = 1;
        //                    var tempRange = (Excel.Range)_myDataTemplateWorkSheet.Cells[_CountDataInTemplate, 1];
        //                    ExcelTemplateInput(tempRange);
        //                }
        //                _flag = true;
        //                break;
        //            case 4:
        //                Reset();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        switch (_StartProgram)
        //        {
        //            case 1:
        //                MeasurementValues.Instance().VoltageStandby = (float)7.5;
        //                MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.OK;
        //                break;
        //            case 2:
        //                MeasurementValues.Instance().Voltage = (float)25.0;
        //                MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;

        //                MeasurementValues.Instance().Current = (float)1.032;
        //                MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;
        //                break;
        //            case 3:
        //                MeasurementValues.Instance().IRLeft = "L010X1";
        //                MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.NG;
        //                MeasurementValues.Instance().IRCenter = "L111XX";
        //                MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.OK;
        //                MeasurementValues.Instance().IRRight = "L0101X";
        //                MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.NG;

        //                if (!MeasurementValues.Instance().FinalJudgement())
        //                {
        //                    MeasurementValues.Instance().JudgeFinal = MeasurementValues.Judge.NG;
        //                    Common.Instance().CountNG += 1;
        //                }
        //                Common.Instance().CountTotal += 1;
        //                Uri fileUri = new Uri(Environment.CurrentDirectory + "\\MyQRCode.png");
        //                imgQRCode.Source = new BitmapImage(fileUri);
        //                if (_myDataTemplateWorkSheet != null)
        //                {
        //                    _CountDataInTemplate = 1;
        //                    var tempRange = (Excel.Range)_myDataTemplateWorkSheet.Cells[_CountDataInTemplate, 1];
        //                    ExcelTemplateInput(tempRange);
        //                }
        //                _flag = false;
        //                break;
        //            case 4:
        //                Reset();
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
        /// <summary>
        /// Qua trinh hoat dong
        /// </summary>
        async void ProcessOperation()
        {
            while (true)
            {

                if (_StartProgram == 1 && _currentProgram == 0)
                {
                    _currentProgram = _StartProgram;
                }
                _currentProgram = _StartProgram;
                switch (_currentProgram)
                {
                    case 0:
                        if (_flagSetIni)
                        {
                            ResetBackDefault();
                            await Wait1Second();
                        }
                        break;
                    case 1:
                        if (!COM_MeasureVolCur.IsOpen)
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                txtMessage.Text = "RUNNING...\n\t1. Checking Standby Voltage";
                            }));
                            COM_MeasureVolCur.Open();
                            await Wait1Second();
                            await Wait1Second();
                            _flagStartRead = true;
                        }
                        break;
                    case 2:
                        if (_Done1 == true && _flagStartRead == false)
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                txtMessage.Text += "\n\t2. Checking Charging Volatge and Charging Current";
                            }));
                            COM_MeasureVolCur.Write(_commandONOFF, 0, _commandONOFF.Length);
                            await Wait1Second();
                            COM_IR.Write("1");
                            await Wait2Second();
                            await Wait1500MiliSecond();
                            _Done1 = false;
                            _flagStartRead = true;
                        }
                        break;
                    case 3:
                        if (_Done2)
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                txtMessage.Text += "\n\t3. Checking IR";
                            }));
                            if (!COM_IR.IsOpen)
                                COM_IR.Open();
                            COM_IR.Write("0");
                            await Wait2Second();
                            COM_IR.Write("2");
                            _Done2 = false;
                            _flagIR = true;
                        }

                        break;
                    case 4:
                        // Đánh giá kết quả cuối cùng
                        if (_flagIR)
                        {
                            Dispatcher.Invoke(new Action(() =>
                            {
                                txtMessage.Text += "\n\t4. Waiting Final Judge...";
                            }));
                            COM_MeasureVolCur.Write(_commandONOFF, 0, _commandONOFF.Length);
                            await Wait1Second();
                            COM_MeasureVolCur.Close();
                            Common.Instance().CountTotal += 1;
                            //Đánh giá kết quả OK NG cuối cùng
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
                            //Ghi dữ liệu vào file báo cáo Excel
                            if (_myDataTemplateWorkSheet != null)
                            {
                                DefaultValues.Instance().ID++;
                                _CountDataInTemplate += 1;
                                var tempRange = (Excel.Range)_myDataTemplateWorkSheet.Cells[_CountDataInTemplate, 1];
                                ExcelTemplateInput(tempRange);
                            }
                            //Cập nhật dữ liệu Counter
                            using (var dao = new UserDAO())
                            {
                                string dt = DateTime.Now.ToString("dd/MM/yyyy");
                                if (dao.CheckExist(dt))
                                {
                                    dao.EditCounterAmount(dt, Common.Instance().CountOK, Common.Instance().CountNG, Common.Instance().CountTotal);
                                }
                            }
                            if (MeasurementValues.Instance().JudgeFinal == MeasurementValues.Judge.OK)
                            {
                                // Gửi dữ liệu cho máy in QRCode
                                Common.Instance()._ModelCode = DefaultValues.Instance().ModelName;
                                Common.Instance()._Strings = DefaultValues.Instance().ModelName;
                                Common.Instance()._Time = DateTime.Now.ToString("yyyyMMddHHmmss");

                                Common.Instance()._QRCode = Common.Instance().QRCodeString(DefaultValues.Instance().IRLeft, DefaultValues.Instance().IRCenter, DefaultValues.Instance().IRRight,
                                    MeasurementValues.Instance().VoltageStandby.ToString(), MeasurementValues.Instance().Voltage.ToString(), MeasurementValues.Instance().Current.ToString());
                                await Wait1Second();
                                SendZplOverTcp(PrinterIPAddress, DefaultValues.Instance().ModelName, Common.Instance()._QRCode);

                                Dispatcher.Invoke(new Action(() =>
                                {
                                    string _t = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                                    QRCodeWriter.CreateQrCode(Common.Instance()._QRCode, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("D:\\Data\\QRCode\\mQRCode" + _t + ".png");
                                    Uri fileUri = new Uri("D:\\Data\\QRCode\\mQRCode" + _t + ".png");
                                    imgQRCode.Source = new BitmapImage(fileUri);
                                }));
                            }
                            _flagIR = false;
                            _flagSetIni = true;
                            Dispatcher.Invoke(new Action(() =>
                            {
                                txtMessage.Text = "END!";
                            }));
                        }
                        // Gửi dữ liệu cho PLC đưa cơ cấu lại vị trí bắt đầu, (đặt giá trị thanh ghi về O)
                        //_currentProgram = 0;
                        break;
                    default:
                        break;
                }
                Thread.Sleep(100);
            }
        }
        private async Task Wait2Second()
        {
            await Task.Delay(2000);
        }
        private async Task Wait1Second()
        {
            await Task.Delay(1000);
        }
        private async Task Wait1500MiliSecond()
        {
            await Task.Delay(1500);
        }
        private async Task Wait500MiliSecond()
        {
            await Task.Delay(500);
        }

        void GetDataPLC()
        {
            while (true)
            {
                //Read PLC Keyence
                try
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        byte[] result = eeipClient.AssemblyObject.getInstance(100);
                        _StartProgram = EEIPClient.ToUshort(result);
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
            COM_MeasureVolCur = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            COM_MeasureVolCur.ReadTimeout = 2000;
            COM_MeasureVolCur.WriteTimeout = 2000;
            COM_MeasureVolCur.DataReceived += new SerialDataReceivedEventHandler(COM_MeasureVolCur_DataReceived);
            //COM_MeasureVolCur.Open();

            //Initialize COM check IR
            COM_IR = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            COM_IR.ReadTimeout = 2000;
            COM_IR.WriteTimeout = 2000;
            COM_IR.DataReceived += COM_IR_DataReceived;
            COM_IR.Open();

            COM_IR.Write("0");

            //Initialize eeip connect PLC Keyence
            eeipClient = new EEIPClient();
            eeipClient.IPAddress = PLCIPAddress;
            eeipClient.RegisterSession();

            _threadPLC = new Thread(GetDataPLC);
            _threadPLC.IsBackground = false;
            _threadPLC.Start();

            //Chạy thread chu trình chạy
            _threadProcess = new Thread(ProcessOperation);
            _threadProcess.IsBackground = false;
            _threadProcess.Start();

            if (COM_IR.IsOpen && _threadPLC.ThreadState == System.Threading.ThreadState.Running && _threadProcess.ThreadState == System.Threading.ThreadState.Running)
                txtMessage.Text = "ALL READY!";
        }

        /// <summary>
        /// Send string print
        /// </summary>
        /// <param name="theIpAddress"></param>
        /// <param name="strPrint"></param>
        private void SendZplOverTcp(string theIpAddress, string modelCode, string strPrint)
        {
            // Instantiate connection for ZPL TCP port at given address
            Connection thePrinterConn = new TcpConnection(theIpAddress, TcpConnection.DEFAULT_ZPL_TCP_PORT);

            try
            {
                // Open the connection - physical connection is established here.
                thePrinterConn.Open();

                // This example prints "This is a ZPL test." near the top of the label.
                string zplData = "^XA^FO40,220^ADN,18,10^FD" + modelCode + "^FS^FO35,45^BQA,2,4,Q,7^FD" + strPrint + "^FS^XZ";

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

        bool _Done1, _Done2;
        private void COM_MeasureVolCur_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            if (_flagStartRead == true && _currentProgram == 1)
            {
                //COM_MeasureVolCur.DataReceived -= COM_MeasureVolCur_DataReceived;
                int count1 = COM_MeasureVolCur.BytesToRead;
                byte[] bytearr1 = new byte[count1];
                COM_MeasureVolCur.Read(bytearr1, 0, count1);

                for (int i = 0; i < bytearr1.Length; i++)
                {
                    if (bytearr1[i] != 255)
                    {
                        continue;
                    }
                    // Đánh giá OK NG
                    if (_currentProgram == 1 && _Done1 == false)
                    {
                        MeasurementValues.Instance().VoltageStandby = (float)bytearr1[i + 6] / 10f;
                        if (MeasurementValues.Instance().VoltageStandby > DefaultValues.Instance().StandbyVoltageMin
                            && MeasurementValues.Instance().VoltageStandby < DefaultValues.Instance().StandbyVoltageMax)
                            MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.OK;
                        else
                            MeasurementValues.Instance().JudgeVoltageStandby = MeasurementValues.Judge.NG;

                        Array.Clear(bytearr1, 0, bytearr1.Length);

                    }
                    break;
                }
                _Done1 = true;
                _flagStartRead = false;
            }
            if (_flagStartRead == true && _currentProgram == 2)
            {
                //COM_MeasureVolCur.DataReceived -= COM_MeasureVolCur_DataReceived;
                int count1 = COM_MeasureVolCur.BytesToRead;
                int[] bytearr1 = new int[count1];
                for (int i = 0; i < bytearr1.Length; i++)
                {
                    bytearr1[i] = COM_MeasureVolCur.ReadByte();
                }
                //COM_MeasureVolCur.Read(bytearr1, 0, count1);


                for (int i = bytearr1.Length - 1; i > 0; i--)
                {
                    if (bytearr1[i] != 255)
                    {
                        continue;
                    }
                    MeasurementValues.Instance().Voltage = ((float)bytearr1[i - 31] * 256 + (float)bytearr1[i - 30]) / 10f;
                    MeasurementValues.Instance().Current = ((float)bytearr1[i - 28] * 0.25f) + ((float)bytearr1[i - 27]) / 1000f;

                    if (MeasurementValues.Instance().Voltage > DefaultValues.Instance().ChargingVoltageMin
                        && MeasurementValues.Instance().Voltage < DefaultValues.Instance().ChargingVoltageMax)
                        MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.OK;
                    else
                        MeasurementValues.Instance().JudgeVoltage = MeasurementValues.Judge.NG;


                    if (MeasurementValues.Instance().Current < DefaultValues.Instance().ChargingCurrentMin
                        || MeasurementValues.Instance().Current > DefaultValues.Instance().ChargingCurrentMax)
                    {
                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.NG;
                    }
                    else
                        MeasurementValues.Instance().JudgeCurrent = MeasurementValues.Judge.OK;

                    Array.Clear(bytearr1, 0, bytearr1.Length);
                    break;
                }
                _Done2 = true;
                _flagStartRead = false;
            }
        }
        bool _flagIR;
        private void COM_IR_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_flagIR)
            {
                //COM_IR.DataReceived -= COM_IR_DataReceived;
                //string tempRecieveString = "IR,44,32,14,8,9,16,85,43,33,14,7,10,16,85,43,32,22,10,16,85,44,32,23,9,16,85,43,33,22,10,16,85,43,32,22,10,16,85,43,32,22,10,16,85,43,32,23,9,16,85,IC,44,10,11,10,21,10,16,85,43,10,11,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,11,10,21,10,16,85,0,44,10,11,10,21,10,16,85,43,IL,32,13,19,17,85,44,32,13,19,16,85,44,32,13,19,17,85,44,32,13,19,16,0,85,44,32,14,18,16,85,44,32,13,19,17,85,44,32,13,19,16,85,44,32,13,18,17,85,44,32,IE";
                Thread.Sleep(100);
                string tempRecieveString = "";
                //tempRecieveString = COM_IR.ReadTo("IE") + "IE";
                try
                {
                    tempRecieveString = COM_IR.ReadExisting();
                }
                catch
                {

                }
                if (tempRecieveString.IndexOf("IE") >= 0)
                {

                    tempRecieveString = _strReceiveCOM_IR + tempRecieveString;
                    _strReceiveCOM_IR = "";

                    int index1 = tempRecieveString.IndexOf("IR");
                    int index2 = tempRecieveString.IndexOf("IC");
                    int index3 = tempRecieveString.IndexOf("IL");
                    int index4 = tempRecieveString.IndexOf("IE");

                    string temp1 = tempRecieveString.Substring(index1, index2 + 2);
                    //_strIRRight = temp1.Substring(temp1.IndexOf(""), temp1.LastIndexOf("85") - temp1.IndexOf("85") - 1);
                    string temp2 = tempRecieveString.Substring(index2, index3 - index2 + 2);
                    //_strIRCenter = temp2.Substring(temp2.IndexOf("85"), temp2.LastIndexOf("85") - temp2.IndexOf("85") - 1);
                    string temp3 = tempRecieveString.Substring(index3, index4 - index3 + 2);
                    //_strIRLeft = temp3.Substring(temp3.IndexOf("85"), temp3.LastIndexOf("85") - temp3.IndexOf("85") - 1);

                    string[] arr1 = temp1.Split(',');
                    string[] arr2 = temp2.Split(',');
                    string[] arr3 = temp3.Split(',');

                    float[] _arrIRRight = new float[arr1.Length];
                    float[] _arrIRCenter = new float[arr2.Length];
                    float[] _arrIRleft = new float[arr3.Length];

                    // Vẽ đồ thị sóng
                    Dispatcher.Invoke(new Action(() =>
                    {
                        AddValuesToArrayIR(arr1, _arrIRRight);
                        AddValuesToArrayIR(arr2, _arrIRCenter);
                        AddValuesToArrayIR(arr3, _arrIRleft);

                        // Đánh giá kết quả OK NG
                        CheckDataIR_OKNG(_arrIRRight, _arrIRCenter, _arrIRleft);

                        DrawGraph(_arrIRRight, graphIRRight);
                        realtime = 0;
                        DrawGraph(_arrIRCenter, graphIRCenter);
                        realtime = 0;
                        DrawGraph(_arrIRleft, graphIRLeft);
                        realtime = 0;
                    }));
                }
                else
                {
                    _strReceiveCOM_IR += tempRecieveString;
                }
            }
        }
        bool CheckDataIR_OKNG(float[] arrRight, float[] arrCenter, float[] arrLeft)
        {
            int countR = 0, countC = 0, countL = 0;
            for (int i = 0; i < arrRight.Length; i++)
            {
                if (8.2f <= arrRight[i] && arrRight[i] <= 8.6f)
                {
                    if (i <= arrRight.Length - 5)
                    {

                        if (arrRight[i + 4] >= 0.8f && arrRight[i + 4] <= 1.5f)
                        {
                            countR++;
                        }
                        if (countR >= 2)
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
                    //else
                    //{
                    //    MeasurementValues.Instance().IRRight = "Null";
                    //    MeasurementValues.Instance().JudgeIRRight = MeasurementValues.Judge.NG;
                    //}
                }
            }
            for (int i = 0; i < arrCenter.Length; i++)
            {
                if (8.2f <= arrCenter[i] && arrCenter[i] <= 8.6f)
                {
                    if (i <= arrCenter.Length - 7)
                    {
                        if (arrCenter[i + 2] >= 0.8 && arrCenter[i + 3] >= 0.8 && arrCenter[i + 4] >= 0.8 && arrCenter[i + 2] <= 1.5 && arrCenter[i + 3] <= 1.5 && arrCenter[i + 4] <= 1.5)
                        {
                            countC++;
                        }
                        if (countC >= 2)
                        {
                            MeasurementValues.Instance().IRCenter = DefaultValues.Instance().IRCenter;
                            MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.OK;
                        }
                        else
                        {
                            MeasurementValues.Instance().IRCenter = "Null";
                            MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.NG;
                        }
                        if (arrCenter[i + 6] >= 0.7 && arrCenter[i + 6] <= 1.4)
                        {
                            countR++;
                        }
                        if (countR >= 2)
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
                    //else
                    //{
                    //    MeasurementValues.Instance().IRCenter = "Null";
                    //    MeasurementValues.Instance().JudgeIRCenter = MeasurementValues.Judge.NG;
                    //}
                }
            }
            for (int i = 0; i < arrLeft.Length; i++)
            {
                if (8.2f <= arrLeft[i] && arrLeft[i] <= 8.6f)
                {
                    if (i <= arrLeft.Length - 4)
                    {
                        if (arrLeft[i + 3] >= 0.8f && arrLeft[i + 3] <= 1.5f)
                        {
                            countL++;
                        }
                        if (countL >= 2)
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
                //else
                //{
                //    MeasurementValues.Instance().IRLeft = "Null";
                //    MeasurementValues.Instance().JudgeIRLeft = MeasurementValues.Judge.NG;
                //}
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
                //Ghi nhat ky he thống
                dao.AddNewAction(DateTime.Now.ToString("ddMMyyyyHHmmssfff"), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "Open Software, System Start Operation!");

                //Tải dữ liệu model vào combobox
                cbbModelList.ItemsSource = dao.GetModelList();
                dgResultList.ItemsSource = dao.GetResultList();

                cbbModelList.SelectedIndex = 0;

                var _s = dao.GetDefaultValues(cbbModelList.SelectedItem.ToString());
                DefaultValues.Instance().ModelName = _s[0].ModelName;
                DefaultValues.Instance().StandbyVoltageMin = float.Parse(_s[0].StandbyVoltageMin);
                DefaultValues.Instance().StandbyVoltageMax = float.Parse(_s[0].StandbyVoltageMax);
                DefaultValues.Instance().ChargingVoltageMin = float.Parse(_s[0].ChargingVoltageMin);
                DefaultValues.Instance().ChargingVoltageMax = float.Parse(_s[0].ChargingVoltageMax);
                DefaultValues.Instance().ChargingCurrentMin = float.Parse(_s[0].ChargingCurrentMin);
                DefaultValues.Instance().ChargingCurrentMax = float.Parse(_s[0].ChargingCurrentMax);

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
                tempRange.Value2 = DefaultValues.Instance().IRLeft;
                tempRange = tempRange.Offset[0, 1];
                //Standby IR Center
                tempRange.Value2 = DefaultValues.Instance().IRCenter;
                tempRange = tempRange.Offset[0, 1];
                //Standby IR Right
                tempRange.Value2 = DefaultValues.Instance().IRRight;
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
            SendZplOverTcp(PrinterIPAddress, DefaultValues.Instance().ModelName, "/20201217174700/A001-L0111X/A002-L111XX/A003-L011X1/A042-7.6-9.0-7.0/A027-24.6-25.8-24.0/A026-0.982-1.20-0.95/");
            //DEMO1.4DEMO1.4/20201217174700/A001-L0111X/A002-L111XX/A003-L011X1/A042-7.6-9.0-7.0/A027-24.6-25.8-24.0/A026-0.982-1.20-0.95/
            //if (_myDataTemplateWorkSheet != null)
            //{
            //    _CountDataInTemplate += 1;
            //    var tempRange = (Excel.Range)_myDataTemplateWorkSheet.Cells[_CountDataInTemplate, 1];
            //    ExcelTemplateInput(tempRange);
            //}
            //QRCodeWriter.CreateQrCode("Abc-1234,cde678,0074741740140140401,74981749174", 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("MyQRCode.png");


            //string tempRecieveString = "IR,44,32,14,8,9,16,85,43,33,14,10,16,85,43,32,22,10,16,85,44,32,23,9,16,85,43,33,22,10,16,85,43,32,22,10,16,85,43,32,22,10,16,85,43,32,23,9,16,85,IC,44,10,11,10,21,10,16,85,43,10,11,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,10,10,21,10,16,85,43,10,11,10,21,10,16,85,0,44,10,11,10,21,10,16,85,43,IL,32,13,19,17,85,44,32,13,19,16,85,44,32,13,19,17,85,44,32,13,19,16,0,85,44,32,14,18,16,85,44,32,13,19,17,85,44,32,13,19,16,85,44,32,13,18,17,85,44,32,IE";
            //int index1 = tempRecieveString.IndexOf("IR");
            //int index2 = tempRecieveString.IndexOf("IC");
            //int index3 = tempRecieveString.IndexOf("IL");
            //int index4 = tempRecieveString.IndexOf("IE");

            //string temp1 = tempRecieveString.Substring(index1 + 3, index2 - 4);
            //_strIRRight = temp1.Substring(temp1.IndexOf("85"), temp1.LastIndexOf("85") - temp1.IndexOf("85") - 1);
            //string temp2 = tempRecieveString.Substring(index2 + 3, index3 - index2 - 4);
            //_strIRCenter = temp2.Substring(temp2.IndexOf("85"), temp2.LastIndexOf("85") - temp2.IndexOf("85") - 1);
            //string temp3 = tempRecieveString.Substring(index3 + 3, index4 - index3 - 4);
            //_strIRLeft = temp3.Substring(temp3.IndexOf("85"), temp3.LastIndexOf("85") - temp3.IndexOf("85") - 1);

            //string[] arr1 = _strIRRight.Split(',');
            //string[] arr2 = _strIRCenter.Split(',');
            //string[] arr3 = _strIRLeft.Split(',');

            //float[] _arrIRRight = new float[arr1.Length];
            //float[] _arrIRCenter = new float[arr2.Length];
            //float[] _arrIRleft = new float[arr3.Length];

            //AddValuesToArrayIR(arr1, _arrIRRight);
            //AddValuesToArrayIR(arr2, _arrIRCenter);
            //AddValuesToArrayIR(arr3, _arrIRleft);

            //CheckDataIR_OKNG(_arrIRRight, _arrIRCenter, _arrIRleft);

            //DrawGraph(_arrIRRight, graphIRRight);
            //realtime = 0;
            //DrawGraph(_arrIRCenter, graphIRCenter);
            //realtime = 0;
            //DrawGraph(_arrIRleft, graphIRLeft);
            //realtime = 0;

            //_StartProgram++;
            //if (_StartProgram > 4)
            //    _StartProgram = 0;
            //Fake_Run();
        }
        void AddValuesToArrayIR(string[] arr1, float[] arr2)
        {
            for (int i = 1; i < arr1.Length - 1; i++)
            {
                arr2[i] = float.Parse(arr1[i]) / 10f;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Cập nhật dữ liệu Counter
            using (var dao = new UserDAO())
            {
                string dt = DateTime.Now.ToString("dd/MM/yyyy");
                if (dao.CheckExist(dt))
                {
                    dao.EditCounterAmount(dt, Common.Instance().CountOK, Common.Instance().CountNG, Common.Instance().CountTotal);
                }
            }
            try
            {
                if (COM_IR.IsOpen)
                    COM_IR.Write("0");
                _threadPLC.Abort();
                _threadProcess.Abort();
                COM_MeasureVolCur.Close();
                COM_IR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
                wd.EvAddEditDone += Wd_EvAddEditDone;
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
        /// <summary>
        /// Sự kiện Thêm hoặc sửa thành công
        /// </summary>
        private void Wd_EvAddEditDone()
        {
            using (var dao = new UserDAO())
            {
                cbbModelList.SelectedItem = DefaultValues.Instance().ModelName;
                cbbModelList.ItemsSource = dao.GetModelList();
                cbbModelList_SelectionChanged(null, null);
            }
        }
        private void Event_PushF3(object sender, ExecutedRoutedEventArgs e)
        {
            wdCheckQRCode wd = new wdCheckQRCode();
            wd.ShowDialog();
        }

        private void Event_PushF4(object sender, ExecutedRoutedEventArgs e)
        {
            if (Common.Instance().RoleID == 0)
                mnuLogin_Click(null, null);
            else
                mnuSignOut_Click(null, null);
        }
        private void Event_PushF5(object sender, ExecutedRoutedEventArgs e)
        {
            mnuLogs_Click(null, null);
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
                DefaultValues.Instance().StandbyVoltageMin = float.Parse(_s[0].StandbyVoltageMin);
                DefaultValues.Instance().StandbyVoltageMax = float.Parse(_s[0].StandbyVoltageMax);
                DefaultValues.Instance().ChargingVoltageMin = float.Parse(_s[0].ChargingVoltageMin);
                DefaultValues.Instance().ChargingVoltageMax = float.Parse(_s[0].ChargingVoltageMax);
                DefaultValues.Instance().ChargingCurrentMin = float.Parse(_s[0].ChargingCurrentMin);
                DefaultValues.Instance().ChargingCurrentMax = float.Parse(_s[0].ChargingCurrentMax);
            }
        }

        private void mnuSetCurrent_Click(object sender, RoutedEventArgs e)
        {
            wdSetCurrent wd = new wdSetCurrent();
            wd.ShowDialog();
        }

        private void mnuLogin_Click(object sender, RoutedEventArgs e)
        {
            wdLogin wdLogin = new wdLogin();
            wdLogin.EventLogin += WdLogin_EventLogin;
            wdLogin.ShowDialog();
        }
        /// <summary>
        /// Sự kiện hoàn thành việc đăng nhập
        /// </summary>
        /// <param name="logSt"></param>
        /// <param name="roleId"></param>
        private void WdLogin_EventLogin(LoginState logSt, int roleId)
        {
            switch (logSt)
            {
                case LoginState.Success:
                    switch (roleId)
                    {
                        case 1:
                            mnuAdd.IsEnabled = true;
                            mnuEdit.IsEnabled = true;
                            mnuSetCurrent.IsEnabled = true;
                            mnuChangePass.IsEnabled = true;
                            mnuLogs.IsEnabled = true;
                            break;
                        case 2:
                            mnuSetCurrent.IsEnabled = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case LoginState.Fail:
                    MessageBox.Show("Role Name or Password ís not correct!\nTry again!");
                    break;
                case LoginState.Null:
                    if (roleId == 0)
                    {
                        mnuAdd.IsEnabled = false;
                        mnuEdit.IsEnabled = false;
                        mnuSetCurrent.IsEnabled = false;
                        mnuChangePass.IsEnabled = false;
                        mnuLogs.IsEnabled = false;
                    }
                    break;
                default:
                    break;
            }
        }

        private void mnuLogs_Click(object sender, RoutedEventArgs e)
        {
            if (Common.Instance().RoleID == 1)
            {
                wdSystemLogs wdLog = new wdSystemLogs();
                wdLog.ShowDialog();
            }
        }

        private void mnuChangePass_Click(object sender, RoutedEventArgs e)
        {
            wdChangePassword wdChangePass = new wdChangePassword();
            wdChangePass.EventChangePassword += ChangePasswordSuccess;
            wdChangePass.ShowDialog();
        }

        private void ChangePasswordSuccess()
        {
            mnuSignOut_Click(null, null);
        }

        private void mnuSignOut_Click(object sender, RoutedEventArgs e)
        {
            WdLogin_EventLogin(LoginState.Null, 0);
            Common.Instance().RoleID = 0;
        }

        private void Event_PushF6(object sender, ExecutedRoutedEventArgs e)
        {
            if (Common.Instance().RoleID != 0)
            {
                mnuSetCurrent_Click(null, null);
            }
        }

        private void mnuEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
