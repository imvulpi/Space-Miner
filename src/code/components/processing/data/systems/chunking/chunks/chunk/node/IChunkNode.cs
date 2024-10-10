using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.node
{
    public interface IChunkNode
    {
        public IChunkInfo Info { get; set; }
    }
}
