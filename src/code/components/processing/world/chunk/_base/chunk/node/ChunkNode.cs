using Godot;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.node;

namespace SpaceMiner.src.code.components.processing.data.game.chunks.chunk
{
    public partial class ChunkNode : Node2D, IChunkNode
    {
        public ChunkNode()
        {
            Info ??= new ChunkInfo();
        }

        public ChunkNode(IChunkInfo info)
        {
            Info ??= info;
        }

        public IChunkInfo Info {  get; set; }
        public override void _PhysicsProcess(double delta)
        {
            Info.LastUpdate = Time.GetTicksMsec();
        }
    }
}
