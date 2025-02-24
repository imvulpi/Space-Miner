using Godot;
using SpaceMiner.src.code.components.processing.data._base;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.entities.implementations.player;
using SpaceMiner.src.code.components.user.special.player;
using System;

namespace SpaceMiner.src.code.components.processing.world.entities.player
{
    public class PlayerEntity : IPlayerEntity
    {
        public IPlayerData PlayerData { get; set; }
        public Node ConnectToNode { get; set; }
        public Node PlayerNode { get; set; }
        public Node MovementNode { get; set; }
        public void Initialize()
        {
            PlayerNode ??= new Node2D() { Name = "Player" };
            PlayerNode.AddChild(MovementNode);
            ConnectToNode.AddChild(PlayerNode);
            if(MovementNode is ISpaceshipData data)
            {
                data.BoundToEntity = this;
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
