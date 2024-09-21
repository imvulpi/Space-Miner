using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.logging;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.node;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler.base_coupler
{
    public class BaseChunkCoupler : IBaseChunkCoupler
    {
        public string SaveName { get; set; }

        /// <summary>
        /// Retrieves a ChunkNode which implements IChunkNode
        /// </summary>
        /// <param name="path">Absolute path to the chunk</param>
        /// <returns>ChunkNode from path</returns>
        /// <exception cref="GameException">Throws an error if it fails to instatiate the file.</exception>
        public ChunkNode Load(string path)
        {
            PackedScene packedScene = ResourceLoader.Load<PackedScene>(path);
            ChunkNode chunkNode = packedScene.InstantiateOrNull<ChunkNode>() ?? throw new GameException(commons.errors.PrettyErrorType.OperationFailed, "ChunkLoading", $"Loaded {path} could not be retrieved as Chunk node");
            return chunkNode;
        }

        /// <summary>
        /// Saves a chunk which needs to be a Node2D and needs to implement IChunkNode
        /// </summary>
        /// <typeparam name="T">Node2D class implementing IChunkNode</typeparam>
        /// <param name="chunk">An instance of Node2D implementing IChunkNode</param>
        /// <param name="path">Absolute path where it should be saved (must include the filename)</param>
        /// <exception cref="GameException"></exception>
        public void Save<T>(T chunk, string path) where T : Node2D, IChunkNode
        {
            PackedScene packedScene = new PackedScene();
            packedScene.Pack(chunk);
            Error saveErr = ResourceSaver.Save(packedScene, path);
            if (saveErr != Error.Ok)
            {
                throw new GameException(commons.errors.PrettyErrorType.OperationFailed, "ChunkSaving", $"Saving chunk: {chunk.Name} failed. | {saveErr}");
            }
        }
    }
}
