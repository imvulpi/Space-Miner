using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths.spaceships_prefabs;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.data.game.spaceships.factory;
using SpaceMiner.src.code.components.processing.data.game.spaceships.prospector;

namespace SpaceMiner.src.code.components.user.entities.spaceships.prospector
{
    public class ProspectorSpaceshipFactory : ISpaceshipFactory
    {
        public Spaceship GetSpaceship(Spaceship spaceship)
        {
            if(spaceship is ProspectorSpaceship spaceshipData)
            {
                ProspectorSpaceship prospectorSpaceship = ResourceLoader.Load<PackedScene>(SpaceshipPaths.PROSPECTOR_SPACESHIP).Instantiate<ProspectorSpaceship>();
                prospectorSpaceship.CurrentCargo = spaceshipData.CurrentCargo;
                return prospectorSpaceship;
            }
            else
            {
                throw new GameException(PrettyErrorType.Invalid, "Wrong Spaceship", $"This factory only supports certain spaceship types and {spaceship.GetType()} type is not supported.");
            }
        }
    }
}
