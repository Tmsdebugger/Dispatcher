using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Consumer2: ConsumerBase
    {
        public Consumer2(IDispatcher<WorkerBase, ReqMsgBase> router)
            :base(router)
        { }

        [ExpectEvtMsg(typeof(EvtMsg1))]
        public void HandleEvtMsg1(EvtMsg1 msg)
        {
            Console.WriteLine("Consumer2 is handling EvtMsg1 {0}", msg.Para3);
        }

        public void InvokeReq1()
        {
            Console.WriteLine("Consumer2 is invoking ReqMsg1");
            myRouter.DispatchMsg(new ReqMsg1() { Para1="Konigsee" });
        }
    }
}
