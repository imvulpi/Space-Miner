using Godot;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunk_manager;
using SpaceMiner.src.code.components.user.blocks.core.factories;
using SpaceMiner.src.code.components.user.blocks.unique.barrier;
using SpaceMiner.src.code.components.user.blocks.unique.empty;

public partial class CampaignLoader : Node2D
{
	ChunkManager Manager = new();
	[Export] CharacterBody2D characterBody;
	public override void _Ready()
	{
		string saveName = "campaign";
        BlockFactory extensibleBlockFactory = new BlockFactory();
		IBlockFactory spaceminerFactory = extensibleBlockFactory.RegisterBlockFactory("spaceminer", new BlockFactory());
        spaceminerFactory.RegisterBlockFactory("unique", new BlockFactory());
        spaceminerFactory.BlockFactories["unique"].RegisterBlockFactory("empty", new EmptyBlockFactory());
        spaceminerFactory.BlockFactories["unique"].RegisterBlockFactory("barrier", new BarrierBlockFactory());

        ChunkManager chunkManager = new(2, saveName, "universe", GetNode<Node2D>("Chunks"))
		{
			BlockFactoryManager  = extensibleBlockFactory,
		};

        Manager = chunkManager;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		try
		{
			Manager.Manage(characterBody.Position);
		}
		catch (GameException ex)
		{
			if(ex.ErrorType != null && ex.ErrorType == PrettyErrorType.Critical)
			{
				ExceptionHandler.HandleException(ex, true);
			} 
		}
	}
}
