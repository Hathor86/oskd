using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNATools.Managers;

namespace XNATools.Services.Interfaces
{
    /// <summary>
    /// Interface for all services
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Gets a bool that define if this IService exists in the
        /// service manager
        /// </summary>
        bool ExistInManager { get; }
    }
}
