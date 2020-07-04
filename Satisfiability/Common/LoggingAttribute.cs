using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;

namespace Satisfiability.Common
{
    /// <summary>
    /// This attribute is used to add logging functionality in an 
    /// unobtrusive way. The class decorated with this attribute has 
    /// to be derived from a context bound object. Since this attribute 
    /// is a context attribute, the context properties have to be added 
    /// during a call to GetPropertiesForNewContext.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LoggingAttribute : ContextAttribute
    {
        /// <summary>
        /// Default constructor, delegates work to base class implementation.
        /// </summary>
        public LoggingAttribute() : base("Logging")
        {
        }

        public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
        {
            ccm.ContextProperties.Add(new LoggingProperty());
        }

        public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
        {
            return false;
        }
    }
}
