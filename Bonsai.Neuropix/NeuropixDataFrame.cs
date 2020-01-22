using OpenCV.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.NeuropixPXI
{
    public class NeuropixDataFrame
    {
        const int ChannelCount = 384;
        internal const int SampleCount = 12;

        public NeuropixDataFrame(Mat packet, float bufferCapacity)
        {
        }

        public NeuropixDataFrame(Mat[] packets, float bufferCapacity)
        {
        }

        public Mat StartTrigger { get; private set; }

        public Mat Synchronization { get; private set; }

        public Mat Counters { get; private set; }

        public Mat LfpData { get; private set; }

        public Mat ApData { get; private set; }

        public float BufferCapacity { get; private set; }

        static Mat Transpose(Mat source)
        {
            var result = new Mat(source.Cols, source.Rows, source.Depth, source.Channels);
            CV.Transpose(source, result);
            return result;
        }
    }
}
