using Godot;

namespace Godot4.Empty.objects.entities.interfaces.velocity
{
    public interface IExternalVelocity2D
    {
        public Vector2 ExternalVelocity { get; set; }
        public void AddVelocity(Vector2 velocity, float timeMs = 0);
        public void AddVelocity(Vector2 velocity);
        public void OverrideVelocity(Vector2 velocity, float timeMs = 0);
        public void OverrideVelocity(Vector2 velocity);
        public void ClearOverride();
    }
}
