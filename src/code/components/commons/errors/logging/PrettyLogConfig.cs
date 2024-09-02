using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.errors.logging
{
    public struct PrettyLogConfig
    {
        public PrettyLogConfig(bool includeDate)
        {
            this.includeDate = includeDate;
        }
        
        public bool includeDate;

    }
}
