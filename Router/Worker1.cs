using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Worker1 : WorkerBase
    {
        public Worker1(IDispatcher<ConsumerBase, EvtMsgBase> router)
            :base(router)
        { }

        [ExpectReqMsg(typeof(ReqMsg2))]
        public void HandleReqMsg2(ReqMsg2 msg)
        {
            Console.WriteLine("Worker1 is handling ReqMsg2 {0}", msg.Para2);
            myRouter.DispatchMsg(new EvtMsg2() { Para4="Praha" });
        }
    }
}
