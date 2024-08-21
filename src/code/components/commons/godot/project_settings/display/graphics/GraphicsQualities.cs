using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.src.code.components.commons.godot.project_settings.display.graphics
{
    public class GraphicsQualitiesHelper
    {
        private const string LOW = "low";
        private const string MEDIUM = "medium";
        private const string HIGH = "high";
        
        public string GetStringGraphicsQuality(GraphicsQuality quality)
        {
            switch(quality)
            {
                case GraphicsQuality.High:
                    return HIGH;
                case GraphicsQuality.Medium:
                    return MEDIUM;
                case GraphicsQuality.Low:
                    return LOW;
            }
            return null;
        }

        public GraphicsQuality GetGraphicsQuality(string quality)
        {
            switch (quality)
            {
                case HIGH:
                    return GraphicsQuality.High;
                case MEDIUM:
                    return GraphicsQuality.Medium;
                case LOW:
                    return GraphicsQuality.Low;
            }
            return GraphicsQuality.High;
        }
    }

    public enum GraphicsQuality
    {
        Low,
        Medium,
        High
    }
}
