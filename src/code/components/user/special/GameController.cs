using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions;
using SpaceMiner.src.code.components.commons.errors;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using SpaceMiner.src.code.components.commons.godot.project_settings.game.world;
using SpaceMiner.src.code.components.commons.other;
using SpaceMiner.src.code.components.commons.other.IO;
using SpaceMiner.src.code.components.commons.other.paths.external_paths;
using SpaceMiner.src.code.components.commons.other.paths.internal_paths;
using SpaceMiner.src.code.components.processing.data.game.chunks.chunk;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.data.game.spaceships.prospector;
using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.processing.data.systems.chunking;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunk_manager;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunkizer;
using SpaceMiner.src.code.components.processing.data.systems.chunking.chunks.coupler;
using SpaceMiner.src.code.components.user.blocks.core.factories;
using SpaceMiner.src.code.components.user.blocks.unique.barrier;
using SpaceMiner.src.code.components.user.blocks.unique.empty;
using SpaceMiner.src.code.components.user.special;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using SpaceMiner.src.code.components.user.entities.asteroids;
using SpaceMiner.src.code.components.processing.data.settings.user.couplers;
using SpaceMiner.src.code.components.user.special.player;
using ProtoBuf;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using SpaceMiner.src.code.components.user.blocks.structures;

public partial class GameController : Node, IGameController
{
    public IGameSetting GameSettings { get; set; }
    public SpaceshipCoupler SpaceshipManager { get; set; }
    public Spaceship Spaceship { get; set; }
    public ChunkManager ChunkManager { get; set; }
    public AsteroidSpawner AsteroidSpawner { get; set; }
    public IPlayerData PlayerData { get; set; }
    [Export] public Button Save { get; set; }
    private PlayerDataCoupler playerDataCoupler = new();
    private SpaceshipCoupler spaceshipCoupler = new();
    private UserSettings UserSettings { get; set; }
    public void Initialize()
    {
        try
        {
            UserSettingCoupler settingCoupler = new();
            UserSettings userSettings = settingCoupler.Load(new UserSettings()) as UserSettings;
            UserSettings = userSettings;
            IPlayerData playerData = playerDataCoupler.GetPlayerData(GameSettings.SaveName, userSettings.UUID);
            if (playerData != null)
            {
                PlayerData = playerData;
            }
            else
            {
                PlayerData newPlayerData = new PlayerData()
                {
                    UUID = userSettings.UUID,
                    Nickname = userSettings.Username,
                    Balance = 0
                };
                playerDataCoupler.SavePlayerData(GameSettings.SaveName, userSettings.UUID, newPlayerData);
                PlayerData = newPlayerData;
            }   
            
            GameSettings.Path ??= Path.Join(Godot.OS.GetUserDataDir(), ExternalPaths.SAVES_DIR, GameSettings.SaveName);
            SpaceshipManager ??= new();
            SpaceshipManager.Initialize();
            Spaceship = spaceshipCoupler.GetSavedSpaceship(GameSettings);
            if (Spaceship == null)
            {
                ProspectorSpaceship prospectorSpaceship = new();
                prospectorSpaceship.PlayerData = PlayerData;
                Spaceship = SpaceshipManager.SpaceshipFactory.GetSpaceship(prospectorSpaceship);
                spaceshipCoupler.SaveSpaceship(GameSettings, Spaceship);
            }
            else if(Spaceship is ProspectorSpaceship prospector)
            {
                prospector.PlayerData = PlayerData;
            }

            if (GameSettings.WorldType == WorldType.Prebuild && !Directory.Exists(Path.Join(GameSettings.Path, ExternalPaths.CHUNKS_DIR)))
            {
                SceneManagerHelper sceneManagerHelper = new();
                SceneManager sceneManager = sceneManagerHelper.FindSceneManager(this);
                if (sceneManager != null)
                {
                    Chunkizer chunkizer = new();
                    ChunkCoupler chunkCoupler = new ChunkCoupler();
                    Node node = sceneManager.LoadScene(InternalPaths.MAIN_CAMPAIGN);
                    List<(ChunkNode, PackedScene)> list = chunkizer.ChunkizeMap(node.GetNode("%Blocks"));
                    foreach ((ChunkNode chunk, PackedScene packedChunk) in list)
                    {
                        chunk.Info.Dimension = "universe";
                        chunk.Info.SaveName = GameSettings.SaveName;
                        try
                        {
                            string chunkDir = Path.Join(GameSettings.Path, ExternalPaths.CHUNKS_DIR,
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
                sceneManager.UnloadScenes(InternalPaths.MAIN_CAMPAIGN);
            }

            BlockFactory extensibleBlockFactory = new BlockFactory();
            IBlockFactory spaceminerFactory = extensibleBlockFactory.RegisterBlockFactory("spaceminer", new BlockFactory());
            spaceminerFactory.RegisterBlockFactory("unique", new BlockFactory());
            spaceminerFactory.BlockFactories["unique"].RegisterBlockFactory("empty", new EmptyBlockFactory());
            spaceminerFactory.BlockFactories["unique"].RegisterBlockFactory("barrier", new BarrierBlockFactory());
            spaceminerFactory.RegisterBlockFactory("structures", new StructuresBlockFactory());
            spaceminerFactory.BlockFactories["structures"].RegisterBlockFactory("orbital_materials", new StructuresBlockFactory());

            Node2D chunksNode = new Node2D()
            {
                Name = "Chunks",
            };
            AddChild(chunksNode);

            ChunkManager chunkManager = new(2, GameSettings.SaveName, "universe", chunksNode)
            {
                BlockFactoryManager = extensibleBlockFactory,
            };
        
            ChunkManager = chunkManager;
            //chunkManager.MarkChunks(); // Debug to see chunks
            AddChild(Spaceship);
            AsteroidSpawner = new(Spaceship, this);
            AsteroidSpawner.Manage();
        }
        catch (GameException ex)
        {
            if (ex.ErrorType != null && ex.ErrorType == PrettyErrorType.Critical)
            {
                ExceptionHandler.HandleException(ex, true);
            }
        }
        catch(Exception ex)
        {
            ExceptionHandler.HandleException(ex, true);
        }

        Save.Pressed += Save_Pressed;
    }

    private void Save_Pressed()
    {
        GD.Print("Saved");
        playerDataCoupler.SavePlayerData(GameSettings.SaveName, UserSettings.UUID, (Spaceship as ProspectorSpaceship).PlayerData as PlayerData);
        spaceshipCoupler.SaveSpaceship(GameSettings, Spaceship);
    }

    public override void _Process(double delta)
    {
        try
        {
            ChunkManager.Manage(Spaceship.Position);
        }
        catch (GameException ex)
        {
            if (ex.ErrorType != null && ex.ErrorType == PrettyErrorType.Critical)
            {
                ExceptionHandler.HandleException(ex, true);
            }
        }
    }
}
