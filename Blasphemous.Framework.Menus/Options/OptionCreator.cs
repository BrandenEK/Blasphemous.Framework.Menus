using Blasphemous.Framework.UI;
using TMPro;
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
        }).AddImage(new ImageCreationOptions());

        // Initialize toggle option
        var selectable = holder.gameObject.AddComponent<ToggleOption>();
        selectable.Initialize(boxImage);

        // Add click events
        _menu.AddClickable(boxImage.rectTransform, selectable.Toggle);

        return selectable;
    }

    /// <summary>
    /// Adds a multi-choice setting to the UI
    /// </summary>
    public ArrowOption CreateArrowOption(string name, Transform parent, Vector2 position, int size, Color color, string header, string[] options)
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
            Position = new Vector2(0, size),
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleCenter,
            Color = color,
            FontSize = size,
            Contents = header
        });

        // Create option text
        var optionText = UIModder.Create(new RectCreationOptions()
        {
            Name = "option",
            Parent = holder,
            Position = Vector2.zero,
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleCenter,
            Color = Color.yellow,
            FontSize = size - 5,
            Contents = string.Empty
        });

        // Create left arrow image
        var leftArrow = UIModder.Create(new RectCreationOptions()
        {
            Name = "left",
            Parent = holder,
            Position = new Vector2(-150, 5),
            Size = new Vector2(size, size)
        }).AddImage(new ImageCreationOptions());

        // Create right arrow image
        var rightArrow = UIModder.Create(new RectCreationOptions()
        {
            Name = "right",
            Parent = holder,
            Position = new Vector2(150, 5),
            Size = new Vector2(size, size)
        }).AddImage(new ImageCreationOptions());

        // Initialize arrow option
        var selectable = holder.gameObject.AddComponent<ArrowOption>();
        selectable.Initialize(optionText, leftArrow, rightArrow, options);

        // Add click events
        _menu.AddClickable(leftArrow.rectTransform, () => selectable.ChangeOption(-1));
        _menu.AddClickable(rightArrow.rectTransform, () => selectable.ChangeOption(1));

        return selectable;
    }

    /// <summary>
    /// Adds a text-entry setting to the UI
    /// </summary>
    public TextOption CreateTextOption(string name, Transform parent, Vector2 position, int size, Color color, int lineSize, string header, bool numeric, bool allowZero, int max)
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
        }).AddImage(new ImageCreationOptions());

        // Initialize text option
        var selectable = holder.gameObject.AddComponent<TextOption>();
        selectable.Initialize(underline, valueText, numeric, allowZero, max);

        // Add click events
        _menu.AddClickable(underline.rectTransform, () => selectable.SetSelected(true), () => selectable.SetSelected(false));

        return selectable;
    }

    private static readonly Color TEXT_COLOR = Color.black;
    private const int TEXT_SIZE = 55;
}
