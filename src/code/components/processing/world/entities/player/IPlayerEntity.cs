using Godot;
using Godot4.Empty.objects.entities.interfaces.velocity;
using SpaceMiner.src.code.components.processing.entities.interfaces;
using SpaceMiner.src.code.components.user.special.player;

namespace SpaceMiner.src.code.components.processing.entities.implementations.player
{
    public interface IPlayerEntity
    {
        public IPlayerData PlayerData { get; set; }
        public Node PlayerNode { get; set; }
        public Node MovementNode { get; set; }
        public void Initialize();
        public void Update();
    }
}
