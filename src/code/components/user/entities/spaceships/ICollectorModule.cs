using Godot;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.user.entities.asteroids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.spaceships
{
    public interface ICollectorModule
    {
        public SpaceshipHUD SpaceshipHUD { get; set; }
        public Area2D ScanArea { get; set; }
        public AsteroidType[] CollectTypes { get; set; }
        public void Initialize();
    }
}
