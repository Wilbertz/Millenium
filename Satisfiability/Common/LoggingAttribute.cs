using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using MethodBoundaryAspect.Fody.Attributes;
using NLog;

namespace Satisfiability.Common
{
    public sealed class LoggingAttribute : OnMethodBoundaryAspect
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (args.MethodExecutionTag == null)
            {
                args.MethodExecutionTag = 0;
            }
            else
            {
                args.MethodExecutionTag = (int)args.MethodExecutionTag + 1;
            }

            if (!(args.Method.DeclaringType is null))
            {
                Logger.Info(
                    $"{GetIndentation((int)args.MethodExecutionTag)}Init: {args.Method.DeclaringType.FullName}.{args.Method.Name} [{args.Arguments.Length}] params");
            }
            foreach (var item in args.Method.GetParameters())
            {
                Logger.Debug($"{GetIndentation((int)args.MethodExecutionTag)}{item.Name}: {args.Arguments[item.Position]}");
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (args.ReturnValue is Task t)
            {
                t.ContinueWith(task =>
                {
                    if (task.GetType().IsGenericType)
                    {
                        dynamic taskWithResult = task;
                        Logger.Info($"Exit: [{taskWithResult.Result}]");
                    }
                    else
                    {
                        Logger.Info($"Exit: []");
                    }
                });
            }
            else
            {
                Logger.Info($"{GetIndentation((int)args.MethodExecutionTag)}Exit: [{args.ReturnValue}]");
            }
            args.MethodExecutionTag = (int)args.MethodExecutionTag - 1;
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Logger.Error($"OnException: {args.Exception.GetType()}: {args.Exception.Message}");
        }

        private string GetIndentation(int count)
        {
            return String.Concat(Enumerable.Repeat("   ", count));
        }
    }
}
