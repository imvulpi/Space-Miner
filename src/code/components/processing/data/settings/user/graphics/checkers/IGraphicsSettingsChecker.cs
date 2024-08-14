using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics.checkers
{
    public interface IGraphicsSettingsChecker
    {
        public bool Check(GraphicsSettings graphicsSettings);
        public bool CheckScreenMode(string value);
        public bool CheckAspectType(string value);
        public bool CheckScreenResolution(string value);
        public bool CheckGraphicsQuality(string value);
        public bool CheckVSync(string value);
    }
}
