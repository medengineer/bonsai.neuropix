using OpenCV.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuropixPXI.Native;

namespace Bonsai.NeuropixPXI
{
    public class Probe : Source<NeuropixDataFrame>
    {
        public Probe() {}

        public override IObservable<NeuropixDataFrame> Generate()
        {
            return Observable.Create<NeuropixDataFrame>((observer, cancellationToken) =>
            {
                return Task.Factory.StartNew(() =>
                {
                    try
                    {
                        int result = NeuropixAPI.openBS(0);

                        Console.WriteLine("Testing!!!!!!");
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
