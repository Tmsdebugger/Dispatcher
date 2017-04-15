using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    public class MsgBase
    { }

    class ReqMsgBase : MsgBase
    {
        
    }

    public class EvtMsgBase : MsgBase
    {
        
    }

    class ReqMsg1 : ReqMsgBase
    {
        public string Para1 { get; set; }
    }

    class ReqMsg2:ReqMsgBase
    {
        public string Para2 { get; set; }
    }

    class EvtMsg1: EvtMsgBase
    {
        public string Para3 { get; set; }
    }

    class EvtMsg2 : EvtMsgBase
    {
        public string Para4 { get; set; }
    }
}
