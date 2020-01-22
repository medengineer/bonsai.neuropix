using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NeuropixPXI.Native
{
	public struct ElectrodePacket
	{
		public UInt32 timestamp;
		public UInt16 Status;
		public UInt16 payloadLength;
	};

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void np_packetcallback(ref ElectrodePacket packet, IntPtr userdata);
}
