using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class WorkerBase
    {
        protected IDispatcher<ConsumerBase, EvtMsgBase> myRouter;
        public WorkerBase(IDispatcher<ConsumerBase, EvtMsgBase> router)
        {
            myRouter = router;
        }
    }
}
