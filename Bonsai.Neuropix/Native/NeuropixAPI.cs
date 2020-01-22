using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NeuropixPXI.Native
{
    static class NeuropixAPI
    {
        const string LibName = "NeuropixAPI_x64_1_17";

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int openBS(int slotID);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int createProbePacketCallback(int slotID, int portID, int dockID, int source, IntPtr handle, np_packetcallback callback, IntPtr userdata);
    }
}
