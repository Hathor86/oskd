using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Services.Interfaces
{
    /// <summary>
    /// Interface for all services who wants to be InputService
    /// </summary>
    public interface IInputService:IService
    {
        /// <summary>
        /// Gets a bool that define if state has change since last update
        /// </summary>
        bool StateHasChanged { get; }
    }
}
