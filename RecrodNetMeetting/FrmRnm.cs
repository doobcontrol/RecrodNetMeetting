using NAudio.Gui;
using NAudio.Wave;

namespace RecrodNetMeetting
{
    public partial class FrmRnm : Form
    {
        int sampleRate = 8000;
        int channels = 1;
        WaveFormat workWF;

        bool inRecording = false;
        SingleRecorder sr;
        MutiRecordEngine mr;

        private string outputFilename;
        private readonly string outputFolder;

        public FrmRnm()
        {
            InitializeComponent();

            this.Text = "Â¼Òô³ÌÐò";
            btnRecord.Image=Properties.Resources.Microphone_icon;
            lbRecordTime.Text = "";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            rbMix.Checked = true;

            outputFolder = Path.Combine(Path.GetTempPath(), "NAudioDemo");
            Directory.CreateDirectory(outputFolder);

            workWF = new WaveFormat(sampleRate, channels);
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (inRecording)
            {
                stop();
            }
            else
            {
                start();
            }
        }
        private void start()
        {
            setUIRecordingStatus(true);

            if (gbRecordStyle.Tag.ToString() == "»ìÂ¼")
            {
                outputFilename = GetFileName();
                mr = new MutiRecordEngine(
                    Path.Combine(outputFolder, outputFilename),
                    sampleRate, channels);
                mr.RecordEventArgsHandler += new EventHandler<RecordEventArgs>(onRecordMsg);

                SingleRecorder sr = new SingleRecorder(workStyle.Mix, recorderFrom.Mic, workWF);
                sr.RecordEventArgsHandler += new EventHandler<RecordEventArgs>(onRecordMsg);
                mr.AddMixerInput(sr); 
                sr = new SingleRecorder(workStyle.Mix, recorderFrom.Speaker, workWF);
                sr.RecordEventArgsHandler += new EventHandler<RecordEventArgs>(onRecordMsg);
                mr.AddMixerInput(sr);

                mr.startRecord();

            }
            else if (gbRecordStyle.Tag.ToString() == "ÄÚÂ¼")
            {
                sr = new SingleRecorder(workStyle.Signle, recorderFrom.Speaker, workWF);
                sr.RecordEventArgsHandler += new EventHandler<RecordEventArgs>(onRecordMsg);
                sr.startRecord();
            }
            else if (gbRecordStyle.Tag.ToString() == "ÍâÂ¼")
            {
                sr = new SingleRecorder(workStyle.Signle, recorderFrom.Mic, workWF);
                sr.RecordEventArgsHandler += new EventHandler<RecordEventArgs>(onRecordMsg);
                sr.startRecord();
            }
        }
        private void stop()
        {
            if (gbRecordStyle.Tag.ToString() == "»ìÂ¼")
            {
                mr.stopRecord();
            }
            else if (gbRecordStyle.Tag.ToString() == "ÄÚÂ¼")
            {
                sr.stopRecord();
            }
            else if (gbRecordStyle.Tag.ToString() == "ÍâÂ¼")
            {
                sr.stopRecord();
            }
        }


        private void setUIRecordingStatus(bool startRecord)
        {
            if(startRecord == inRecording)
            {
                return;
            }

            if (InvokeRequired)
            {
                this.Invoke(new Action(() => { setUIRecordingStatus(startRecord); }));
            }
            else
            {
                inRecording = startRecord;
                if (inRecording)
                {
                    this.Text = gbRecordStyle.Tag.ToString() + " - Â¼ÒôÖÐ¡­¡­";
                    btnRecord.Image = Properties.Resources.Stop_icon;
                    lbRecordTime.Text = "0";
                    this.ControlBox = false;

                    this.Height -= panelSetting.Height;
                    panelSetting.Visible = false; 
                    initChart();
                }
                else
                {
                    this.Text = "Â¼Òô³ÌÐò";
                    btnRecord.Image = Properties.Resources.Microphone_icon;
                    this.ControlBox = true;

                    panelSetting.Visible = true;
                    this.Height += panelSetting.Height;
                    finishChart();
                }

            }
        }
        private void setRecordTimes(string RecordTimes)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => { setRecordTimes(RecordTimes); }));
            }
            else
            {
                lbRecordTime.Text = RecordTimes;
            }
        }

        private void rbMix_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as RadioButton).Checked)
            {
                gbRecordStyle.Tag = (sender as RadioButton).Text;
            }
        }
    
        private void onRecordMsg(object sender, RecordEventArgs e)
        {
            if (e.IsStop)
            {
                if (gbRecordStyle.Tag.ToString() == "»ìÂ¼")
                {
                    mr.Dispose();
                    mr = null;
                }
                else
                {
                    //sr.Dispose();
                    sr = null;
                }
                setUIRecordingStatus(false);
            }
            else
            {
                if (e.RecordSeconds != 0)
                {
                    setRecordTimes(e.RecordSeconds.ToString());
                }
                if (e.MyWaveStream != null)
                {
                    showWave(sender as SingleRecorder, e.MyWaveStream);
                }
            }
        }
        private string GetFileName()
        {
            //var deviceName = captureDevice.GetType().Name;/

            return $"Mix {sampleRate} {channels} {DateTime.Now:yyy-MM-dd HH-mm-ss}.wav";
        }

        private void btnOpenRecordFileFold_Click(object sender, EventArgs e)
        {
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = windir + @"\explorer.exe";
            prc.StartInfo.Arguments = Path.Combine(Path.GetTempPath(), "NAudioDemo");// outputFolder;
            prc.Start();
        }

        #region ÏÔÊ¾²¨ÐÎ

        private void initChart()
        {
            WaveViewer wv = new WaveViewer();
            wv.SamplesPerPixel = 1;
            panelChart.Controls.Add(wv);
            wv.Dock = DockStyle.Fill;
            if (gbRecordStyle.Tag.ToString() == "»ìÂ¼")
            {
                wv = new WaveViewer();
                wv.SamplesPerPixel = 1;
                panelChart.Controls.Add(wv);
                wv.Height = panelChart.Height / 2;
                wv.Dock = DockStyle.Top;
                wv.SendToBack();
            }
        }
        private void finishChart()
        {
            foreach(WaveViewer wv in panelChart.Controls)
            {
                wv.Dispose();
            }
            panelChart.Controls.Clear();
        }
        private void showWave(SingleRecorder sr, WaveStream ws)
        {
            WaveViewer wv;
            if (gbRecordStyle.Tag.ToString() == "»ìÂ¼")
            {
                if(sr.RecorderFrom == recorderFrom.Mic)
                {
                    wv = panelChart.Controls[0] as WaveViewer;
                }
                else
                {
                    wv = panelChart.Controls[1] as WaveViewer;
                }
            }
            else
            {
                wv = panelChart.Controls[0] as WaveViewer;
            }
            wv.WaveStream = ws;
        }

        #endregion
    }
}