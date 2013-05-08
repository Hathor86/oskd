using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Exceptions
{
    /// <summary>
    /// Class for holding exception of type CallBeforeLoadContentException
    /// Raised when trying to set a property before methods LoadContent as been called
    /// </summary>
    public sealed class CallBeforeLoadContentException:Exception
    {
        /// <summary>
        /// new CallBeforeLoadContentException
        /// </summary>
        public CallBeforeLoadContentException(string propertyName)
            :base(string.Format("{0} cannot be set before LoadContent() is called",propertyName))
        {
            
        }
    }
}
