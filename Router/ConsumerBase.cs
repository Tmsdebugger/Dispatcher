using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    abstract class ConsumerBase
    {
        protected IDispatcher<WorkerBase, ReqMsgBase> myRouter;
        public ConsumerBase(IDispatcher<WorkerBase, ReqMsgBase> router)
        {
            myRouter = router;
        }
    }
}
