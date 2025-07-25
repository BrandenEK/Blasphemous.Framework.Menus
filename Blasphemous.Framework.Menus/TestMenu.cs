﻿using Blasphemous.Framework.Menus.Options;
using Blasphemous.ModdingAPI;
using UnityEngine;

namespace Blasphemous.Framework.Menus;

internal class TestMenu(string title, int priority, bool hasClickable) : ModMenu
{
    protected internal override int Priority { get; } = priority;

    public override void OnShow()
    {
        ModLog.Warn($"Showing {title} menu");
    }

    public override void OnHide()
    {
        ModLog.Warn($"Hiding {title} menu");
    }

    public override void OnOptionsChanged(string option)
    {
        base.OnOptionsChanged(option);

        ModLog.Info($"An option {option} was changed!");
    }

    protected internal override void CreateUI(Transform ui)
    {
        if (!hasClickable)
            return;

        // Toggles

        var defaultToggle = new ToggleCreator(this);
        var specialToggle = new ToggleCreator(this)
        {
            BoxSize = 70,
            TextSize = 70
        };

        var toggle1 = defaultToggle.CreateOption("test1", ui, new Vector2(-500, 0), "Test toggle");
        var toggle2 = specialToggle.CreateOption("test11", ui, new Vector2(-500, 200), "Test toggle");

        // Arrows

        var defaultArrow = new ArrowCreator(this);
        var specialArrow = new ArrowCreator(this)
        {
            ArrowSize = 54,
            TextSize = 54,
            ElementSpacing = 180,
            TextColorAlt = Color.cyan
        };

        string[] options = [ "Option 1", "Option 2", "Option 3" ];
        var arrow1 = defaultArrow.CreateOption("test2", ui, new Vector2(0, 0), "Test arrow", options);
        var arrow2 = specialArrow.CreateOption("test22", ui, new Vector2(0, 200), "Test arrow", options);

        // Texts

        var defaultText = new TextCreator(this);
        var specialText = new TextCreator(this)
        {
            LineSize = 50,
            TextSize = 20,
            ElementSpacing = 30,
            TextColor = Color.black
        };

        var text1 = defaultText.CreateOption("test3", ui, new Vector2(500, 0), "Test text", false, true, 16);
        var text2 = specialText.CreateOption("test33", ui, new Vector2(500, 200), "Test text", false, true, 16);
    }
}
