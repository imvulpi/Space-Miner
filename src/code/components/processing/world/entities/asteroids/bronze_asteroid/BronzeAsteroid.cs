using Godot;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.entities.asteroids;
using System;

namespace SpaceMiner.src.code.components.user.entities.entities.implementations.asteroid
{
    public partial class BronzeAsteroid : AsteroidEntity, IAsteroidWear
    {
        public Action AsteroidDestroyed { get; set; }
        public int Durability { get; set; } = 10000;
        public override AsteroidType AsteroidType { get; set; } = AsteroidType.Bronze;
        public override BronzeAsteroid CreateScene()
        {
            PackedScene packedScene = ResourceLoader.Load<PackedScene>(InternalPaths.BRONZE_ASTEROID);
            return packedScene.Instantiate<BronzeAsteroid>();
        }

        public void Hit(int points)
        {
            Durability -= points;
            if (Durability <= 0)
            {
                AsteroidDestroyed?.Invoke();
                GetParent().RemoveChild(this);
                QueueFree();
            }
        }
    }
}