using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships
{
    public interface ISpaceshipData
    {
        public string SpaceshipName { get; set; }
        public float MaxFuelCapacity { get; set; }
        public float MaxSpeed { get; set; }
        public float Acceleration { get; set; }
        public float RotationSpeed { get; set; }
    }
}
