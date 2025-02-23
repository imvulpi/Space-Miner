using Godot;
using SpaceMiner.src.code.components.processing.ui.menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.entities.spaceships
{
    public partial class CargoViewController : Control
    {
        public CargoModule CargoModule { get; set; }
        [Export] public PackedScene CargoMenuScene { get; set; }
        [Export] public Button CargoButton { get; set; }
        private CargoMenu CargoMenu = null;
        public override void _Ready()
        {
            CargoButton.Pressed += CargoButton_Pressed;
        }

        private void CargoButton_Pressed()
        {
            if (CargoMenu == null)
            {
                CargoMenu = CargoMenuScene.Instantiate<CargoMenu>();
                CargoMenu.CargoList = CargoModule.CargoList;
                CargoMenu.CloseMenu += CargoMenu_CloseMenu;
                AddChild(CargoMenu);
                CargoMenu.Initialize();
            }
            else
            {
                CargoMenu_CloseMenu(this, EventArgs.Empty);
            }
        }

        private void CargoMenu_CloseMenu(object sender, EventArgs e)
        {
            if(CargoMenu != null)
            {
                RemoveChild(CargoMenu);
                CargoMenu = null;
            }
        }
    }
}
