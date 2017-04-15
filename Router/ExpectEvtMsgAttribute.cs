using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExpectEvtMsgAttribute : Attribute
    {
        public Type ExpectMsgType { get; set; }

        public ExpectEvtMsgAttribute(Type type)
        {
            if (type != typeof(EvtMsgBase))
                throw new ArgumentException("Excpect a ReqMsg here.");

            ExpectMsgType = type;
        }
    }
}

