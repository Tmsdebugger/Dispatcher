using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Consumer1 : ConsumerBase
    {
        public Consumer1(IDispatcher<WorkerBase,ReqMsgBase> router)
            :base(router)
        { }

        [ExpectEvtMsg(typeof(EvtMsg2))]
        public void HandleEvtMsg2(EvtMsg2 msg)
        {
            Console.WriteLine("Consumer1 is handling EvtMsg2 {0}", msg.Para4);
        }

        public void InvokeReq2()
        {
            Console.WriteLine("Consumer1 is invoking ReqMsg2");
            myRouter.DispatchMsg(new ReqMsg2() { Para2="Rome" });
        }
    }
}
