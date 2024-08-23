using Godot;

namespace SpaceMiner.src.code.components.commons.godot.project_settings.display.graphics
{
    public class GraphicsQualitiesHelper
    {
        private const string LOW = "Low";
        private const string MEDIUM = "Medium";
        private const string HIGH = "High";
        
        public GraphicsQuality? GetGraphicsQuality(string quality, bool provideDefaultValue = false)
        {
            switch (quality)
            {
                case HIGH: return GraphicsQuality.High;
                case MEDIUM: return GraphicsQuality.Medium;
                case LOW: return GraphicsQuality.Low;
                default:
                    GD.PushError($"Graphics quality: [{quality}] is not conversible | <String to Enum>");
                    if (provideDefaultValue)
                    {
                        return GraphicsQuality.Medium;
                    }
                    return null;
            }
        }
        public string GetGraphicsQuality(GraphicsQuality quality, bool provideDefaultValue = false)
        {
            switch (quality)
            {
                case GraphicsQuality.High: return HIGH;
                case GraphicsQuality.Medium: return MEDIUM;
                case GraphicsQuality.Low: return LOW;
                default:
                    GD.PushError($"Graphics quality: [{quality}] is not conversible | <Enum to String>");
                    if (provideDefaultValue)
                    {
                        return MEDIUM;
                    }
                    return null;
            }
        }
    }

    public enum GraphicsQuality
    {
        High,
        Medium,
        Low,
    }
}
