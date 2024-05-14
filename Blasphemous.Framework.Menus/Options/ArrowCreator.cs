using Blasphemous.Framework.UI;
using UnityEngine;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// Creates arrow options
/// </summary>
public class ArrowCreator(ModMenu menu)
{
    private readonly ModMenu _menu = menu;

    /// <summary> The pixel size of the arrow images </summary>
    public int ArrowSize { get; set; } = 36;
    /// <summary> The pixel size of the header and option text </summary>
    public int TextSize { get; set; } = 36;
    /// <summary> The color of the header text </summary>
    public Color TextColor { get; set; } = new Color32(192, 192, 192, 255);
    /// <summary> The color of the selected option text </summary>
    public Color TextColorAlt { get; set; } = new Color32(255, 231, 65, 255);
    /// <summary> The pixel space in between the arrow images </summary>
    public int ElementSpacing { get; set; } = 120;

    /// <summary>
    /// Adds a multi-choice setting to the UI
    /// </summary>
    public ArrowOption CreateOption(string name, Transform parent, Vector2 position, string header, string[] options)
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
            Position = new Vector2(0, ArrowSize),
        }).AddText(new TextCreationOptions()
        {
            Alignment = TextAnchor.MiddleCenter,
            Color = TextColor,
            FontSize = TextSize,
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
            Color = TextColorAlt,
            FontSize = TextSize - 5,
            Contents = string.Empty
        });

        // Create left arrow image
        var leftArrow = UIModder.Create(new RectCreationOptions()
        {
            Name = "left",
            Parent = holder,
            Position = new Vector2(-ElementSpacing, 0),
            Size = new Vector2(ArrowSize, ArrowSize)
        }).AddImage();

        // Create right arrow image
        var rightArrow = UIModder.Create(new RectCreationOptions()
        {
            Name = "right",
            Parent = holder,
            Position = new Vector2(ElementSpacing, 0),
            Size = new Vector2(ArrowSize, ArrowSize)
        }).AddImage();

        // Initialize arrow option
        var selectable = holder.gameObject.AddComponent<ArrowOption>();
        selectable.Initialize(optionText, leftArrow, rightArrow, options);

        // Add click events
        _menu.AddClickable(leftArrow.rectTransform, () => selectable.ChangeOption(-1));
        _menu.AddClickable(rightArrow.rectTransform, () => selectable.ChangeOption(1));

        return selectable;
    }
}
