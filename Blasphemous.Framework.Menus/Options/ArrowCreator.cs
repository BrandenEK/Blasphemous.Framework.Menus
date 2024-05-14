using Blasphemous.Framework.UI;
using UnityEngine;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// Creates arrow options
/// </summary>
public class ArrowCreator(ModMenu menu)
{
    private readonly ModMenu _menu = menu;

    /// <summary>
    /// Adds a multi-choice setting to the UI
    /// </summary>
    public ArrowOption CreateOption(string name, Transform parent, Vector2 position, int size, Color color, string header, string[] options)
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
        }).AddImage();

        // Create right arrow image
        var rightArrow = UIModder.Create(new RectCreationOptions()
        {
            Name = "right",
            Parent = holder,
            Position = new Vector2(150, 5),
            Size = new Vector2(size, size)
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
