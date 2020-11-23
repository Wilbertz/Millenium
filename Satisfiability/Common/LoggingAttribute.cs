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
            if (!(args.Method.DeclaringType is null))
            {
                Logger.Info(
                    $"Init: {args.Method.DeclaringType.FullName}.{args.Method.Name} [{args.Arguments.Length}] params");
            }
            foreach (var item in args.Method.GetParameters())
            {
                Logger.Debug($"{item.Name}: {args.Arguments[item.Position]}");
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
                Logger.Info($"Exit: [{args.ReturnValue}]");
            }
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Logger.Error($"OnException: {args.Exception.GetType()}: {args.Exception.Message}");
        }
    }
}
