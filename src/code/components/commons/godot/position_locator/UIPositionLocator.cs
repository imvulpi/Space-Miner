using Godot;
using System.Collections.Generic;

namespace SpaceMiner.src.code.components.commons.godot.position_locator
{
    public class UIPositionLocator
    {
        public static Control GetChildNodeAtPosition(Node parentNode, Vector2 position, UIPositionLocatorType type = UIPositionLocatorType.ZIndex, bool onlyVisible = true, int currentDepth = 0, int maxDepth = 4)
        {
            List<(Control, int)> childAndDepth = GetAllChildrenNodesAtPosition(parentNode, position, new List<(Control, int)>(), onlyVisible);
            return GetFinalNode(childAndDepth, type);
        }

        private static List<(Control, int)> GetAllChildrenNodesAtPosition(Node parentNode, Vector2 position, List<(Control, int)> childList, bool onlyVisible = true, int currentDepth = 0, int maxDepth = 4)
        {
            int childCount = parentNode.GetChildCount(true);
            if (currentDepth < maxDepth)
            {
                for (int i = 0; i < childCount; i++)
                {
                    Control childNode = parentNode.GetChildOrNull<Control>(i, true);
                    if (childNode != null && IsPositionInElement(position, childNode) && CheckVisibilityRules(childNode, onlyVisible))
                    {
                        childList.Add((childNode, currentDepth));
                        if (childNode.GetChildCount() > 0)
                        {
                            List<(Control, int)> positions = GetAllChildrenNodesAtPosition(childNode, position, childList, true, currentDepth++);
                            if (positions != null)
                            {
                                childList.AddRange(positions);
                            }
                        }
                    }
                }
            }
            return childList;
        }

        private static bool CheckVisibilityRules(Control node, bool onlyVisible)
        {
            if (onlyVisible && node.Visible)
            {
                return true;
            }
            else if (!onlyVisible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Control GetFinalNode(List<(Control, int)> childAndDepth, UIPositionLocatorType findType)
        {
            if (findType == UIPositionLocatorType.Shallow)
            {
                return GetShallowest(childAndDepth);
            }
            else if (findType == UIPositionLocatorType.DeepestNormal)
            {
                return childAndDepth[childAndDepth.Count - 1].Item1;
            }
            else if (findType == UIPositionLocatorType.ZIndex)
            {
                return GetBiggestZIndex(childAndDepth);
            }
            return null;
        }

        private static Control GetBiggestZIndex(List<(Control, int)> nodesDeeps)
        {
            int biggestZIndex = int.MinValue;
            Control currentFitNode = null;
            foreach ((Control, int) item in nodesDeeps)
            {
                //GD.Print($"Checking: {item.Item1.Name} Z Index {item.Item1.ZIndex}");
                if (item.Item1.ZIndex > biggestZIndex)
                {
                    biggestZIndex = item.Item1.ZIndex;
                    currentFitNode = item.Item1;
                }
            }

            if (currentFitNode != null)
            {
                return currentFitNode;
            }
            return null;
        }

        private static Control GetShallowest(List<(Control, int)> nodesDeeps)
        {
            int smallestDepth = int.MaxValue;
            Control currentFitNode = null;
            foreach ((Control, int) childNode in nodesDeeps)
            {
                if (smallestDepth > childNode.Item2)
                {
                    smallestDepth = childNode.Item2;
                    currentFitNode = childNode.Item1;
                }
            }

            if (currentFitNode != null)
            {
                return currentFitNode;
            }
            return null;
        }

        private static bool IsPositionInElement(Vector2 position, Control element)
        {
            Vector2 minPosition = element.GlobalPosition;
            Vector2 maxPosition = new Vector2(element.GlobalPosition.X + element.Size.X, element.GlobalPosition.Y + element.Size.Y);
            if (position.X >= minPosition.X && position.X <= maxPosition.X && position.Y >= minPosition.Y && position.Y <= maxPosition.Y)
            {
                return true;
            }
            return false;
        }
    }
}
