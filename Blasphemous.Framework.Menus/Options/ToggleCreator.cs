using Blasphemous.Framework.UI;
using UnityEngine;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// Creates toggle options
/// </summary>
public class ToggleCreator(ModMenu menu)
{
    private readonly ModMenu _menu = menu;

    /// <summary>
    /// Adds a true/false setting to the UI
    /// </summary>
    public ToggleOption CreateOption(string name, Transform parent, Vector2 position, int size, Color color, string header)
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
            Position = new Vector2(12, 0),
            Pivot = new Vector2(0, 0.5f),
            Size = new Vector2(100, size)
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleLeft,
            Color = color,
            FontSize = size,
            Contents = header
        });

        // Create box image
        var boxImage = UIModder.Create(new RectCreationOptions()
        {
            Name = "box",
            Parent = holder,
            Position = Vector2.zero,
            Pivot = new Vector2(1, 0.5f),
            Size = new Vector2(size, size)
        }).AddImage();

        // Initialize toggle option
        var selectable = holder.gameObject.AddComponent<ToggleOption>();
        selectable.Initialize(boxImage);

        // Add click events
        _menu.AddClickable(boxImage.rectTransform, selectable.Toggle);

        return selectable;
    }
}
