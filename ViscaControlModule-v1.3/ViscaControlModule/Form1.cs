using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using OpenCvSharp;
using System.Diagnostics;

namespace ViscaControlModule
{
    public partial class Form1 : Form
    {
        #region Tracking Variance
        private Tracker trackerProcess = new Tracker();

        private CvCapture cap;
        private bool boolenTrack = true;
        private const int camWidth = 640;
        private const int camHeight = 480;
        private int indexCamera = 0;
        private IplImage src;
        private IplImage prevFrame = new IplImage(camWidth, camHeight, BitDepth.U8, 1); // prev frame for track
        private IplImage currFrame = new IplImage(camWidth, camHeight, BitDepth.U8, 1); // current frame for track..
        private IplImage BitMapSizeFrame;
        private double[] BoundingBOX = new double[4]; // 바운딩 박스 변수 

        private CvRect currRect, prevRect;
        private Thread cameraThread;
        #endregion

        #region <Serial Variance>
        private SerialPort serial = new SerialPort();
        private ViscaProtocol visca = new ViscaProtocol();
        private int baudrate = 19200; // defalt
        private string protocol = "VISCAToTeacher"; // defalt => VISCAToStudent
        private string Port = "PORT3"; // defalt
        private bool viscaToStudent = true; // deflat => viscaToStudent

        private string mReceiveData = string.Empty;
        private bool isConnect = false;

        private Stream _serialStream;
        private byte ControlSpeed = 0x08;
        private byte ControlAdress = 0x81;
        #endregion </Serial Variance>
        
        public Form1()
        {
            InitializeComponent();
            Load += new EventHandler(initPortHandler);
            //InitSerialPort();
            
            //if (initWebCamera())
            //{
            //    StartTimer();
            //}
            //else
            //{
            //    Console.WriteLine("웹캠에 문제가 있습니다");
            //}

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load += new EventHandler(initPortHandler);
            
           
        }

        #region <Tracking Process>
        public bool initWebCamera()
        {
            try
            {                                
                cap = CvCapture.FromCamera(CaptureDevice.DShow, indexCamera);        //0은 첫번째 카메라               cap.FrameWidth = camWidth;//640;
                cap.FrameHeight = camHeight; //480;
                cap.FrameWidth = camWidth;
                BitMapSizeFrame = new IplImage(pictureBox1.Width, pictureBox1.Height, BitDepth.U8, 3);


                pictureBox1.Image = cap.QueryFrame().ToBitmap();
                return true;
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        private void StartTimer()
        {
            cameraThread = new Thread(new ThreadStart(TimerElasped));
            cameraThread.Start();
        }

        void TimerElasped()
        {
            while (CvWindow.WaitKey(10) < 0)
            {
                using (src = cap.QueryFrame())
                {

                    if (boolenTrack)
                    {
                        try
                        {
                            int t = Environment.TickCount;
                            Cv.CvtColor(src, currFrame, ColorConversion.BgraToGray);
                            trackerProcess.AdaptiveDifferentialImage(prevFrame, currFrame);
                            t = Environment.TickCount - t;
                            Cv.PutText(src, string.Format("FPS : {0}", 1000.0f / t), Cv.Point(10, 30), new CvFont(FontFace.Vector0, 0.8, 0.8, 1.0, 2), Cv.RGB(255, 0, 0));
                            Cv.PutText(src, string.Format("Center : ({0}, {1})", trackerProcess.histoCenter.X, trackerProcess.histoCenter.Y),
                            Cv.Point(10, 60), new CvFont(FontFace.Vector0, 0.8, 0.8, 1.0, 2), Cv.RGB(255, 0, 0));

                            CvPoint center = new CvPoint(0,0);
                            if (trackerProcess.isTrackSuccess)
                            {                                
                                center.X = trackerProcess.histoCenter.X + trackerProcess.BoundingBox.Width / 2;
                                center.Y = trackerProcess.histoCenter.Y;

                                currRect = trackerProcess.BoundingBox;
                                Cv.Rectangle(src, currRect, Cv.RGB(255, 255, 255), 8);
                                Cv.Circle(src, center, 1, Cv.RGB(255, 0, 0), 8);
                                prevRect = currRect;    
                            }
                            else
                            {
                                Cv.Rectangle(src, prevRect, Cv.RGB(255, 255, 255), 8);
                            }

                            if( center.X != 0)
                                Console.WriteLine(DeterminancePreset(center));
                            
                            
                            int Line = src.Width/6;
                            Cv.PutText(src, string.Format("Preset1"), Cv.Point(Line-100, src.Height - 10), new CvFont(FontFace.Vector0, 0.8, 0.8, 1.0, 2), Cv.RGB(0, 255, 0));
                            Cv.Line(src, Cv.Point(Line, 0), Cv.Point(Line, src.Height), Cv.RGB(0, 255, 0), 3); Line += src.Width / 6;
                            
                            Cv.PutText(src, string.Format("Preset2"), Cv.Point(Line - 100, src.Height - 10), new CvFont(FontFace.Vector0, 0.8, 0.8, 1.0, 2), Cv.RGB(0, 255, 0));
                            Cv.Line(src, Cv.Point(Line, 0), Cv.Point(Line, src.Height), Cv.RGB(0, 255, 0), 3); Line += src.Width / 6;

                            Cv.PutText(src, string.Format("Preset3"), Cv.Point(Line - 100, src.Height - 10), new CvFont(FontFace.Vector0, 0.8, 0.8, 1.0, 2), Cv.RGB(0, 255, 0));
                            Cv.Line(src, Cv.Point(Line, 0), Cv.Point(Line, src.Height), Cv.RGB(0, 255, 0), 3); Line += src.Width / 6;

                            Cv.PutText(src, string.Format("Preset4"), Cv.Point(Line - 100, src.Height - 10), new CvFont(FontFace.Vector0, 0.8, 0.8, 1.0, 2), Cv.RGB(0, 255, 0));
                            Cv.Line(src, Cv.Point(Line, 0), Cv.Point(Line, src.Height), Cv.RGB(0, 255, 0), 3); Line += src.Width / 6;

                            Cv.PutText(src, string.Format("Preset5"), Cv.Point(Line - 100, src.Height - 10), new CvFont(FontFace.Vector0, 0.8, 0.8, 1.0, 2), Cv.RGB(0, 255, 0));
                            Cv.Line(src, Cv.Point(Line, 0), Cv.Point(Line, src.Height), Cv.RGB(0, 255, 0), 3); Line += src.Width / 6;

                            Cv.PutText(src, string.Format("Preset6"), Cv.Point(Line - 100, src.Height - 10), new CvFont(FontFace.Vector0, 0.8, 0.8, 1.0, 2), Cv.RGB(0, 255, 0));

                            Cv.Resize(src, BitMapSizeFrame);
                            //Cv.PyrDown(src, BitMapSizeFrame);

                            pictureBox1.Image = BitMapSizeFrame.ToBitmap();

                            Cv.Copy(currFrame, prevFrame);
                                                       

                        }
                        catch (AccessViolationException ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    else
                    {
                        //Cv.Resize(src, BitMapSizeFrame);
                        //Cv.PyrDown(src, BitMapSizeFrame);

                        pictureBox1.Image = BitMapSizeFrame.ToBitmap();

                    }
                }

            }

        }

        private string DeterminancePreset(CvPoint center)
        {
            int min = 0, max = src.Width/6;
            string result = string.Empty;
            if (center.X >= min && center.X <= max)
            {
               
                result = "Preset1";
            }
            min += src.Width / 6; max += src.Width / 6;
            if (center.X >= min && center.X <= max)
            {
                
                result = "Preset2";
            }
            min += src.Width / 6; max += src.Width / 6;
            if (center.X >= min && center.X <= max)
            {
                
                result = "Preset3";
            }
            min += src.Width / 6; max += src.Width / 6;
            if (center.X >= min && center.X <= max)
            {
                
                result = "Preset4";
            }
            min += src.Width / 6; max += src.Width / 6;
            if (center.X >= min && center.X <= max)
            {
               
                result = "Preset5";
            }
            min += src.Width / 6; max += src.Width / 6;
            if (center.X >= min && center.X <= max)
            {
                
                result = "Preset6";
            }
            return result;
        }
        #endregion </Tracking Process>
        
        #region <SerialPort Process>
        private void initPortHandler(object sender, EventArgs e) // 포트 초기화
        {
            try
            {
                serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);

                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    comboPort.Items.Add(port);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InitSerialPort()
        {
            try
            {
                serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);

                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    comboPort.Items.Add(port);
                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if( isConnect)
                {
                    int length = serial.ReadByte();
                    byte[] buffer = new byte[length];
                    serial.Read(buffer, 0, length);

                    string hex = BitConverter.ToString(buffer);
                    Console.WriteLine(hex);



                    //mReceiveData = serial.ReadExisting();
                    //if (mReceiveData != string.Empty && isConnect)
                    //    Console.WriteLine("Response : " +  mReceiveData);
                }

                //mReceiveData = serial.ReadExisting();
                //if (mReceiveData != string.Empty && isConnect)
                //    Console.WriteLine("Response : " + mReceiveData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void comboBaudrate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {                
                // 통신포트가 열려 있을 경우 닫음
                if (serial.IsOpen)
                {
                    serial.Close();
                    MessageBox.Show(serial.PortName + " : " + serial.BaudRate + ", 8N1");
                    isConnect = false;
                    _serialStream = null;
                }
                //string[] names = comboBaudrate.SelectedItem.ToString().Split(':');
                //serial.BaudRate = int.Parse(names[1].ToString().Trim());
                string names = comboBaudrate.SelectedItem.ToString();
                serial.BaudRate = int.Parse(names);

            }
            catch (Exception)
            {
                Console.WriteLine("열수 없음.");
                Console.WriteLine(serial.PortName + "연결실패");
            }
        }

        private void comboProtocol_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboProtocol.SelectedItem.ToString() == "VISCAToTeacher")
            {
                viscaToStudent = false;
            }
            else
            {
                viscaToStudent = true;
            }
        }

        private void comboPort_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (serial.IsOpen)
            {
                serial.Close();
                _serialStream = null;
                Console.WriteLine(serial.PortName + "해제");
                isConnect = false;
            }
            serial.PortName = comboPort.SelectedItem.ToString();
            //OpenComport(sender, e);
        }

        private void OpenComport(object sender, EventArgs e)
        {
            try
            {
                serial.Open();
                _serialStream = serial.BaseStream;
                Console.WriteLine(serial.PortName + "연결");

                isConnect = true;
            }
            catch (Exception)
            {
                Console.WriteLine(serial.PortName + "연결실패");
                //MessageBox.Show("통신포트 " + serial.PortName + "열 수 없습니다");
                Console.WriteLine("통신포트 " + serial.PortName + "열 수 없습니다");
            }
        }
        
        private void btnEnvironSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (serial.IsOpen)
                {
                    serial.Close(); // 닫기
                    _serialStream = null;
                    Console.WriteLine(serial.PortName + "해제");
                    isConnect = false;
                }
                else
                {
                    serial.DataBits = 8;
                    serial.Parity = Parity.None;
                    serial.Open(); // 열기
                    isConnect = true;
                    Console.WriteLine(serial.PortName + "연결");
                    _serialStream = serial.BaseStream;
                    SendPacketAdressSet();
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(serial.PortName + ex.Message);
                //Console.WriteLine("통신포트 " + serial.PortName + "접근할 수 없습니다.");
            }
            catch (Exception)
            {
                Console.WriteLine("통신포트 " + serial.PortName + "열 수 없습니다");
            }
        }

        private void SendPacketAdressSet()
        {
            
            byte[] buffer = visca.SetAdress();
            serial.Write(buffer, 0, buffer.Length); 
            Thread.Sleep(10); 
        }


        #endregion </SerialPort Process>

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                try
                {

                    visca.ControlTiltUp(_serialStream, ControlSpeed);

                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                try
                {
                    visca.ControlPanLeft(_serialStream, ControlSpeed);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                try
                {
                    visca.ControlPanRight(_serialStream, ControlSpeed);                    

                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                try
                {
                    visca.ControlTiltDown(_serialStream, ControlSpeed);
                    
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Process[] mProcess = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);
            foreach (System.Diagnostics.Process p in mProcess)
            {
                p.Kill();
            }
        }

        private void btnPreset3_Click(object sender, EventArgs e)
        {
            if (isConnect)
            {
                try
                {
                    byte[] buffer = visca.PresetHome(ControlAdress);
                    _serialStream.Write(buffer, 0, buffer.Length);
                    System.Threading.Thread.Sleep(10);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnSpeedUp_Click(object sender, EventArgs e)
        {
            if (ControlSpeed >= 1 && ControlSpeed < 17)
            {
                ControlSpeed++;
                Console.WriteLine( ControlSpeed.ToString() );
            }
        }

        private void btnSpeedDown_Click(object sender, EventArgs e)
        {
            if (ControlSpeed >= 1 && ControlSpeed < 17)
            {
                ControlSpeed--;
                Console.WriteLine(ControlSpeed.ToString());
            }

        }

        private void btnPreset1_Click(object sender, EventArgs e)
        {

            visca.ControlPreset1(_serialStream, ControlAdress);

        }

        private void btnPreset5_Click(object sender, EventArgs e)
        {
            visca.ControlPreset5(_serialStream, ControlAdress);
        }


       
        
    }
}
