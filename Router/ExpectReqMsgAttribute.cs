using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    public class ExpectReqMsgAttribute : ExpectMsgAttribute
    {
        public Type ExpectMsgType { get; set; }

        public ExpectReqMsgAttribute(Type type)
        {
            if (type != typeof(ReqMsgBase))
                throw new ArgumentException("Excpect a ReqMsg here.");

            ExpectMsgType = type;
        }
    }
}

