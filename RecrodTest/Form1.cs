using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RecrodTest
{
    public partial class Form1 : Form
    {
        private IWaveIn captureDevice;
        private string outputFilename;
        private readonly string outputFolder;
        private WaveFileWriter writer;

        public Form1()
        {
            InitializeComponent();

            btnRecord.Text = "开始";
            lbRecordTime.Text = "";

            outputFolder = Path.Combine(Path.GetTempPath(), "NAudioDemo");
            Directory.CreateDirectory(outputFolder);
        }

        bool inrecording = false;
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (inrecording)
            {
                StopRecording();
            }
            else
            {
                startRecord();
            }
        }
        private void startRecord()
        {

            if (captureDevice == null)
            {
                captureDevice = CreateWaveInDevice();
            }

            outputFilename = GetFileName();
            writer = new WaveFileWriter(Path.Combine(outputFolder, outputFilename), captureDevice.WaveFormat);
            captureDevice.StartRecording();
            SetControlStates(true);

        }

        private void SetControlStates(bool isRecording)
        {
            inrecording = isRecording;
            if (inrecording)
            {
                btnRecord.Text = "停止";
                lbRecordTime.Text = "0";
                this.ControlBox = false;
            }
            else
            {
                btnRecord.Text = "开始";
                this.ControlBox = true;
            }
        }
        private IWaveIn CreateWaveInDevice()
        {
            IWaveIn newWaveIn;

            var device = getWasapiLoopbackDevice();
            newWaveIn = new WasapiLoopbackCapture(device);

            // Both WASAPI and WaveIn support Sample Rate conversion!
            var sampleRate = 8000; // (int)comboBoxSampleRate.SelectedItem;
            var channels = 1;// comboBoxChannels.SelectedIndex + 1;
            newWaveIn.WaveFormat = new WaveFormat(sampleRate, channels);

            newWaveIn.DataAvailable += OnDataAvailable;
            newWaveIn.RecordingStopped += OnRecordingStopped;
            return newWaveIn;
        }
        private MMDevice getWasapiLoopbackDevice()
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();

            var renderDevices = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();

            return renderDevices[0]; //使用找到的第一个
        }
        void StopRecording()
        {
            Debug.WriteLine("StopRecording");
            captureDevice?.StopRecording();
        }

        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
            }
            else
            {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
                int secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);
                lbRecordTime.Text = secondsRecorded.ToString();
            }
        }

        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<StoppedEventArgs>(OnRecordingStopped), sender, e);
            }
            else
            {
                FinalizeWaveFile();
                if (e.Exception != null)
                {
                    MessageBox.Show(String.Format("A problem was encountered during recording {0}",
                                                  e.Exception.Message));
                }
                SetControlStates(false);
            }
        }

        private void FinalizeWaveFile()
        {
            writer?.Dispose();
            writer = null;
        }

        private string GetFileName()
        {
            var deviceName = captureDevice.GetType().Name;
            var sampleRate = $"{captureDevice.WaveFormat.SampleRate / 1000}kHz";
            var channels = captureDevice.WaveFormat.Channels == 1 ? "mono" : "stereo";

            return $"{deviceName} {sampleRate} {channels} {DateTime.Now:yyy-MM-dd HH-mm-ss}.wav";
        }
    }
}
