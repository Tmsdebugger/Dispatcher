using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, this is a router.");

            IDispatcher<ConsumerBase,EvtMsgBase> evtMsgRouter = new Dispatcher<ConsumerBase, EvtMsgBase>();
            WorkerBase worker1 = new Worker1(evtMsgRouter);
            WorkerBase worker2 = new Worker2(evtMsgRouter);

            IDispatcher<WorkerBase, ReqMsgBase> reqMsgRouter = new Dispatcher<WorkerBase, ReqMsgBase>();
            Consumer1 consumer1 = new Consumer1(reqMsgRouter);
            Consumer2 consumer2 = new Consumer2(reqMsgRouter);

            evtMsgRouter.RegisterTarget(consumer1);
            evtMsgRouter.RegisterTarget(consumer2);
            reqMsgRouter.RegisterTarget(worker1);
            reqMsgRouter.RegisterTarget(worker2);

            consumer1.InvokeReq2();
            consumer2.InvokeReq1();

            consumer1.InvokeReq2();
            consumer2.InvokeReq1();

            Console.ReadLine();
        }
    }
}
