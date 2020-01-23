using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NeuropixPXI.Native
{
	[StructLayout(LayoutKind.Sequential)]
	unsafe struct ElectrodePacket
	{
		internal const int PROBE_SUPERFRAMESIZE = 12;
		internal const int PROBE_CHANNEL_COUNT = 384;

		public fixed UInt32 timestamp[PROBE_SUPERFRAMESIZE];
		public fixed Int16 apData[PROBE_SUPERFRAMESIZE * PROBE_CHANNEL_COUNT];
		public fixed Int16 lfpData[PROBE_CHANNEL_COUNT];
		public fixed UInt16 Status[PROBE_SUPERFRAMESIZE];
	};

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	delegate void np_packetcallback(ref ElectrodePacket packet, IntPtr userdata);
}
