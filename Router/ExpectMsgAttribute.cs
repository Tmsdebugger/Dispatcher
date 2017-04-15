using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExpectMsgAttribute :Attribute
    {
    }
}
