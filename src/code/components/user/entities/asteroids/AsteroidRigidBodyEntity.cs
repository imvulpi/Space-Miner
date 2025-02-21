using Godot;
using SpaceMiner.src.code.components.processing.entities.interfaces.velocity;
using SpaceMiner.src.code.components.user.entities.asteroids;

namespace SpaceMiner.src.code.components.user.entities.entities
{
    public partial class AsteroidRigidBodyEntity : RigidBody2D, IAsteroidEntity, IVelocityEntity
    {
        [Export] public Sprite2D Sprite { get; set; }
        [Export] public float MovementSpeed { get; set; }
        [Export] public int RotationSpeed { get; set; }
        public AsteroidType AsteroidType { get; set; }
        public Vector2 Velocity { get { return LinearVelocity; } set { LinearVelocity = value; } }
        public virtual void Move()
        {
            LinearVelocity = new Vector2(1, 1);
        }

        public virtual void Rotate()
        {
            AngularVelocity = 5f;
        }
    }
}
