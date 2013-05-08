using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNATools.Attributes
{
    /// <summary>
    /// Define a code name for an assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]    
    public class AssemblyCodeNameAttribute:Attribute
    {
        #region Fields

        private string _codeName;

        #endregion

        #region cTor(s)

        public AssemblyCodeNameAttribute(string codeName)
        {
            _codeName = codeName;
        }

        public AssemblyCodeNameAttribute()
            : this(string.Empty)
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the codename given to the assembly
        /// </summary>
        public string CodeName
        {
            get
            {
                return _codeName;
            }
        }

        #endregion
    }
}
