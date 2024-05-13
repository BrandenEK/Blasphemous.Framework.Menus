using Blasphemous.Framework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// Handles creating options for mod menus
/// </summary>
public class OptionCreator
{
    private readonly ModMenu _menu;

    internal OptionCreator(ModMenu menu)
    {
        _menu = menu;
    }

    /// <summary>
    /// Adds a true/false setting to the UI
    /// </summary>
    public ToggleOption CreateToggleOption(string name, Transform parent, Vector2 position, int size, Color color, string header)
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
            Size = new Vector2(100, size),
            Pivot = new Vector2(0, 0.5f)
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
            Pivot = new Vector2(1, 0.5f),
            Size = new Vector2(size, size)
        }).AddImage(new ImageCreationOptions());

        // Initialize toggle option
        var selectable = holder.gameObject.AddComponent<ToggleOption>();
        selectable.Initialize(boxImage);

        // Add click events
        _menu.AddClickable(boxImage.rectTransform, selectable.Toggle);

        return selectable;
    }

    private static readonly Color TEXT_COLOR = Color.black;
    private const int TEXT_SIZE = 55;
}
