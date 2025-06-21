using Blasphemous.ModdingAPI.Files;
using UnityEngine;

namespace Blasphemous.Framework.Menus;

/// <summary>
/// Loads and stores UI icons
/// </summary>
public class IconLoader
{
    /// <summary> Icon for the cursor </summary>
    public Sprite Cursor { get; }

    /// <summary> Icon for unselected toggle boxes </summary>
    public Sprite ToggleOff { get; }
    /// <summary> Icon for selected toggle boxes </summary>
    public Sprite ToggleOn { get; }
    /// <summary> Icon for disabled toggle boxes </summary>
    public Sprite ToggleNo { get; }

    /// <summary> Icon for disabled left arrow boxes </summary>
    public Sprite ArrowLeftOff { get; }
    /// <summary> Icon for disabled right arrow boxes </summary>
    public Sprite ArrowRightOff { get; }
    /// <summary> Icon for enabled left arrow boxes </summary>
    public Sprite ArrowLeftOn { get; }
    /// <summary> Icon for enabled right arrow boxes </summary>
    public Sprite ArrowRightOn { get; }

    /// <summary> Icon for unselected text boxes </summary>
    public Sprite TextOff { get; }
    /// <summary> Icon for selected text boxes </summary>
    public Sprite TextOn { get; }

    internal IconLoader(FileHandler file)
    {
        // Load cursor icon
        file.LoadDataAsSprite("cursor.png", out Sprite cursor);
        Cursor = cursor;

        // Load toggle icons
        file.LoadDataAsFixedSpritesheet("toggle.png", new Vector2(54, 54), out Sprite[] toggle);
        ToggleOff = toggle[0];
        ToggleOn = toggle[1];
        ToggleNo = toggle[2];

        // Load arrow icons
        file.LoadDataAsFixedSpritesheet("arrow.png", new Vector2(54, 54), out Sprite[] arrow);
        ArrowLeftOn = arrow[0];
        ArrowRightOn = arrow[1];
        ArrowLeftOff = arrow[2];
        ArrowRightOff = arrow[3];

        // Load text icons
        file.LoadDataAsFixedSpritesheet("text.png", new Vector2(141, 54), out Sprite[] text, new SpriteImportOptions()
        {
            Border = new Vector4(33, 21, 33, 0),
            PixelsPerUnit = 96,
        });
        TextOn = text[0];
        TextOff = text[1];
    }
}
