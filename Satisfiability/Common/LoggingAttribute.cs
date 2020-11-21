using System.Threading.Tasks;

using MethodBoundaryAspect.Fody.Attributes;
using NLog;

namespace Satisfiability.Common
{
    public sealed class LoggingAttribute : OnMethodBoundaryAspect
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public override void OnEntry(MethodExecutionArgs args)
        {
            _logger.Info($"Init: {args.Method.DeclaringType.FullName}.{args.Method.Name} [{args.Arguments.Length}] params");
            foreach (var item in args.Method.GetParameters())
            {
                _logger.Debug($"{item.Name}: {args.Arguments[item.Position]}");
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (args.ReturnValue is Task t)
            {
                t.ContinueWith(task => _logger.Info($"Exit: [{args.ReturnValue}]"));
            }
        }

        public override void OnException(MethodExecutionArgs args)
        {
            _logger.Error($"OnException: {args.Exception.GetType()}: {args.Exception.Message}");
        }
    }
}
