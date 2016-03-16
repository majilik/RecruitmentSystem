using NLog;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using RecruitmentSystem.Logging;
using System;
using System.Reflection;

namespace RecruitmentSystem.Attributes
{
    /// <summary>
    /// Represents the logic used to trace method invocations. The attribute
    /// can be registered globally in <see name="AssemblyInfo"/> or applied to
    /// specific classes or methods. Targets that this attribute should not
    /// enhance should be decorated with [TraceLogger(AttributeExclude = true)].
    /// </summary>
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    [Serializable]
    public class TraceLogger : MethodInterceptionAspect
    {
        /// <summary>
        /// Invoked when a target (assembly, class or method) contains a method which has been invoked.
        /// Arguments and return values are retrieved via reflection, making it inefficient, so this
        /// should not be applied in production code except when debugging. Any method parameters and
        /// return values are logged with <see cref="EventLogger"/>, which is thread-safe, so an event-id
        /// is written for each method invocation to easily be able to distinguish between events.
        /// </summary>
        /// <param name="args">The arguments of advices of aspect type <see cref="MethodInterceptionAspect"/>.</param>
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            MethodInfo methodInfo = args.Method as MethodInfo;

            if (methodInfo != null && args != null)
            {
                string loggerName = methodInfo.ReflectedType.FullName;

                Arguments arguments = args.Arguments;
                ParameterInfo[] parameters = methodInfo.GetParameters();
                string eventID = Guid.NewGuid().ToString();

                for (int i = 0; i < args.Arguments.Count; i++)
                {
                    EventLogger.Log(loggerName, LogLevel.Trace, eventID, parameters[i] + " " + arguments[i]);
                }

                if (methodInfo.ReturnType != typeof(void))
                {
                    EventLogger.Log(loggerName, LogLevel.Trace, eventID, args.ReturnValue.GetType().FullName + " " +
                        args.ReturnValue == null ? "null" : args.ReturnValue.ToString());
                }
            }

            base.OnInvoke(args);
        }
    }
}