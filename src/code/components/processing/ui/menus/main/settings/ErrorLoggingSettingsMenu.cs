using Godot;
using SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest;
using SpaceMiner.src.code.components.processing.ui.menu.interfaces;
using System;

public partial class ErrorLoggingSettingsMenu : Control, SpaceMiner.src.code.components.experiments.testing.scripts.MenusTest.IMenuInject
{
    public SpaceMiner.src.code.components.processing.ui.menu.interfaces.IMenuManager MenuManager { get; set; }
    public IMenu Menu { get; set; }
    [Export] public CheckBox ErrorLogging { get; set; }
    [Export] public CheckBox WarningLogging { get; set; }
    [Export] public CheckBox InfoLogging { get; set; }
}
