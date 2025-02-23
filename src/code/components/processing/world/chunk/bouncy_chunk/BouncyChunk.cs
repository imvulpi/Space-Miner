using Godot;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks;

public partial class BouncyChunk : Node2D
{
	[Export] public ColorRect ChunkColorRect;
	[Export] public Area2D BouncyArea2D;
	[Export] public Node2D BouncySurface;
	[Export] public CollisionShape2D BouncyCollisionShape;
	[Export] public CollisionShape2D StaticCollisionShape;
	[Export(PropertyHint.Range, "1, 100")] public float BouncySizePercentage = 6.25f;

    /// <summary>
	/// Sets the chunk rotation, there is only one part that will cause a bounce which is initialy right<br></br>
	/// See below if you need a bounce in different rotations. Help note:<br></br>
    /// 0 - Right<br></br>
    /// 90 - Down<br></br>
    /// 180 - Left<br></br>
    /// 270 - Up<br></br>
    /// </summary>
    [Export(PropertyHint.Enum, "Right:0, Down:90, Left:180, Up:270")] public int ChunkRotation = 0;
	public override void _Ready()
	{
        BouncySurface.Position = new Vector2(ChunkConstants.CHUNK_SIZE/2, ChunkConstants.CHUNK_SIZE/2);
        ChunkColorRect.Size = new Vector2(ChunkConstants.CHUNK_SIZE, ChunkConstants.CHUNK_SIZE);
		Vector2 bouncySize = new(ChunkConstants.CHUNK_SIZE * (BouncySizePercentage/100), ChunkConstants.CHUNK_SIZE);
		Vector2 staticSize = new(ChunkConstants.CHUNK_SIZE - bouncySize.X, ChunkConstants.CHUNK_SIZE);

		RectangleShape2D bouncyRectangle = new();
		RectangleShape2D staticRectangle = new();
		bouncyRectangle.Size = bouncySize;
		staticRectangle.Size = staticSize;

		BouncyCollisionShape.Shape = bouncyRectangle;
		StaticCollisionShape.Shape = staticRectangle;
		BouncySurface.RotationDegrees = ChunkRotation;
	}
}
