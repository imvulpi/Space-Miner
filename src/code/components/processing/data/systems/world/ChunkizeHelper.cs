using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.commons.other.IO;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.commons.other;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunkizer;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler;
using SpaceMiner.src.code.components.processing.data.systems.chunking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.systems.world
{
    public class ChunkizeHelper
    {
        /// <summary>
        /// Chunkizes a map and saves it in chunks directory in the save.
        /// </summary>
        /// <param name="nodeInScene">Any node in a scene</param>
        /// <param name="gameSettings">Game settings</param>
        /// <param name="mapPath">The path to map that will be chunkized</param>
        public void ChunkizeMap(Node nodeInScene, IGameSettings gameSettings, string mapPath)
        {
            SceneManagerHelper sceneManagerHelper = new();
            SceneManager sceneManager = sceneManagerHelper.FindSceneManager(nodeInScene);
            if (sceneManager != null)
            {
                Chunkizer chunkizer = new();
                ChunkCoupler chunkCoupler = new ChunkCoupler();
                Node node = sceneManager.LoadScene(mapPath);
                List<(ChunkNode, PackedScene)> list = chunkizer.ChunkizeMap(node.GetNode("%Blocks"));
                foreach ((ChunkNode chunk, PackedScene packedChunk) in list)
                {
                    chunk.Info.Dimension = "universe";
                    chunk.Info.SaveName = gameSettings.SaveName;
                    try
                    {
                        string chunkDir = Path.Join(gameSettings.Path, ExternalPaths.CHUNKS_DIR,
                            chunk.Info.Dimension);
                        DirectoryHelper.ValidateUserDirectories(chunkDir);
                        string path = Path.Join(chunkDir, ChunkHelper.GetChunkFilename(chunk.Info.ChunkPosition));
                        chunkCoupler.Save(chunk.Info, path);
                    }
                    catch (Exception e)
                    {
                        ExceptionHandler.HandleException(e, true);
                    }
                }
            }
            sceneManager.UnloadScenes(mapPath);
        }
    }
}
