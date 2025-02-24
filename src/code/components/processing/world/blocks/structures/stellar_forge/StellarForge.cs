using Godot;
using SpaceMiner.src.code.components.processing.data.game.spaceships;
using SpaceMiner.src.code.components.processing.entities.implementations.player;
using SpaceMiner.src.code.components.user.blocks;
using SpaceMiner.src.code.components.user;
using SpaceMiner.src.code.components.commons.other.DI;
using DryIoc;
using SpaceMiner.src.code.components.user.entities.spaceships;
using SpaceMiner.src.code.components.processing.world.entities.spaceships;
using SpaceMiner.src.code.components.processing.world.entities.player;
using System.Collections.Generic;

public partial class StellarForge : Block, IOrganizedStructure
{
    public override string ID { get; set; } = "spaceminer.structures.stellar_forge";
    [Export] public Area2D ShopArea { get; set; }
    [Export] public PackedScene ShopMenuScene { get; set; }
    [Export] public StellarShopMenu StellarShop {get;set;}
    private GameOverlayController GameOverlay { get; set; }
    private IStellarBoosts StellarBoosts { get; set; }
    private PlayerEntity PlayerEntity { get; set; }
    private Dictionary<string, float> ProductPrices = new Dictionary<string, float>()
        {
            { "max_speed_plus_10", 250000 }
        };
    public override void _Ready()
    {
        ShopArea.BodyEntered += ShopArea_BodyEntered;
        ShopArea.BodyExited += ShopArea_BodyExited;
    }

    private void ShopArea_BodyExited(Node2D body)
    {
        if (body is ISpaceship spaceship)
        {
            PlayerEntity = null;
            if (body is IStellarBoostHolder stellarBoosts)
            {
                StellarBoosts = null;
                GameOverlay.RemoveChild(StellarShop);
                GameOverlay = null;
            }
        }
    }

    private void ShopArea_BodyEntered(Node2D body)
    {
        if (body is ISpaceship spaceship)
        {
            PlayerEntity = spaceship.BoundToEntity;
            if (body is IStellarBoostHolder stellarBoosts)
            {
                StellarBoosts = stellarBoosts.StellarBoosts;
                GameOverlay = DIContainer.Container.Resolve<GameOverlayController>();
                StellarShopMenu shopMenu = ShopMenuScene.Instantiate<StellarShopMenu>();
                SetShopFunctions(shopMenu);
                GameOverlay.AddChild(shopMenu);
            }
        }
    }

    private void SetShopFunctions(StellarShopMenu shopMenu)
    {
        StellarShop = shopMenu;
        shopMenu.Products = ProductPrices;
        shopMenu.GetAmountOwned += (string sterllar_id) =>
        {
            if(StellarBoosts.BoostsAndAmounts.TryGetValue(sterllar_id, out int value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        };
        shopMenu.PurchaseConfirmed += (string id, int amount) =>
        {
            if (ProductPrices.TryGetValue(id, out float price))
            {
                float total = price * amount;
                if (PlayerEntity.PlayerData.Balance >= (total))
                {
                    if (StellarBoosts.BoostsAndAmounts.ContainsKey(id))
                    {
                        StellarBoosts.BoostsAndAmounts[id] += amount;
                    }
                    else
                    {
                        StellarBoosts.BoostsAndAmounts.Add(id, amount);
                    }
                    PlayerEntity.PlayerData.RemoveBalance(total);
                    StellarBoosts.ApplyBoosts();
                }
                else
                {
                    shopMenu.ShowError("Not enough money");
                }
            }
        };
    }
}
