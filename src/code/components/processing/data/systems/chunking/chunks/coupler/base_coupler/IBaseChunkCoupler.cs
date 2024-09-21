using Godot;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.node;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler.base_coupler
{
    public interface IBaseChunkCoupler
    {
        public ChunkNode Load(string path);
        public void Save<T>(T chunk, string path)
            where T : Node2D, IChunkNode;
    }
}
