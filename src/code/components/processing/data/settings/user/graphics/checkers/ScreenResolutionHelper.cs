using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.processing.data.settings.user.graphics.helpers
{
    public class ScreenResolutionHelper
    {
        /// <summary>
        /// Returns (-1, -1) if can't resolve the width and height
        /// </summary>
        /// <param name="resolution">string in 'WidthxHeight'</param>
        /// <returns></returns>
        public (int width, int height) GetWidthAndHeight(string resolution)
        {
            resolution = resolution.ToLower();
            string[] resolutions = resolution.Split('x');

            if (resolutions.Length == 2)
            {
                if (int.TryParse(resolutions[0], out int parsedWidth) && int.TryParse(resolutions[1], out int parsedHeight))
                {
                    return (parsedWidth, parsedHeight);
                }
                else
                {
                    GD.PushError("Resolution value parsing failed, The provided resolutions could not be parsed to a number");
                }
            }
            else
            {
                GD.PushError("Resolution value parsing failed, There isn't exactly 2 parts divided by an 'x' ex. 1920x1080.");
            }
            return (-1, -1);
        }

        public bool CheckResolution(string resolution)
        {
            (int width, int height) = GetWidthAndHeight(resolution);
            if (width == -1 && height == -1)
            {
                return false;
            }
            return true;
        }
    }
}
