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
        public static extern int openBS(byte slot);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int openProbe(byte slot, sbyte port);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int closeBS(byte slot);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int closeProbe(byte slot, sbyte port);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int init(byte slot, sbyte port);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int arm(byte slotID);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int setTriggerInput(byte slotID, int inputline);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int setSWTrigger(byte slotId);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int readElectrodeData(byte slot, sbyte port, [Out] ElectrodePacket[] packet, out IntPtr actualAmount, IntPtr requestedAmount);

        /*
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int createProbePacketCallback(int slotID, int portID, int dockID, int source, IntPtr handle, np_packetcallback callback, IntPtr userdata);
        */
    }
}
