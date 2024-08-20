using Godot;
using SpaceMiner.scripts.commons.godot;
using SpaceMiner.src.code.components.commons.godot.position_locator;
using System;
using System.Timers;

public partial class AdvancedScrollbar : VScrollBar
{
	[Export]
	private Control scrollableNode = null;
	[Export]
	private float speed = 8f;
 	public float screenY = 0;
	public float scrollableDistance = 0;
	private int minFrames = 0;
	public override void _Ready()
	{
        CallDeferred("UpdateSize");
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        timer.Elapsed += UpdateSizeDeffered;
        timer.Enabled = true;
        Scrolling += ScrollSettingsMenu_Scrolling;
	}
    private void ScrollSettingsMenu_Scrolling()
    {
		scrollableNode.Position = new Vector2(scrollableNode.Position.X, (float)-Value);
    }

    public void UpdateSizeDeffered(object sender, ElapsedEventArgs e)
    {
        if (!IsInsideTree() && !IsQueuedForDeletion())
        {
            QueueFree();
        }
        else
        {
            CallThreadSafe("UpdateSize");
        }
    }

    public void UpdateSize()
    {
        screenY = GetViewportRect().Size.Y;
        scrollableDistance = scrollableNode.Size.Y - screenY;
        MaxValue = scrollableDistance;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

        Vector2 mousePos = GetGlobalMousePosition();
        Control firstNode = UIPositionLocator.GetChildNodeAtPosition(GetParent(), mousePos);
        ItemList itemNode = null;
        try
		{
            itemNode = (ItemList)firstNode;
        }catch (InvalidCastException){}
            GD.Print(itemNode);
		if (itemNode == null || (itemNode != null && !ItemListHelper.IsItemListScrollable(itemNode) || !itemNode.Visible))
		{
            if (Input.IsActionJustPressed("ScrollDown"))
            {
                Value += 1 * speed;
                ScrollSettingsMenu_Scrolling();
            }
            else if (Input.IsActionJustPressed("ScrollUp"))
            {
                if (Value > 0)
                {
                    Value -= 1 * speed;
                }
                ScrollSettingsMenu_Scrolling();
            }
        }
	}
}
