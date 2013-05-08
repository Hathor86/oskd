using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Exceptions
{
    /// <summary>
    /// Class for holding ServiceMissingException
    /// This occured when a service has not been initialized
    /// </summary>
    public sealed class ServiceMissingException:Exception
    {
        #region Fields
        #endregion

        #region cTor(s)

        /// <summary>
        /// New ServiceMissingException
        /// </summary>
        /// <param name="serviceName">Service who's missing</param>
        public ServiceMissingException(string serviceName)
            :base(string.Format("{0} has not been initialized",serviceName))
        {}

        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion
    }
}
