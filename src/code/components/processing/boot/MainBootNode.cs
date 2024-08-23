using Godot;
using SpaceMiner.src.code.components.processing.boot.checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpaceMiner.src.code.components.processing.boot
{
    public partial class MainBootNode : Node, IMainBoot
    {
        public MainBootNode() {
            BootActions = new List<Action>()
            {
                new UserSettingsChecker().Check,
            };
        }
        public List<Action> BootActions { get; set; }
        public void Boot()
        {
            foreach(var action in BootActions)
            {
                action.Invoke();
            }
        }
        public override void _Ready()
        {
            Boot();
        }
    }
}
