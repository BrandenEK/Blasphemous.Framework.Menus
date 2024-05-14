using Blasphemous.Framework.UI;
using UnityEngine;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// Creates text options
/// </summary>
public class TextCreator(ModMenu menu)
{
    private readonly ModMenu _menu = menu;

    /// <summary>
    /// Adds a text-entry setting to the UI
    /// </summary>
    public TextOption CreateOption(string name, Transform parent, Vector2 position, int size, Color color, int lineSize, string header, bool numeric, bool allowZero, int max)
    {
        // Create ui holder
        var holder = UIModder.Create(new RectCreationOptions()
        {
            Name = name,
            Parent = parent,
            Position = position,
        });

        // Create header text
        var headerText = UIModder.Create(new RectCreationOptions()
        {
            Name = "header",
            Parent = holder,
            Position = new Vector2(-10, 0),
            Pivot = new Vector2(1, 0.5f)
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleRight,
            Color = color,
            FontSize = size,
            Contents = header
        });

        // Create value text
        var valueText = UIModder.Create(new RectCreationOptions()
        {
            Name = "value",
            Parent = holder,
            Position = new Vector2(lineSize / 2, 0),
            Pivot = new Vector2(0.5f, 0.5f)
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleCenter,
            Color = Color.yellow,
            FontSize = size - 5,
            Contents = string.Empty
        });

        // CReate underline image
        var underline = UIModder.Create(new RectCreationOptions()
        {
            Name = "line",
            Parent = holder,
            Position = Vector2.zero,
            Pivot = new Vector2(0, 0.5f),
            Size = new Vector2(lineSize, 50)
        }).AddImage();

        // Initialize text option
        var selectable = holder.gameObject.AddComponent<TextOption>();
        selectable.Initialize(underline, valueText, numeric, allowZero, max);

        // Add click events
        _menu.AddClickable(underline.rectTransform, () => selectable.SetSelected(true), () => selectable.SetSelected(false));

        return selectable;
    }
}
