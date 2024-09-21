using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info
{
    public interface IChunkInfo
    {
        public string FileName { get; set; }
        public string SaveName { get; set; }
        public string Dimension { get; set; }
        public string Version { get; set; }
        public Vector2 ChunkPosition { get; set; }
        public ulong LastUpdate { get; set; }
    }
}
