using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships.factory
{
    public interface ISpaceshipFactory
    {
        public Spaceship GetSpaceship(Spaceship spaceship);
    }
}
