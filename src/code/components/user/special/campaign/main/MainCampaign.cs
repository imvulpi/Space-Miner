using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunkizer;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler;
using System;
using System.Collections.Generic;

public partial class MainCampaign : Node2D
{
	public override void _Ready()
	{
        Chunkizer campaignChunkize = new Chunkizer();
        ChunkCoupler chunkCoupler = new ChunkCoupler();
        List<(ChunkNode, PackedScene)> packedScenes = campaignChunkize.ChunkizeMap(GetNode("%Blocks"));
        foreach ((ChunkNode chunk, PackedScene packedChunk) in packedScenes)
        {
            chunk.Info.Dimension = "universe";
            chunk.Info.SaveName = "campaign";
            try
            {
                GD.Print(chunk.Info.BlocksData.Count);
                chunkCoupler.Save(chunk.Info);  
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e, true);
            }
        }
    }

	public override void _Process(double delta)
	{
	}
}
