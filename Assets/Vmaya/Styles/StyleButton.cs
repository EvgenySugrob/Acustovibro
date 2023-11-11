using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.Styles
{
    [RequireComponent(typeof(Button))]
    public class StyleButton : BaseStyleElement
    {
        protected Button button => GetComponent<Button>();

        public override void ApplyStyle(StyleItem item)
        {
            if (item is StyleButtonItem)
            {
                StyleButtonItem bitem = item as StyleButtonItem;

                ColorBlock colors = button.colors;
                colors.normalColor = bitem.BackgroundColor;
                colors.highlightedColor = bitem.HighlightedColor;
                colors.pressedColor = bitem.PressedColor;
                colors.selectedColor = bitem.SelectedColor;
                colors.disabledColor = bitem.DisabledColor;
                button.colors = colors;
            }
            else
            {
                Image background = button.GetComponent<Image>();

                if (item.BackgroundSprite)
                    background.sprite = item.BackgroundSprite;

                background.color = item.BackgroundColor;
            }

            ApplyTextStyle(button, item);

        }

        public override void ApplyFromEditor()
        {
            StyleButtonItem item = StyleList.Instance.GetItemButton(Class());

            item.BackgroundColor = button.colors.normalColor;
            item.HighlightedColor = button.colors.highlightedColor;
            item.PressedColor = button.colors.pressedColor;
            item.SelectedColor = button.colors.selectedColor;
            item.DisabledColor = button.colors.disabledColor;

            StyleItem sitem = default;
            GetTextStyle(button, ref sitem);

            item.TextSize = sitem.TextSize;
            item.fontStyle = sitem.fontStyle;
            item.TextColor = sitem.TextColor;

            StyleList.Instance.SetItemButton(Class(), item);
        }
    }
}
