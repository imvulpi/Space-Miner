using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities
{
    public interface ISceneCreateable<T> where T : class
    {
        T CreateScene();
    }
}
