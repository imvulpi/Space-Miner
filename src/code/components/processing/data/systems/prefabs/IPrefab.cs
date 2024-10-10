using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.systems.prefabs
{
    public interface IPrefab<T> where T : class
    {
        public T CreatePrefab();
    }
}
