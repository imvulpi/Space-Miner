using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMiner.scripts.commons.godot
{
    public class ItemListHelper
    {
        public bool IsItemListScrollable(ItemList list)
        {
            if (list == null)
            {
                return false;
            }

            StyleBox styleBox = list.GetThemeStylebox("panel");
            float marginTop = styleBox.ContentMarginTop;
            float marginBottom = styleBox.ContentMarginBottom;
            float fontSize = list.GetThemeFontSize("panel");
            int vSeperation = list.GetThemeConstant("v_separation");
            float allItemsHeight = marginTop + marginBottom + (fontSize + vSeperation * 2) * list.ItemCount;

            if (allItemsHeight > list.Size.Y)
            {
                return true;
            }
            return false;
        }
    }
}
