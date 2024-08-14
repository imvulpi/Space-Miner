using Godot;
using System;

namespace SpaceMiner.src.code.components.commons.godot.exports
{
    /// <summary>
    /// This will not check non-nullable values or for default values of these non-nullable types
    /// </summary>
    internal class ExportChecker : IExportChecker
    {
        public void CheckNotSetExportsNoSource(params object[] exports)
        {
            if (CheckEmptyAndWarn(exports))
            {
                return;
            }

            uint nullCount = 0;
            foreach (object obj in exports)
            {
                if (obj == null)
                {
                    nullCount++;
                }
            }
            if (nullCount > 0)
            {
                string errorMessage = $"{nullCount} Export {(nullCount == 1 ? "value is" : "values are")} not set somewhere";
                GD.PushError(errorMessage);
            }
        }

        public void CheckNotSetExports(string source, params object[] exports)
        {
            if (CheckEmptyAndWarn())
            {
                return;
            }

            uint nullCount = 0;
            foreach (object obj in exports)
            {
                if (obj == null)
                {
                    nullCount++;
                }
            }
            if (nullCount > 0)
            {
                string errorMessage = $"{nullCount} Export {(nullCount == 1 ? "value is" : "values are")} not set at {source}";
                GD.PushError(errorMessage);
            }
        }

        public void CheckNotSetExports(object sourceObject, params object[] exports)
        {
            CheckNotSetExports(sourceObject.ToString(), null);
        }

        private bool CheckEmptyAndWarn(params object[] objects)
        {
            if (objects == null || objects.Length == 0)
            {
                string errorMessage = "there is no objects in the ExportChecker";
                GD.PushWarning(errorMessage);
                return true;
            }
            return false;
        }
    }
}
