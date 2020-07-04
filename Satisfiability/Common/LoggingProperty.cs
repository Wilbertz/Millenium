using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;

namespace Satisfiability.Common
{
    public class LoggingProperty : IContextProperty, IContributeObjectSink
    {
        public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
        {
            return new LoggingAspect(next);
        }

        public string Name => "LoggingProperty";

        public void Freeze(Context newContext)
        {
            // Nothing to do.
        }

        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }
    }
}
