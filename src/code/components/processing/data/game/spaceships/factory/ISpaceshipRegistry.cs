using SpaceMiner.src.code.components.user.blocks.core.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.game.spaceships.factory
{
    public interface ISpaceshipRegistry
    {
        Dictionary<string, ISpaceshipFactory> SpaceshipFactories { get; set; }
        public ISpaceshipFactory RegisterBlockFactory(string domain, ISpaceshipFactory spaceshipFactory);
        public bool DeregisterBlockFactory(string domain);
        public ISpaceshipFactory GetBlockFactory(string domain);
        public void SetBlockFactory(string domain, ISpaceshipFactory spaceshipFactory);
    }
}
