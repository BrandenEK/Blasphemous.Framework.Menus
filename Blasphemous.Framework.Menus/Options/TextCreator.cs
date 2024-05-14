using Blasphemous.Framework.UI;
using UnityEngine;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// Creates text options
/// </summary>
public class TextCreator(ModMenu menu)
{
    private readonly ModMenu _menu = menu;

    /// <summary> The pixel size of the underline </summary>
    public int LineSize { get; set; } = 200;
    /// <summary> The pixel size of the header and value text </summary>
    public int TextSize { get; set; } = 36;
    /// <summary> The color of the header text </summary>
    public Color TextColor { get; set; } = new Color32(192, 192, 192, 255);
    /// <summary> The color of the value text </summary>
    public Color TextColorAlt { get; set; } = new Color32(255, 231, 65, 255);
    /// <summary> The pixel space in between the header and value text </summary>
    public int ElementSpacing { get; set; } = 10;

    /// <summary>
    /// Adds a text-entry setting to the UI
    /// </summary>
    public TextOption CreateOption(string name, Transform parent, Vector2 position, string header, bool numeric, bool allowZero, int max)
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
            Position = new Vector2(-ElementSpacing, 0),
            Pivot = new Vector2(1, 0.5f)
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleRight,
            Color = TextColor,
            FontSize = TextSize,
            Contents = header
        });

        // Create value text
        var valueText = UIModder.Create(new RectCreationOptions()
        {
            Name = "value",
            Parent = holder,
            Position = new Vector2(LineSize / 2, 0),
            Pivot = new Vector2(0.5f, 0.5f)
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleCenter,
            Color = TextColorAlt,
            FontSize = TextSize - 5,
            Contents = string.Empty
        });

        // Create underline image
        var underline = UIModder.Create(new RectCreationOptions()
        {
            Name = "line",
            Parent = holder,
            Position = Vector2.zero,
            Pivot = new Vector2(0, 0.5f),
            Size = new Vector2(LineSize, TextSize)
        }).AddImage();

        // Initialize text option
        var selectable = holder.gameObject.AddComponent<TextOption>();
        selectable.Initialize(_menu, underline, valueText, numeric, allowZero, max);

        // Add click events
        _menu.AddClickable(underline.rectTransform, () => selectable.SetSelected(true), () => selectable.SetSelected(false));

        return selectable;
    }
}
