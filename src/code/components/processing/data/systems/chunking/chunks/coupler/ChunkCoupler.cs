using ProtoBuf;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info;
using System.IO;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler
{
    public class ChunkCoupler : IChunkCoupler
    {
        public IChunkInfo Load(string saveName, string dimension, string formattedName)
        {
            string path = OsPath.Join(ExternalPaths.SAVES_DIR, saveName, ExternalPaths.CHUNKS_DIR, dimension, formattedName);
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                return Serializer.Deserialize<ChunkInfo>(stream);
            }
        }

        public IChunkInfo Load(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                return Serializer.Deserialize<ChunkInfo>(stream);
            }
        }

        /// <summary>
        /// Saves a chunk. Requires chunk info: SaveName, Dimension, ChunkPosition
        /// </summary>
        /// <param name="chunkInfo"></param>
        public void Save(IChunkInfo chunkInfo, string path)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, chunkInfo);
                byte[] serializedData = stream.ToArray();
                File.WriteAllBytes(path, serializedData);
            }        
        }
    }
}
