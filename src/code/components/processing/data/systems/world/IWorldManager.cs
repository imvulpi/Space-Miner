using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.systems.world
{
    interface IWorldManager
    {
        public void Initialize();
        public void Manage();
    }
}
