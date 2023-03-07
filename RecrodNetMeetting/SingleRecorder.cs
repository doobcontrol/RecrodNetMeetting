using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace RecrodNetMeetting
{
    internal class SingleRecorder
    {
        private WasapiCapture _audioCapturer;
        public BufferedWaveProvider _waveBuffer;
        private workStyle myWorkStyle;
        private recorderFrom meRecorderFrom;
        private WaveFormat myWorkWF;

        public workStyle WorkStyle { get => myWorkStyle; }
        public recorderFrom RecorderFrom { get => meRecorderFrom; }
        public WaveFormat WorkWF { get => myWorkWF; }

        private string outputFilename;
        private string outputFolder;
        private WaveFileWriter writer;

        public EventHandler<RecordEventArgs> RecordEventArgsHandler;

        public ISampleProvider WaveBuffer { 
            get {
                ISampleProvider retSP = _waveBuffer.ToSampleProvider();

                //把MIC声音放大10倍
                if (meRecorderFrom == recorderFrom.Mic)
                {
                    var volumeSampleProvider = new VolumeSampleProvider(_waveBuffer.ToSampleProvider());
                    volumeSampleProvider.Volume = 4.0f; // double the amplitude of every sample - may go above 0dB
                    retSP = volumeSampleProvider;
                }

                return retSP;
            }  
        }

        public SingleRecorder(workStyle workStyle, recorderFrom rType, WaveFormat workWF)
        {
            myWorkStyle = workStyle;
            meRecorderFrom = rType;
            myWorkWF = workWF;

            if (rType == recorderFrom.Mic)
            {
                _audioCapturer = getWasapiCapture(workWF); 
            }
            else
            {
                _audioCapturer = getWasapiLoopbacCapture(workWF);
            }
            _audioCapturer.DataAvailable += AudioCapturer_DataAvailable;
            _audioCapturer.RecordingStopped += OnRecordingStopped;
            
            // This buffer can host up to 5 second of audio, after that it crashed when calling AddSamples.
            // So we should make sure we don't store more than this amount.
            _waveBuffer = new BufferedWaveProvider(_audioCapturer.WaveFormat)
            {
                DiscardOnBufferOverflow = false,
                ReadFully = false
            };       
        }
        private string GetFileName(string par)
        {
            //var deviceName = captureDevice.GetType().Name;/

            return $"{par}-{DateTime.Now:yyy-MM-dd HH-mm-ss}.wav";
        }

        private void AudioCapturer_DataAvailable(object? sender, WaveInEventArgs e)
        {
            WaveStream ws;
            if (myWorkStyle == workStyle.Signle)
            {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
                int secondsRecorded = secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);

                ws = new RawSourceWaveStream(
                    e.Buffer, 0, e.BytesRecorded,
                    myWorkWF);
                this.RecordEventArgsHandler?.Invoke(this, new RecordEventArgs(false, secondsRecorded, ws));
            }
            else
            {

                // Add the captured sample to the wave buffer.
                _waveBuffer.AddSamples(e.Buffer, 0, e.BytesRecorded);

                ws = new RawSourceWaveStream(
                    e.Buffer, 0, e.BytesRecorded,
                    myWorkWF);
                this.RecordEventArgsHandler?.Invoke(this, new RecordEventArgs(false, 0, ws));
            }
        }
        private WasapiLoopbackCapture getWasapiLoopbacCapture(WaveFormat workWF)
        {
            var deviceEnum = new MMDeviceEnumerator();

            var renderDevices =
                deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();

            WasapiLoopbackCapture retWasapiLoopbackCapture = new WasapiLoopbackCapture(renderDevices[0]);
            retWasapiLoopbackCapture.WaveFormat = workWF;
            return retWasapiLoopbackCapture;
        }
        private WasapiCapture getWasapiCapture(WaveFormat workWF)
        {
            var deviceEnum = new MMDeviceEnumerator();

            var renderDevices =
                deviceEnum.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();

            WasapiCapture retWasapiCapture = new WasapiCapture(renderDevices[0]);
            retWasapiCapture.WaveFormat = workWF;
            return retWasapiCapture;
        }

        public void startRecord()
        {
            if (myWorkStyle == workStyle.Signle)
            {
                outputFolder = Path.Combine(Path.GetTempPath(), "NAudioDemo");
                Directory.CreateDirectory(outputFolder);
                outputFilename = GetFileName(meRecorderFrom.ToString());
                writer = new WaveFileWriter(Path.Combine(outputFolder, outputFilename), _audioCapturer.WaveFormat);
            }

            _audioCapturer.StartRecording();
        }
        public void stopRecord()
        {
            _audioCapturer.StopRecording();
        }

        void OnRecordingStopped(object? sender, StoppedEventArgs e)
        {
            if (myWorkStyle == workStyle.Signle)
            {
                writer?.Dispose();
                writer = null;
                this.RecordEventArgsHandler?.Invoke(this, new RecordEventArgs(true, 0, null));
            }
        }
    }
    public enum recorderFrom { Mic, Speaker }
    public enum workStyle { Signle, Mix}
}
