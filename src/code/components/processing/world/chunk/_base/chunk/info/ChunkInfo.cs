using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.user.blocks;
using System;
using System.Buffers;
using System.Collections.Generic;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info
{
    [ProtoContract]
    public class ChunkInfo : IChunkInfo
    {
        [ProtoMember(1)]
        public string SaveName { get; set; }
        [ProtoMember(2)]
        public string Dimension { get; set; }
        [ProtoMember(3)]
        public string FileName { get; set; }
        [ProtoMember(4)]
        public string ChunkVersion { get; set; }
        [ProtoMember(5)]
        public string SrlVersion { get; set; }
        [ProtoMember(6)]
        public Vector2 ChunkPosition { get; set; }
        [ProtoMember(7)]
        public ulong LastUpdate { get; set; }
        [ProtoMember(8)]
        public List<Block> BlocksData { get; set; } = new List<Block>();      
    }
}
