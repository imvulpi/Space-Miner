namespace SpaceMiner.src.code.components.commons.godot.exports
{
    internal interface IExportChecker
    {
        public void CheckNotSetExportsNoSource(params object[] exports);
        public void CheckNotSetExports(string source, params object[] exports);
        public void CheckNotSetExports(object sourceObject, params object[] exports);
    }
}
