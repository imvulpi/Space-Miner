using Godot;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks;

public partial class BarrierChunk : Node2D
{
    [Export] public StaticBody2D StaticBody2D;
    [Export] public CollisionShape2D StaticCollisionShape;
    public override void _Ready()
    {
        StaticBody2D.Position = new Vector2(ChunkConstants.CHUNK_SIZE / 2, ChunkConstants.CHUNK_SIZE / 2);
        Vector2 staticSize = new(ChunkConstants.CHUNK_SIZE, ChunkConstants.CHUNK_SIZE);
        RectangleShape2D staticRectangle = new();
        staticRectangle.Size = staticSize;
        StaticCollisionShape.Shape = staticRectangle;
    }
}
