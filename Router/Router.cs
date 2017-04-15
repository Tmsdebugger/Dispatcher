using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Router
{
    class Dispatcher<T1, T2> : IDispatcher<T1, T2>
    {
        private Dictionary<Type, T1> targetLookup = new Dictionary<Type, T1>();

        private Dictionary<Type, MethodInfo> msgHandlerCache = new Dictionary<Type, MethodInfo>();

        //how to create instances?
        public void RegisterTarget(T1 target)
        {
            targetLookup.Add(target.GetType(), target);
        }

        public void DispatchMsg(T2 msg)
        {
            perfWatch.Reset();
            perfWatch.Start();
            MethodInfo targetMethod = null;
            var msgType = msg.GetType();
            if (msgHandlerCache.Keys.Contains(msgType))
            {
                Console.WriteLine("Get the MsgHandler for {0} from the Cache.", msgType);
                targetMethod = msgHandlerCache[msgType];
            }
            else
            {
                //TODO:how to implement a cache?
                foreach (var workerType in targetLookup.Keys)
                {
                    foreach (var method in workerType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (method.CustomAttributes.Any(ca => ca.ConstructorArguments.Any(a => (Type)a.Value == msgType)))
                        {
                            Console.WriteLine("find the matching attributes for request msg {0}", msgType);
                            msgHandlerCache.Add(msgType, method);
                            targetMethod = method;
                            //method.Invoke(workerLookup[workerType], new object[] { msg });
                        }
                    }
                }
            }

            if (targetMethod != null)
            {

                targetMethod.Invoke(targetLookup[targetMethod.ReflectedType], new object[] { msg });
            }
            //Thread.Sleep(10);
            perfWatch.Stop();
            Console.WriteLine("Dispatch the message cost {0} ms", 1000.0 * (double)perfWatch.ElapsedTicks/Stopwatch.Frequency);
        }

        private Stopwatch perfWatch = new Stopwatch();
    }

    interface IDispatcher<T1, T2>
    {
        void RegisterTarget(T1 target);

        void DispatchMsg(T2 msg);
    }
}
