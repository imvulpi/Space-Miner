using Godot;

namespace Godot4.Empty.prefabs.bounce_surface
{
    public partial class BouncyAreaController : Area2D
    {
        [Export] public float speedModifier = 1.0f;
        [Export] public float bounceTimeMs = 1000;
        public override void _Ready()
        {
            BodyEntered += BouncyArea_BodyEntered;

        }
        private void BouncyArea_BodyEntered(Node2D body)
        {
            Vector2 velocity = body.Get(CharacterBody2D.PropertyName.Velocity).AsVector2();
            if (velocity == Vector2.Zero)
            {
                velocity = body.Get(RigidBody2D.PropertyName.LinearVelocity).AsVector2();
            }
            Vector2 bounceVelocity = new Vector2();
            
            // -2 is there to substract and finally negate the vector which causes it to reflect the value creating a bounce.
            // It's basically velocity.X - 2 * dot product * cos/sin of rotation (cos for x, sin for y)
            // The dot product is multipled by cos/sin of rotation to vector value in x/y axis
            bounceVelocity.X = velocity.X - 2 * ((velocity.X * Mathf.Cos(Rotation) + velocity.Y * Mathf.Sin(Rotation)) * Mathf.Cos(Rotation));
            bounceVelocity.Y = velocity.Y - 2 * ((velocity.X * Mathf.Cos(Rotation) + velocity.Y * Mathf.Sin(Rotation)) * Mathf.Sin(Rotation));
            bounceVelocity *= speedModifier;

            // TODO: BELOW COMMENTED CODE IS STILL EXPERIMENTAL AND NOT YET IMPLEMENTED
/*            if (body is IExternalVelocityEntity2D externalVelocity)
            {
                externalVelocity.OverrideVelocity(bounceVelocity, bounceTimeMs);
            }
            else
            {
                body.Set("velocity", bounceVelocity);
                body.Set("linear_velocity", bounceVelocity);
            }*/
        }
    }
}
