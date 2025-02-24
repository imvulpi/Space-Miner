using Godot;
using SpaceMiner.src.code.components.commons.godot.project_settings.game.world;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunk_manager;
using SpaceMiner.src.code.components.processing.world.entities.player;
using SpaceMiner.src.code.components.user.blocks.core.factories;
using SpaceMiner.src.code.components.user.blocks.structures;
using SpaceMiner.src.code.components.user.blocks.unique.barrier;
using SpaceMiner.src.code.components.user.blocks.unique.empty;
using SpaceMiner.src.code.components.user.entities.asteroids;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SpaceMiner.src.code.components.processing.data.systems.world
{
    public class WorldManager : IWorldManager
    {
        public WorldManager(IGameSettings gameSettings, Node connectToNode, PlayerEntity playerEntity)
        {
            GameSettings = gameSettings;
            ConnectToNode = connectToNode;
            PlayerEntity = playerEntity;
        }
        private IGameSettings GameSettings { get; set; }
        private PlayerEntity PlayerEntity { get; set; }
        private Node2D PlayerMovementEntity { get; set; }
        private Node ConnectToNode { get; set; }
        private ChunkManager ChunkManager = new ChunkManager();
        public AsteroidSpawner AsteroidSpawner { get; set; }
        public void Initialize()
        {
            PlayerMovementEntity = PlayerEntity.MovementNode as Node2D;
            if (GameSettings.WorldType == WorldType.Prebuild && !Directory.Exists(Path.Join(GameSettings.Path, ExternalPaths.CHUNKS_DIR)))
            {
                new ChunkizeHelper().ChunkizeMap(ConnectToNode, GameSettings, InternalPaths.MAIN_CAMPAIGN);
            }
            BlockFactory extensibleBlockFactory = SetupBlockFactory();
            Node2D chunksNode = new Node2D() { Name = "Chunks", };
            ConnectToNode.AddChild(chunksNode);
            ChunkManager chunkManager = new(2, GameSettings.SaveName, "universe", chunksNode) { BlockFactoryManager = extensibleBlockFactory, };
            ChunkManager = chunkManager;
            //chunkManager.MarkChunks(); // Debug to see chunks

            Node2D entitiesNode = new() { Name = "Entities" };
            Node2D asteroids = new() { Name = "Asteroids" };
            entitiesNode.AddChild(asteroids);
            ConnectToNode.AddChild(entitiesNode);

            AsteroidSpawner = new(PlayerMovementEntity, asteroids);
            AsteroidSpawner.Initialize();
        }

        private BlockFactory SetupBlockFactory()
        {
            BlockFactory extensibleBlockFactory = new BlockFactory();
            IBlockFactory spaceminerFactory = extensibleBlockFactory.RegisterBlockFactory("spaceminer", new BlockFactory());
            spaceminerFactory.RegisterBlockFactory("unique", new BlockFactory());
            spaceminerFactory.BlockFactories["unique"].RegisterBlockFactory("empty", new EmptyBlockFactory());
            spaceminerFactory.BlockFactories["unique"].RegisterBlockFactory("barrier", new BarrierBlockFactory());
            spaceminerFactory.RegisterBlockFactory("structures", new StructuresBlockFactory());
            spaceminerFactory.BlockFactories["structures"].RegisterBlockFactory("orbital_materials", new StructuresBlockFactory());
            spaceminerFactory.BlockFactories["structures"].RegisterBlockFactory("stellar_forge", new StructuresBlockFactory());
            return extensibleBlockFactory;
        }

        public void Manage()
        {
            ChunkManager.Manage(PlayerMovementEntity.Position);
        }
    }
}
