using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunk_manager
{
    public class ChunkManager : IChunkManager
    {
        public ChunkManager() { }
        public ChunkManager(int chunkDistance, string chunksDirectoryName, string placeName, Node2D connectNode) { 
            ChunkDistance = chunkDistance;
            ChunksDirectoryName = chunksDirectoryName;
            ConnectNode = connectNode;
            PlaceName = placeName;
        }

        public int ChunkDistance { get; set; }
        public string ChunksDirectoryName { get; set; }
        public string PlaceName {  get; set; }
        public Node ConnectNode { get; set; }

        public List<Vector2> LoadedChunksPositions = new();
        public Dictionary<Vector2, Node> LoadedChunks = new();
        public Vector2 LastChunk = Vector2.Inf;

        private IChunkCoupler chunkCoupler = new ChunkCoupler();
        public void Manage(Vector2 position)
        {
            if(ChunkDistance == 0 || ChunksDirectoryName == null || ChunksDirectoryName == String.Empty || ConnectNode == null)
            {
                GameException ex = new(PrettyErrorType.Invalid, "ChunkDistance, GameSaveName or ConnectNode", "Required parameters are not set.");
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
                    LoadedChunks.TryGetValue(chunk, out Node node);

                    if (node != null)
                    {
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

        private void LoadChunk(Vector2 chunkPosition, Node connectNode, string chunksDirectoryName)
        {
            try
            {
                ChunkNode chunkNode = chunkCoupler.Load(chunksDirectoryName, PlaceName, chunkPosition);
                connectNode.AddChild(chunkNode);
                LoadedChunks.Add(chunkPosition, chunkNode);
            }
            catch (Exception)
            {
                EmptyChunk emptyChunk = ResourceLoader.Load<PackedScene>(InternalPaths.EMPTY_CHUNK_SCENE).Instantiate<EmptyChunk>();
                emptyChunk.Position = chunkPosition * ChunkConstants.CHUNK_SIZE;
                emptyChunk.Name = chunkPosition.ToString();
                connectNode.AddChild(emptyChunk);
                LoadedChunks.Add(chunkPosition, emptyChunk);
            }
        }
    }
}
