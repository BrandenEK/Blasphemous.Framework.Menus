using Blasphemous.Framework.UI;
using UnityEngine;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// Creates toggle options
/// </summary>
public class ToggleCreator(ModMenu menu)
{
    private readonly ModMenu _menu = menu;

    /// <summary> The pixel size of the box image </summary>
    public int BoxSize { get; set; } = 36;
    /// <summary> The pixel size of the header text </summary>
    public int TextSize { get; set; } = 36;
    /// <summary> The color of the header text </summary>
    public Color TextColor { get; set; } = new Color32(192, 192, 192, 255);
    /// <summary> The pixel space in between the image and text </summary>
    public int ElementSpacing { get; set; } = 12;

    /// <summary>
    /// Adds a true/false setting to the UI
    /// </summary>
    public ToggleOption CreateOption(string name, Transform parent, Vector2 position, string header)
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
            Position = new Vector2(ElementSpacing, 0),
            Pivot = new Vector2(0, 0.5f),
            Size = new Vector2(100, BoxSize * 2)
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleLeft,
            Color = TextColor,
            FontSize = TextSize,
            Contents = header
        });

        // Create box image
        var boxImage = UIModder.Create(new RectCreationOptions()
        {
            Name = "box",
            Parent = holder,
            Position = Vector2.zero,
            Pivot = new Vector2(1, 0.5f),
            Size = new Vector2(BoxSize, BoxSize)
        }).AddImage();

        // Initialize toggle option
        var selectable = holder.gameObject.AddComponent<ToggleOption>();
        selectable.Initialize(_menu, boxImage);

        // Add click events
        _menu.AddClickable(boxImage.rectTransform, false, selectable.Toggle);

        return selectable;
    }
}
