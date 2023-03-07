using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecrodNetMeetting
{
    internal class RecordEventArgs : EventArgs
    {
        public bool IsStop { get; private set; }
        public int RecordSeconds { get; private set; }
        public WaveStream MyWaveStream { get; private set; }

        public RecordEventArgs(bool isStop, int recordSeconds, WaveStream waveStream)
        {
            IsStop = isStop;
            RecordSeconds = recordSeconds;
            MyWaveStream = waveStream;
        }
    }
}
