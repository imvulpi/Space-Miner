using Godot;
using SpaceMiner.src.code.components.commons.other.game_information;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.chunk.info;
using SpaceMiner.src.code.components.user;
using SpaceMiner.src.code.components.user.blocks;
using System.Collections.Generic;

namespace SpaceMiner.src.code.components.processing.data.systems.chunking.chunkizer
{
    public class Chunkizer
    {
        public List<(ChunkNode chunk, PackedScene packedChunk)> ChunkizeMap(Node rootNode)
        {
            List<(Node node, Vector2 chunkVector)> nodeInfo = GetNodes(rootNode);
            List<(Node node, Vector2 chunkVector)> structurizedNodes = StructurizeNodes(nodeInfo);

            Dictionary<Vector2, ChunkNode> chunks = new();
            foreach ((Node node, Vector2 nodeChunk) in structurizedNodes)
            {
                if (chunks.TryGetValue(nodeChunk, out ChunkNode chunkNode))
                {
                    if(node is Block block)
                    {
                        block.BlockPosition = block.Position + ((nodeChunk * -1) * ChunkConstants.CHUNK_SIZE);
                        chunkNode.Info.BlocksData.Add(block);
                    }
                    chunkNode.AddChild(node);
                    node.Owner = chunkNode;
                }
                else
                {
                    ChunkNode newChunkNode = CreateChunk(nodeChunk);
                    if (node is Block block)
                    {
                        block.BlockPosition = block.Position + ((nodeChunk * -1) * ChunkConstants.CHUNK_SIZE);
                        newChunkNode.Info.BlocksData.Add(block);
                    }
                    chunks.Add(nodeChunk, newChunkNode);
                    newChunkNode.AddChild(node);
                    node.Owner = newChunkNode;
                }
            }

            return ConvertToChunks(chunks);
        }

        private ChunkNode CreateChunk(Vector2 chunkPosition)
        {
            ChunkNode newChunkNode = new()
            {
                Name = ChunkHelper.GetChunkFilename(chunkPosition),
                GlobalPosition = new Vector2(chunkPosition.X*ChunkConstants.CHUNK_SIZE, chunkPosition.Y*ChunkConstants.CHUNK_SIZE)
            };

            newChunkNode.Info = new ChunkInfo()
            {
                ChunkPosition = chunkPosition,
                FileName = ChunkHelper.GetChunkFilename(chunkPosition),
                ChunkVersion = GameInfo.CHUNK_SYSTEM_VERSION
            };

            return newChunkNode;
        }

        private List<(ChunkNode, PackedScene)> ConvertToChunks(Dictionary<Vector2, ChunkNode> chunkValues)
        {
            List<(ChunkNode, PackedScene)> chunks = new();
            foreach (var item in chunkValues)
            {
                PackedScene packedScene = new PackedScene();
                packedScene.Pack(item.Value);
                chunks.Add((item.Value, packedScene));
            }
            return chunks;
        }

        private List<(Node node, Vector2 chunk)> GetNodes(Node rootNode)
        {
            List<(Node, Vector2)> nodes = new();
            int childCount = rootNode.GetChildCount();
            int initialChildCount = childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (initialChildCount != rootNode.GetChildCount())
                {
                    childCount = rootNode.GetChildCount();
                    i = 0;
                }

                Node node = rootNode.GetChild(i);
                Vector2 nodePosition = node.Get("global_position").AsVector2();
                Vector2 nodeChunk = ChunkHelper.ConvertToChunkVector(nodePosition);
                nodes.Add((node, nodeChunk));
                nodes.AddRange(GetNodes(node));
            }

            return nodes;
        }

        private List<(Node, Vector2)> StructurizeNodes(List<(Node node, Vector2 chunkVector)> values)
        {
            List<(Node, Vector2)> structurizedNodes = new List<(Node, Vector2)>();
            List<Node> entityNodes = new();
            foreach (var (node, chunkVector) in values)
            {
                if (node is IOrganizedStructure entityNode)
                {
                    node.GetParent()?.RemoveChild(node);
                    structurizedNodes.Add((node, chunkVector));
                    entityNodes.Add(node);
                }
                else
                {
                    bool breakStructure = true;
                    foreach (var entity in entityNodes)
                    {
                        if (entity.IsAncestorOf(node))
                        {
                            breakStructure = false;
                        }
                    }

                    if (breakStructure)
                    {
                        node.GetParent()?.RemoveChild(node);
                        node.Set("global_position", ChunkHelper.CalculateRelativePosition(node.Get("global_position").AsVector2()));
                        structurizedNodes.Add((node, chunkVector));
                    }
                }
            }
            return structurizedNodes;
        }
    }
}
