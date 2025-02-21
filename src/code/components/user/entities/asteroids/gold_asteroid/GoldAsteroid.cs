using Godot;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.user.entities.asteroids;
using SpaceMiner.src.code.components.user.entities.entities;
using System;

public partial class GoldAsteroid : AsteroidEntity, IAsteroidWear
{
    public int Durability { get; set; } = 50000;
    public Action AsteroidDestroyed { get; set; }
    public override AsteroidType AsteroidType { get; set; } = AsteroidType.Gold;
    public override GoldAsteroid CreateScene()
    {
        PackedScene packedScene = ResourceLoader.Load<PackedScene>(InternalPaths.GOLD_ASTEROID);
        return packedScene.Instantiate<GoldAsteroid>();
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
