using Godot;
using SpaceMiner.src.code.components.user.blocks;
using System.Collections.Generic;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info
{
    public interface IChunkInfo
    {       
        // Meta data
        public string SaveName { get; set; }
        public string Dimension { get; set; }
        public string FileName { get; set; }
        public string ChunkVersion { get; set; }
        public string SrlVersion { get; set; }
        // Actual data
        public Vector2 ChunkPosition { get; set; }
        public ulong LastUpdate { get; set; }
        public List<Block> BlocksData { get; set; }

        // Ideas: Enviroment type (like special regions)
    }
}
