using Godot;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.entities.asteroids;
using SpaceMiner.src.code.components.user.entities.entities;
using System;

public partial class RockyAsteroid : AsteroidEntity, IAsteroidWear
{
    public Action AsteroidDestroyed { get; set; }
    public int Durability { get; set; } = 5000;
    public override AsteroidType AsteroidType { get; set; } = AsteroidType.Rocky;
    public override RockyAsteroid CreateScene()
    {
        PackedScene packedScene = ResourceLoader.Load<PackedScene>(InternalPaths.ROCKY_ASTEROID);
        return packedScene.Instantiate<RockyAsteroid>();
    }

    public void Hit(int points)
    {
        Durability -= points;
        if (Durability <= 0)
        {
            GD.Print($"Durability: {Durability}");
            AsteroidDestroyed?.Invoke();
            GetParent().RemoveChild(this);
            QueueFree();
        }
    }
}
