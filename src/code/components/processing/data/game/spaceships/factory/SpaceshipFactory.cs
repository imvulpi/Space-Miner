using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships.factory
{
    public class SpaceshipFactory : ISpaceshipFactory, ISpaceshipRegistry
    {
        public Dictionary<string, ISpaceshipFactory> SpaceshipFactories { get; set; }

        public Spaceship GetSpaceship(Spaceship spaceship)
        {
            string[] idParts = spaceship.ID.Split('.', 1);
            if (SpaceshipFactories.TryGetValue(idParts[0], out ISpaceshipFactory factory))
            {
                return factory.GetSpaceship(spaceship);
            }
            else if (SpaceshipFactories.TryGetValue(spaceship.ID, out ISpaceshipFactory specificFactory))
            {
                return specificFactory.GetSpaceship(spaceship);
            }
            else
            {
                throw new GameException(PrettyErrorType.ResourceNotFound, "Spaceship factory", "There is no spaceship factory registered for this ID.")
            }
        }

        public bool DeregisterBlockFactory(string domain)
        {
            if (SpaceshipFactories.Remove(domain))
            {
                return true;
            }
            return false;
        }

        public ISpaceshipFactory GetBlockFactory(string domain)
        {
            return SpaceshipFactories[domain];
        }

        public ISpaceshipFactory RegisterBlockFactory(string domain, ISpaceshipFactory spaceshipFactory)
        {
            if(SpaceshipFactories.TryAdd(domain, spaceshipFactory))
            {
                return spaceshipFactory;
            }
            else
            {
                return SpaceshipFactories[domain];
            }
        }

        public void SetBlockFactory(string domain, ISpaceshipFactory spaceshipFactory)
        {
            SpaceshipFactories[domain] = spaceshipFactory;
        }
    }
}
