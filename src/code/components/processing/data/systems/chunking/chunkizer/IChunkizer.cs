using Godot;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunkizing
{
    public interface IChunkizer
    {
        public List<(IChunkNode chunk, PackedScene packedChunk)> ChunkizeMap(Node rootNode);
    }
}
