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
                var targetMethods = GetExpectMethodInfo(msgType);

                if (targetMethods.Count == 0)
                {
                    Console.WriteLine("Error: The msg {0} cannot be dispatched to any handlers, please check", msgType.Name);
                }
                else if (targetMethods.Count > 1)
                {
                    Console.WriteLine("Error: Find more than one handler for msg {0}, please check", msgType.Name);
                }
                else
                {
                    targetMethod = targetMethods.Single();
                    msgHandlerCache.Add(msgType, targetMethod);
                }
            }

            if (targetMethod != null)
            {
                targetMethod.Invoke(targetLookup[targetMethod.ReflectedType], new object[] { msg });
                perfWatch.Stop();
                Console.WriteLine("Dispatch the message {0} cost {1} ms", msgType.Name, 1000.0 * (double)perfWatch.ElapsedTicks / Stopwatch.Frequency);
            }
        }

        private Stopwatch perfWatch = new Stopwatch();

        private IList<MethodInfo> GetExpectMethodInfo(Type msgType)
        {
            List<MethodInfo> targetMethods = new List<MethodInfo>();
            MethodInfo targetMethod = null;
            foreach (var workerType in targetLookup.Keys)
            {
                try
                {
                    targetMethod = workerType.GetMethods(BindingFlags.Public | BindingFlags.Instance).SingleOrDefault(method =>
                    method.CustomAttributes.Any(ca => ca.AttributeType.BaseType == typeof(ExpectMsgAttribute)
                                            && ca.ConstructorArguments.Any(a => (Type)a.Value == msgType)));
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("There are more than one handlers in one instance of {0} for msg {1}, please check", workerType.Name, msgType.Name);
                }
                if (targetMethod != null)
                {
                    Console.WriteLine("find the matching methods for msg {0} in {1}", msgType, workerType.Name);
                    targetMethods.Add(targetMethod);
                }
            }

            return targetMethods;
        }
    }

    interface IDispatcher<T1, T2>
    {
        void RegisterTarget(T1 target);

        void DispatchMsg(T2 msg);
    }
}
