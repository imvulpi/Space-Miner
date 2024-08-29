using SpaceMiner.src.code.components.commons.godot.project_settings.game.world;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.game
{
    public interface IGameSettingReceive
    {
        string SaveName { get; }
        WorldType WorldType { get; }
        GameDifficulty GameDifficulty { get; }
        DateTime LastPlayed { get; }
    }
}
