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
        // Load cursor
        file.LoadDataAsSprite("cursor.png", out Sprite cursor);
        Cursor = cursor;

        // Load UI
        file.LoadDataAsFixedSpritesheet("ui.png", Vector2.one * 36, out Sprite[] ui);
        ToggleOff = ui[0];
        ToggleOn = ui[1];
        ToggleNo = ui[2];
        ArrowLeftOff = ui[3];
        ArrowRightOff = ui[4];
        ArrowLeftOn = ui[5];
        ArrowRightOn = ui[6];
        TextOff = ui[7];
        TextOn = ui[8];
    }
}
