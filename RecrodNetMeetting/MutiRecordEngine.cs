using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NAudio.Mixer;
using System.Collections;

namespace RecrodNetMeetting
{
    internal class MutiRecordEngine : IDisposable
    {
        private WaveFileWriter writer;
        private readonly MixingSampleProvider mixer;

        public EventHandler<RecordEventArgs> RecordEventArgsHandler;

        public MutiRecordEngine(String destination, int sampleRate = 44100, int channelCount = 2)
        {
            this.writer = new WaveFileWriter(destination, new WaveFormat(sampleRate, channelCount));
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = false;
            mixer.MixerInputEnded += new EventHandler<SampleProviderEventArgs>(MixerInputEndedHandler);
        }
        private void MixerInputEndedHandler(Object sender, SampleProviderEventArgs e)
        {
            if (!finishWrite)
            {
                mixer.AddMixerInput(e.SampleProvider);
            }
        }

        public int secondsRecorded;
        bool finishWrite = false;
        private void streamToFile()
        {
            while (!finishWrite)
            {
                TimeSpan minimalBufferedDuration;
                do
                {
                    // Gets the common duration of sound that all audio recorder captured.
                    minimalBufferedDuration = srList.OrderBy(t => t._waveBuffer.BufferedDuration)
                        .First()._waveBuffer.BufferedDuration;
                    //有一个源无输入
                    if (minimalBufferedDuration.Ticks == 0)
                    {
                        minimalBufferedDuration = srList.OrderBy(t => t._waveBuffer.BufferedDuration)
                            .Last()._waveBuffer.BufferedDuration;
                    }

                    if (minimalBufferedDuration.Ticks > 0)
                    {
                        // Read a sample from the mixer.
                        int bufferLength = (int)minimalBufferedDuration.TotalSeconds * mixer!.WaveFormat.AverageBytesPerSecond;
                        float[] data = new float[bufferLength];
                        int readData = mixer.Read(data, 0, data.Length);
                        if (readData != 0)
                        {
                            writer.WriteSamples(data, 0, readData);
                            secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);

                            //没办法取到WaveStream，暂不在此处输出波形，由录音处分别输出两路

                            //处理时间过长，导致上一级缓冲溢出
                            //byte[] tBuffer = new byte[readData*2]; 

                            //for(int i = 0;i < readData; i++)
                            //{
                            //    short tempSt = (short)(32767f * data[i]);
                            //    tBuffer[i * 2] = (byte)(tempSt & 0xff);//低位
                            //    tBuffer[i * 2 + 1] = (byte)(tempSt >> 8);//高位;
                            //}

                            //WaveStream ws = new RawSourceWaveStream(
                            //    tBuffer, 0, tBuffer.Length,
                            //    srList[0].WaveBuffer.WaveFormat);

                            this.RecordEventArgsHandler?.Invoke(this, 
                                new RecordEventArgs(false, secondsRecorded, null));
                        }
                        else
                        {
                            break;
                        }
                    }
                } while (minimalBufferedDuration.Ticks > 0);
            }
            writer?.Dispose();
            writer = null;
            this.RecordEventArgsHandler?.Invoke(this, new RecordEventArgs(true, 0, null));
        }

        List<SingleRecorder> srList = new List<SingleRecorder>();
        public void AddMixerInput(SingleRecorder recorder)
        {
            srList.Add(recorder);
        }

        public void startRecord()
        {
            foreach(SingleRecorder recorder in srList)
            {
                recorder.startRecord();
                mixer.AddMixerInput(recorder.WaveBuffer);
            }

            Thread writeThread = new Thread(new ThreadStart(() =>
            {
                streamToFile();
            }));
            finishWrite = false;
            writeThread.Start();
        }
        public void stopRecord()
        {
            foreach (SingleRecorder recorder in srList)
            {
                recorder.stopRecord();
            }

            finishWrite = true;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
