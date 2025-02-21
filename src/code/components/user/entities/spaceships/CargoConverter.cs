using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.user.entities.asteroids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.spaceships
{
    class CargoConverter
    {
        public CargoType AsteroidToCargo(AsteroidType asteroidType)
        {
            return asteroidType switch
            {
                AsteroidType.Rocky => CargoType.Rock,
                AsteroidType.Bronze => CargoType.Bronze,
                AsteroidType.Silver => CargoType.Silver,
                AsteroidType.Gold => CargoType.Gold,
                _ => throw new GameException(commons.errors.PrettyErrorType.Invalid, "Asteroid type not implemented", "This asteroid type was not implemented in conversion to cargo"),
            };
        }
    }
}
