using Godot;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.node;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler
{
    public interface IChunkCoupler
    {
        public ChunkNode Load(string saveName, string dimensionName, Vector2 chunkPosition);
        public void Save<T>(T chunk, string saveName, string placeName)
            where T : Node2D, IChunkNode;
    }
}
