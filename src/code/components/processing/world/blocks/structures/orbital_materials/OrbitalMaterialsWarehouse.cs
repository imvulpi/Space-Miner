using DryIoc;
using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.commons.other.DI;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.entities.implementations.player;
using SpaceMiner.src.code.components.user;
using SpaceMiner.src.code.components.user.blocks;
using SpaceMiner.src.code.components.user.entities.spaceships;
using System.Collections.Generic;

[ProtoContract]
public partial class OrbitalMaterialsWarehouse : Block, IOrganizedStructure
{
    public override string ID { get; set; } = "spaceminer.structures.orbital_materials";
    [Export] public Area2D ShopArea { get; set; }
	[Export] public PackedScene ShopMenuScene { get; set; }
    public Dictionary<CargoType, int> CargoPrices { get; set; } = new()
    {
        { CargoType.Rock, 2 },
        { CargoType.Bronze, 16 },
        { CargoType.Silver, 40 },
        { CargoType.Gold, 100 }
    };
    private ShopMenu ShopMenuGlobal;
    private GameOverlayController GameOverlay { get; set; }
    private ICargoSpaceship CargoSpaceship { get; set; }
    private IPlayerEntity PlayerEntity { get; set; }
    public override void _Ready()
	{
        ShopArea.BodyEntered += ShopArea_BodyEntered;
        ShopArea.BodyExited += ShopArea_BodyExited;
    }

    private void ShopArea_BodyExited(Node2D body)
    {
        if (body is ISpaceship spaceship)
        {
            PlayerEntity = spaceship.BoundToEntity;
            if (spaceship.BoundToEntity is ICargoSpaceship cargo)
            {
                CargoSpaceship = null;
            }

            GameOverlay.RemoveChild(ShopMenuGlobal);
            GameOverlay = null;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("Esc") && GameOverlay != null && ShopArea.OverlapsBody(GameOverlay))
        {
            if(ShopMenuGlobal == null)
            {
                ShopMenu shopMenu = ShopMenuScene.Instantiate<ShopMenu>();
                SetShopFunctions(shopMenu);
                GameOverlay.AddChild(shopMenu);
            }
            else
            {
                GameOverlay.RemoveChild(ShopMenuGlobal);
                ShopMenuGlobal = null;
            }
        }
    }

    private void ShopArea_BodyEntered(Node2D body)
    {
        if (body is ISpaceshipData spaceshipData)
        {
            PlayerEntity = spaceshipData.BoundToEntity;
            if (body is ICargoSpaceship cargo)
            {
                CargoSpaceship = cargo;
            }

            GameOverlay = DIContainer.Container.Resolve<GameOverlayController>();
            ShopMenu shopMenu = ShopMenuScene.Instantiate<ShopMenu>();
            SetShopFunctions(shopMenu);
            GameOverlay.AddChild(shopMenu);
        }
    }

    private void SetShopFunctions(ShopMenu shopMenu)
    {
        ShopMenuGlobal = shopMenu;
        shopMenu.Prices = CargoPrices;
        shopMenu.GetUserCargo = (CargoType type) =>
        {
            Cargo userCargo = CargoSpaceship.CargoModule.GetCargo(type);
            if (userCargo != null)
            {
                return userCargo.Amount;
            }
            else
            {
                return 0;
            }
        };

        shopMenu.SellConfirmed = (CargoType cargoType, int amountInt) =>
        {
            if (CargoPrices.TryGetValue(cargoType, out int price))
            {
                if (CargoSpaceship != null)
                {
                    if (CargoSpaceship.CargoModule.GetCargo(cargoType).Amount >= amountInt)
                    {
                        PlayerEntity.PlayerData.AddBalance(price * amountInt);
                        CargoSpaceship.CargoModule.RemoveCargo(cargoType, amountInt);
                    }
                }
            }
        };
    }    
}
