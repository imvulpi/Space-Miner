using Godot;
using SpaceMiner.src.code.components.processing.ui.menu;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class EscOverridenControl : Control
{
    public DefaultMenu menu;
    [Export] public Control HiddenControl {  get; set; }
    public override void _Ready()
	{
        if (menu != null)
        {
            GD.Print("Succesfully passed menu");

            // Overrides ESC to do nothing
            menu.EscActionDelegate = (IMenuManager manager) =>
            {
                return true;
            };
        }
        else
        {
            GD.PushError("Menu was not succesfully passed");
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if(Input.IsActionJustPressed("Esc") && HiddenControl.Visible) {
            // Closes the menu
            menu.Close();
        }
        else if (Input.IsActionJustPressed("Esc") && !HiddenControl.Visible)
        {
            // Shows HiddenControl node if ESC is pressed and the node is not visible.
            HiddenControl.Visible = true;
        }
	}
}
