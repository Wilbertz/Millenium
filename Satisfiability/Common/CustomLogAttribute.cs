using System.Threading.Tasks;

using MethodBoundaryAspect.Fody.Attributes;
using NLog.Fluent;

namespace Satisfiability.Common
{
    public sealed class LoggingAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Log.Info($"Init: {args.Method.DeclaringType.FullName}.{args.Method.Name} [{args.Arguments.Length}] params");
            foreach (var item in args.Method.GetParameters())
            {
                Log.Debug($"{item.Name}: {args.Arguments[item.Position]}");
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (args.ReturnValue is Task t)
            {
                t.ContinueWith(task => Log.Info($"Exit: [{args.ReturnValue}]"));
            }
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Log.Error($"OnException: {args.Exception.GetType()}: {args.Exception.Message}");
        }
    }
}
