using Godot;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.node;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler.base_coupler;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler
{
    public class ChunkCoupler : BaseChunkCoupler, IChunkCoupler
    {
        /// <summary>
        /// Finds and provides ChunkNode from given parameters<br></br>
        /// <br></br>NOTE: This does not convert the position to chunk position.
        /// <br></br>This doesn't add the chunk to the scene, but simply loads and returns an instance
        /// </summary>
        /// <param name="saveName">The current game save name</param>
        /// <param name="dimension">Current dimension or the dimension from which the chunk should be given</param>
        /// <param name="chunkPosition">Position of the chunk</param>
        /// <returns>A ChunkNode instance</returns>
        public ChunkNode Load(string saveName, string dimension, Vector2 chunkPosition)
        {
            return Load(OsPath.Join(ExternalPaths.SAVES_DIR, saveName, ExternalPaths.CHUNKS_DIR, dimension, ChunkHelper.GetChunkFilename(chunkPosition)));
        }

        /// <summary>
        /// Saves a Chunk Node from provided parameters<br></br>
        /// Note: Most if not all parameters can be retrieved from the ChunkInfo of the instance. 
        /// </summary>
        /// <typeparam name="T">Class which is a Node2D implementing IChunkNode</typeparam>
        /// <param name="chunk">The instance of Node2D implementing IChunkNode</param>
        /// <param name="saveName">The current game save name</param>
        /// <param name="dimension">Current dimension or the dimension to which the chunk should be saved</param>
        public void Save<T>(T chunk, string saveName, string dimension) where T : Node2D, IChunkNode
        {
            Save(chunk, OsPath.Join(ExternalPaths.SAVES_DIR, saveName, ExternalPaths.CHUNKS_DIR, dimension, chunk.Info.FileName));
        }
    }
}
