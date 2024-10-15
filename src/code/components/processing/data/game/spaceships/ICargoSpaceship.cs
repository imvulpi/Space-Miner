using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships
{
    public interface ICargoSpaceship
    {
        public float CargoCapacity { get; set; }
        public float LoadSpeed { get; set; }
    }
}
