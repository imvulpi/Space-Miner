using SpaceMiner.src.code.components.processing.data.settings.game;
using SpaceMiner.src.code.components.user.ui.components.other.game_save_item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.user.special
{
    public interface IGameController
    {
        public IGameSetting GameSettings { get; set; }
        public void Initialize();
    }
}
