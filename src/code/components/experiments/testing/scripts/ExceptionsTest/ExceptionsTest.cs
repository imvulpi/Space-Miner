using Godot;
using SpaceMiner.src.code.components.commons.errors.exceptions.handlers;
using System;
using System.IO;

public partial class ExceptionsTest : Control
{
	[Export] public Button ErrorButton { get; set; }
	public override void _Ready()
	{
        ErrorButton.Pressed += ErrorButton_Pressed;
	}

    private void ErrorButton_Pressed()
    {
        try
        {
            throw new Exception("This is an error example, not an actual example. Exception example.");
            using (FileStream fs = File.Open("C:\\non_existent_file.txt", FileMode.Open))
            {
                // This line won't be executed
            }
        }catch(Exception ex)
        {
            ExceptionHandler.HandleException(ex);
        }
    }

    public override void _Process(double delta)
    {

    }
}
