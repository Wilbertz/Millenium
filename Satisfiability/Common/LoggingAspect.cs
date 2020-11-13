using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

using NLog;

namespace Satisfiability.Common
{
    /// <summary>
    /// This class is a helper class used to add logging 
    /// functionality in an unobtrusive way
    /// </summary>
    public class LoggingAspect : IMessageSink
    {
        #region Fields and Properties -----------------------------------------

        private readonly IMessageSink m_next;

        private readonly Logger _logger;

        #endregion

        #region Constructors --------------------------------------------------

        /// <summary>
        /// In order to support chaining of message processing, the next message 
        /// sink in the chain is stored in a local member variable.
        /// </summary>
        /// <param name="next"></param>
        internal LoggingAspect(IMessageSink next)
        {
            _logger = LogManager.GetCurrentClassLogger();

            m_next = next;
        }

        #endregion

        #region Interface IMessageSink ----------------------------------------

        /// <summary>
        /// Get the next sink within the chain of message sinks.
        /// </summary>
        public IMessageSink NextSink => m_next;

        /// <summary>
        /// The processing of synchronous messages is intercepted. Dedicated code 
        /// is run before and after each synchronous message call.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public IMessage SyncProcessMessage(IMessage msg)
        {
            Preprocess(msg);
                IMessage returnMessage = m_next.SyncProcessMessage(msg);
            Postprocess(returnMessage);

            return returnMessage;
        }

        /// <summary>
        /// There is currently no support for asynchronous message processing.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="replySink"></param>
        /// <returns></returns>
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new InvalidOperationException();
        }

        #endregion

        #region Helpers -------------------------------------------------------

        /// <summary>
        /// This method ic called before processing any method call.
        /// </summary>
        /// <param name="msg"></param>
        private void Preprocess(IMessage msg)
        {
            // We only want to process method calls
            if (!(msg is IMethodMessage))
            {
                return;
            }

            IMethodMessage call = msg as IMethodMessage;
            Type type = Type.GetType(call.TypeName);
            if (!(type is null))
            {
                string callStr = type.Name + "." + call.MethodName;

                _logger.Debug($"Entry: {callStr}");

                for (int i = 0; i < call.ArgCount; i++)
                {
                    _logger.Trace($"{callStr}, Argument: {call.GetArgName(i)}, Value: {call.GetArg(i)}");
                }
            }
        }

        /// <summary>
        /// This method is called after any method call.
        /// </summary>
        /// <param name="msg"></param>
        private void Postprocess(IMessage msg)
        {
            // We only want to process method calls
            if (!(msg is IMethodReturnMessage))
            {
                return;
            }

            IMethodReturnMessage call = (IMethodReturnMessage) msg;
            Type type = Type.GetType(call.TypeName);
            if (!(type is null))
            {
                string callStr = type.Name + "." + call.MethodName;

                _logger.Debug($"Exit : {callStr}, ReturnValue: {call.ReturnValue}");

                var task = call.ReturnValue as Task;
                Exception ex = call.Exception;
                if (ex == null)
                {
                    // In case there is an asynchronous call, we have to check the task.
                    if (task?.Exception?.InnerException != null)
                    {
                        ex = task?.Exception?.InnerException;
                    }
                    else
                    {
                        ex = task?.Exception;
                    }
                }
                if (ex != null)
                {
                    _logger.Error($"{callStr}, Exception: {ex}");
                }
            }
        }

        #endregion
    }
}
