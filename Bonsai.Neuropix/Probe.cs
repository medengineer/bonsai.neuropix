using OpenCV.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NeuropixPXI.Native;
using System.Runtime.InteropServices;

namespace Bonsai.NeuropixPXI
{
    public class Probe : Source<NeuropixDataFrame>
    {
        public Probe() {
            Console.WriteLine("Constructor was called...");
        }

        public override IObservable<NeuropixDataFrame> Generate()
        {
            return Observable.Create<NeuropixDataFrame>((observer, cancellationToken) =>
            {
                return Task.Factory.StartNew(() =>
                {
                    try
                    {
                        int rc = NeuropixAPI.openBS(3);
                        Console.WriteLine("Open basestation result: {0}", rc);
                        rc = NeuropixAPI.openProbe(3, 1);
                        Console.WriteLine("Open probe result: {0}", rc);
                        if (rc == 0)
                        {
                            rc = NeuropixAPI.init(3, 1);
                            Console.WriteLine("Init result: {0}", rc);
                            Console.WriteLine("Sleeping for {0} seconds", 2);
                            rc = NeuropixAPI.setTriggerInput(3, 0);
                            rc = NeuropixAPI.arm(3);
                            rc = NeuropixAPI.setSWTrigger(3);
                            int packetCounter = 0;
                            float bufferCapacity = 0;
                            var packets = new ElectrodePacket[1];
                            IntPtr requestedAmount = (IntPtr)packets.Length;
                            IntPtr actualAmount;

                            while (!cancellationToken.IsCancellationRequested)
                            {
                                rc = NeuropixAPI.readElectrodeData(3, 1, packets, out actualAmount, requestedAmount);
                                Console.WriteLine("Received: {0} samples", actualAmount);
                                //observer.OnNext(new NeuropixDataFrame(packets, 0));
                                for (int i = 0; i < actualAmount.ToInt32(); i++)
                                {
                                    observer.OnNext(new NeuropixDataFrame(packets[i], 0));
                                }
                            }
                            Console.WriteLine("Closing basestation...");
                            rc = NeuropixAPI.closeBS(3);
                            Console.WriteLine("CloseBS result: {0}", rc);
                        }
                    }
                    catch (System.Runtime.InteropServices.SEHException ex)
                    {
                        Console.WriteLine(ex);
                        throw;
                    }
                },
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
            });
        }
    }
}
