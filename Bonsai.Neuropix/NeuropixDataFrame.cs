using NeuropixPXI.Native;
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

        internal NeuropixDataFrame(ElectrodePacket packet, float bufferCapacity)
        {
            unsafe
            {
                var apData = new Mat(ElectrodePacket.PROBE_SUPERFRAMESIZE, ElectrodePacket.PROBE_CHANNEL_COUNT, Depth.S16, 1, (IntPtr)packet.apData);
                ApData = new Mat(ElectrodePacket.PROBE_CHANNEL_COUNT, ElectrodePacket.PROBE_SUPERFRAMESIZE, Depth.S16, 1);
                CV.Transpose(apData, ApData);

                var lfpData = new Mat(1, ElectrodePacket.PROBE_CHANNEL_COUNT, Depth.S16, 1, (IntPtr)packet.lfpData);
                LfpData = new Mat(ElectrodePacket.PROBE_CHANNEL_COUNT, 1, Depth.S16, 1);
                CV.Transpose(lfpData, LfpData);
            }
        }

        internal NeuropixDataFrame(ElectrodePacket[] packets, float bufferCapacity)
        {
            unsafe
            {
                var apData = new Mat(ElectrodePacket.PROBE_SUPERFRAMESIZE, ElectrodePacket.PROBE_CHANNEL_COUNT, Depth.S16, 1, IntPtr.Zero);
                var lfpData = new Mat(1, ElectrodePacket.PROBE_CHANNEL_COUNT, Depth.S16, 1, IntPtr.Zero);
                ApData = new Mat(ElectrodePacket.PROBE_CHANNEL_COUNT, ElectrodePacket.PROBE_SUPERFRAMESIZE * packets.Length, Depth.S16, 1);
                LfpData = new Mat(ElectrodePacket.PROBE_CHANNEL_COUNT, packets.Length, Depth.S16, 1);

                fixed (ElectrodePacket* fpackets = packets)
                {
                    for (int i = 0; i < packets.Length; i++)
                    {
                        apData.SetData((IntPtr)fpackets[i].apData, Mat.AutoStep);
                        CV.Transpose(apData, ApData.GetSubRect(new Rect(ElectrodePacket.PROBE_SUPERFRAMESIZE * i++, 0, apData.Rows, apData.Cols)));

                        lfpData.SetData((IntPtr)fpackets[i].lfpData, Mat.AutoStep);
                        CV.Transpose(lfpData, LfpData.GetSubRect(new Rect(ElectrodePacket.PROBE_SUPERFRAMESIZE * i++, 0, lfpData.Rows, lfpData.Cols)));
                    }
                }
            }
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
