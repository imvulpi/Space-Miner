using Godot;

namespace SpaceMiner.src.code.components.commons.godot.project_settings.display.window.stretch
{
    public class AspectTypeHelper
    {
        private const string IGNORE = "ignore";
        private const string KEEP = "keep";
        private const string KEEP_WIDTH = "keep_width";
        private const string KEEP_HEIGHT = "keep_height";
        private const string EXPAND = "expand";

        public AspectType? GetAspectType(string aspectType, bool provideDefaultValue = false)
        {
            switch(aspectType)
            {
                case IGNORE:  return AspectType.Ignore;
                case KEEP: return AspectType.Keep;
                case KEEP_WIDTH: return AspectType.KeepWidth;
                case KEEP_HEIGHT: return AspectType.KeepHeight;
                case EXPAND: return AspectType.Expand;
                default:
                    GD.PushError($"Aspect type: [{aspectType}] is not conversible | <String to Enum>");
                    if (provideDefaultValue)
                    {
                        return AspectType.Keep;
                    }
                    return null;
            }
        }

        public string GetAspectType(AspectType aspectType, bool provideDefaultValue = false)
        {
            switch (aspectType)
            {
                case AspectType.Ignore: return IGNORE;
                case AspectType.Keep: return KEEP;
                case AspectType.KeepWidth: return KEEP_WIDTH;
                case AspectType.KeepHeight: return KEEP_HEIGHT;
                case AspectType.Expand: return EXPAND;
                default:
                    GD.PushError($"Aspect type: [{aspectType}] is not conversible | <Enum to String>");
                    if (provideDefaultValue)
                    {
                        return KEEP;
                    }
                    return null;
            }
        }
    }

    public enum AspectType
    {
        Ignore,
        Keep,
        KeepWidth,
        KeepHeight,
        Expand
    }
}
