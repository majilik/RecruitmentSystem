using NLog;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using RecruitmentSystem.Logging;
using System;
using System.Reflection;

namespace RecruitmentSystem.Attributes
{
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    [Serializable]
    public class TraceLogger : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            MethodInfo methodInfo = args.Method as MethodInfo;

            if (methodInfo != null && args != null)
            {
                EventLogger logger = new EventLogger(methodInfo.ReflectedType.FullName);

                Arguments arguments = args.Arguments;
                ParameterInfo[] parameters = methodInfo.GetParameters();
                string eventID = Guid.NewGuid().ToString();

                for (int i = 0; i < args.Arguments.Count; i++)
                {
                    logger.Log(LogLevel.Trace, eventID, parameters[i] + " " + arguments[i]);
                }

                if (methodInfo.ReturnType != typeof(void) && args.ReturnValue != null)
                {
                    logger.Log(LogLevel.Trace, eventID, args.ReturnValue.ToString());
                }
            }

            base.OnInvoke(args);
        }
    }
}