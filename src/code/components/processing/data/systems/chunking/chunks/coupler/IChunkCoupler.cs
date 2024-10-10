using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler
{
    public interface IChunkCoupler
    {
        public IChunkInfo Load(string saveName, string dimension, string formattedName);
        public IChunkInfo Load(string path);
        public void Save(IChunkInfo chunkInfo);
    }
}
