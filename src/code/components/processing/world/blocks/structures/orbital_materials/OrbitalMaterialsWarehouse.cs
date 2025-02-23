using Godot;
using ProtoBuf;
using SpaceMiner.src.code.components.processing.data.game.spaceships.prospector;
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
    [Export] public Button OpenShopButton { get; set; }
    public Dictionary<CargoType, int> CargoPrices { get; set; } = new()
    {
        { CargoType.Rock, 2 },
        { CargoType.Bronze, 16 },
        { CargoType.Silver, 40 },
        { CargoType.Gold, 100 }
    };
    private ShopMenu ShopMenuGlobal;
    private ProspectorSpaceship userSpaceship { get; set; }
    public override void _Ready()
	{
        ShopArea.BodyEntered += ShopArea_BodyEntered;
        ShopArea.BodyExited += ShopArea_BodyExited;
        OpenShopButton.Pressed += OpenShopButton_Pressed;
    }

    private void ShopArea_BodyExited(Node2D body)
    {
        if (body is ProspectorSpaceship prospectorSpaceship)
        {
            userSpaceship.spaceshipHUD.RemoveChild(ShopMenuGlobal);
            userSpaceship = null;
        }
    }

    private void OpenShopButton_Pressed()
    {
        if (ShopMenuGlobal == null)
        {
            ShopMenu shopMenu = ShopMenuScene.Instantiate<ShopMenu>();
            SetShopFunctions(shopMenu);
            AddChild(shopMenu);
        }
        else
        {
            RemoveChild(ShopMenuGlobal);
            ShopMenuGlobal = null;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("Esc") && userSpaceship != null && ShopArea.OverlapsBody(userSpaceship))
        {
            if(ShopMenuGlobal == null)
            {
                ShopMenu shopMenu = ShopMenuScene.Instantiate<ShopMenu>();
                SetShopFunctions(shopMenu);
                userSpaceship.spaceshipHUD.AddChild(shopMenu);
            }
            else
            {
                userSpaceship.spaceshipHUD.RemoveChild(ShopMenuGlobal);
                ShopMenuGlobal = null;
            }
        }
    }

    private void ShopArea_BodyEntered(Node2D body)
    {
        if(body is ProspectorSpaceship prospectorSpaceship)
        {
            userSpaceship = prospectorSpaceship;
            ShopMenu shopMenu = ShopMenuScene.Instantiate<ShopMenu>();
            SetShopFunctions(shopMenu);
            userSpaceship.spaceshipHUD.AddChild(shopMenu);
        }
    }

    private void SetShopFunctions(ShopMenu shopMenu)
    {
        ShopMenuGlobal = shopMenu;
        shopMenu.Prices = CargoPrices;
        shopMenu.GetUserCargo = (CargoType type) =>
        {
            Cargo userCargo = userSpaceship.CargoModule.GetCargo(type);
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
                userSpaceship.PlayerData.Balance += price * amountInt;
                userSpaceship.spaceshipHUD.Balance.Text = $"{userSpaceship.PlayerData.Balance}$";
                userSpaceship.spaceshipHUD.CargoCapacity.Text = $"{userSpaceship.CargoModule.CurrentCapacity}/{userSpaceship.CargoModule.MaxCapacity}";
                userSpaceship.CargoModule.RemoveCargo(cargoType, amountInt);
            }
        };
    }    
}
