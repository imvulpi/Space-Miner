using Godot;
using SpaceMiner.src.code.components.user.entities.asteroids;

namespace SpaceMiner.src.code.components.user.entities.entities
{
    public partial class AsteroidEntity : Node2D, IAsteroidEntity, ISceneCreateable<AsteroidEntity>
    {
        [Export] public RigidBody2D RigidBody {  get; set; }
        [Export] public Sprite2D Sprite { get; set; }
        [Export] public float MovementSpeed { get; set; }
        [Export] public int RotationSpeed { get; set; }
        public virtual AsteroidType AsteroidType { get; set; }
        public virtual void Move()
        {
             RigidBody.LinearVelocity = new Vector2(1, 1);
        }

        public virtual void Rotate()
        {
            RigidBody.AngularVelocity = 5f;
        }

        public virtual AsteroidEntity CreateScene()
        {
            PackedScene packedScene = ResourceLoader.Load<PackedScene>("res://objects/entities/implementations/asteroid/AsteroidEntity.tscn");
            return packedScene.Instantiate<AsteroidEntity>();
        }
    }
}
