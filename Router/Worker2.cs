using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Worker2:WorkerBase
    {
        public Worker2(IDispatcher<ConsumerBase, EvtMsgBase> router)
            :base(router)
        { }

        [ExpectReqMsg(typeof(ReqMsg1))]
        public void HandleReqMsg1(ReqMsg1 msg)
        {
            Console.WriteLine("Worker2 is handling ReqMsg1 {0}", msg.Para1);
            myRouter.DispatchMsg(new EvtMsg1() { Para3="Veniza" });
        }
    }
}
