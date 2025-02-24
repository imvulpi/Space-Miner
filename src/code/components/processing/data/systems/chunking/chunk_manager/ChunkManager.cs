using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.node;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler;
using SpaceMiner.src.code.components.user.blocks;
using SpaceMiner.src.code.components.user.blocks.core.factories;
using System;
using System.Collections.Generic;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunk_manager
{
    public class ChunkManager : IChunkManager
    {
        public ChunkManager() { }
        public ChunkManager(int chunkDistance, string saveName, string placeName, Node2D connectNode) { 
            ChunkDistance = chunkDistance;
            ChunksDirectoryName = saveName;
            ConnectNode = connectNode;
            PlaceName = placeName;
        }

        public int ChunkDistance { get; set; }
        public string ChunksDirectoryName { get; set; }
        public string PlaceName {  get; set; }
        public Node ConnectNode { get; set; }
        public BlockFactory BlockFactoryManager { get; set; }
        public List<Vector2> LoadedChunksPositions = new();
        public Dictionary<Vector2, ChunkNode> LoadedChunks = new();
        public Vector2 LastChunk = Vector2.Inf;
        public ChunkCoupler ChunkCoupler = new();
        public event EventHandler<ChunkNode> NewChunkLoaded;
        public event EventHandler<ChunkNode> OldChunkUnloaded;
        public void Manage(Vector2 position)
        {
            if (ChunkDistance == 0 || ChunksDirectoryName == null || ChunksDirectoryName == String.Empty || ConnectNode == null || BlockFactoryManager == null)
            {
                // Also checks the properties.
                GameException ex = new(PrettyErrorType.Critical, "Chunk manager properties", $"Required properties are not set. " +
                    $"Invalid check: " +
                    $"ChunkDistance - {(ChunkDistance == 0 ? "invalid" : "valid")} " +
                    $"(ChunksDirectoryName - {ChunksDirectoryName} (check if save exists)) " +
                    $"ConnectNode - {(ConnectNode == null ? "invalid" : "valid")} " +
                    $"BlockFactoryManager - {(BlockFactoryManager == null ? "invalid" : "valid")}");
                throw ex;
            }

            Vector2 currentChunk = ChunkHelper.ConvertToChunkVector(position);
            if (LastChunk != currentChunk)
            {
                Load(currentChunk);
                LastChunk = currentChunk;
                Unload(currentChunk);
            }
        }

        public void MarkChunks()
        {
            foreach ((Vector2 chunkPos, ChunkNode chunkNode) in LoadedChunks)
            {
                ColorRect colorRect = new ColorRect()
                {
                    UniqueNameInOwner = true,
                    Name = "%DebugColor",
                    Size = (Vector2.One * ChunkConstants.CHUNK_SIZE) * new Vector2(0.99f, 0.99f),
                    Position = ChunkConstants.CHUNK_SIZE * new Vector2(0.01f, 0.01f),
                    Color = new Color(0.396f, 1, 0.263f),
                    ZIndex = -100
                };
                chunkNode.AddChild(colorRect);
            }
            NewChunkLoaded += Debug_NewChunkLoaded;
            OldChunkUnloaded += Debug_OldChunkUnloaded;
        }

        private void Debug_OldChunkUnloaded(object sender, ChunkNode e)
        {
            if (e.HasNode("%DebugColor"))
            {
                e.RemoveChild(e.GetNode("%DebugColor"));
            }
        }

        private void Debug_NewChunkLoaded(object sender, ChunkNode e)
        {
            ColorRect colorRect = new ColorRect()
            {
                UniqueNameInOwner = true,
                Name = "%DebugColor",
                Size = (Vector2.One * ChunkConstants.CHUNK_SIZE) * new Vector2(0.99f, 0.99f),
                Position = ChunkConstants.CHUNK_SIZE * new Vector2(0.01f, 0.01f),
                Color = new Color(0.396f, 1, 0.263f),
                ZIndex = -100
            };
            e.AddChild(colorRect);
        }

        public void StopMarkChunks()
        {
            foreach ((Vector2 chunkPos, ChunkNode chunkNode) in LoadedChunks)
            {
                chunkNode.RemoveChild(chunkNode.GetNode("%DebugColor"));
            }
        }

        public void Load(Vector2 position)
        {
            for (int i = -ChunkDistance; i <= ChunkDistance; i++)
            {
                for (int o = -ChunkDistance; o <= ChunkDistance; o++)
                {
                    Vector2 chunk = new Vector2(i + position.X, o + position.Y);
                    if (!LoadedChunksPositions.Contains(chunk))
                    {
                        LoadChunk(chunk, ConnectNode, ChunksDirectoryName);
                        LoadedChunksPositions.Add(chunk);
                    }
                }
            }
        }

        public void Unload(Vector2 position)
        {
            Vector2 minPos = new(position.X - ChunkDistance, position.Y - ChunkDistance);
            Vector2 maxPos = new(position.X + ChunkDistance, position.Y + ChunkDistance);
            int unloaded = 0;
            for (int i = 0; i < LoadedChunksPositions.Count; i++)
            {
                Vector2 chunk = LoadedChunksPositions[i];
                if (chunk.Y < minPos.Y || chunk.X < minPos.X || chunk.Y > maxPos.Y || chunk.X > maxPos.X)
                {
                    LoadedChunks.TryGetValue(chunk, out ChunkNode node);

                    if (node != null)
                    {
                        OldChunkUnloaded?.Invoke(this, node);
                        LoadedChunks.Remove(chunk);
                        LoadedChunksPositions.Remove(chunk);
                        ConnectNode.RemoveChild(node);
                        node.QueueFree();
                        i--;
                        unloaded += 1;
                    }
                }
            }
        }

        private void LoadBlocks(IChunkInfo chunkInfo, Node connectNode)
        {
            foreach (Block data in chunkInfo.BlocksData) {
                connectNode.AddChild(BlockFactoryManager.GetBlock(data.ID, data));
            }
        }

        private void LoadChunk(Vector2 chunkPosition, Node connectNode, string chunksDirectoryName)
        {
            try
            {
                ChunkNode chunkNode = new ChunkNode(ChunkCoupler.Load(chunksDirectoryName, PlaceName, ChunkHelper.GetChunkFilename(chunkPosition)));
                chunkNode.Name = ChunkHelper.GetChunkFilename(chunkPosition);
                chunkNode.Position = chunkPosition * ChunkConstants.CHUNK_SIZE;
                connectNode.AddChild(chunkNode);
                LoadedChunks.Add(chunkPosition, chunkNode);
                LoadBlocks(chunkNode.Info, chunkNode);
                NewChunkLoaded?.Invoke(this, chunkNode);
            }
            catch (Exception ex)
            {
                //GD.Print($"{ex}\n\n");
                EmptyChunk emptyChunk = ResourceLoader.Load<PackedScene>(InternalPaths.EMPTY_CHUNK_SCENE).Instantiate<EmptyChunk>();
                emptyChunk.Position = chunkPosition * ChunkConstants.CHUNK_SIZE;
                emptyChunk.Name = chunkPosition.ToString();
                connectNode.AddChild(emptyChunk);
                LoadedChunks.Add(chunkPosition, emptyChunk);
                NewChunkLoaded?.Invoke(this, emptyChunk);
            }
        }
    }
}
