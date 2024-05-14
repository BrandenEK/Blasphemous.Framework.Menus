using Blasphemous.Framework.Menus.Options;
using Blasphemous.Framework.UI;
using UnityEngine;

namespace Blasphemous.Framework.Menus;

internal class TestMenu(string title, int priority, bool hasClickable) : ModMenu(title, priority)
{
    public override void OnShow()
    {
        Main.MenuFramework.LogWarning($"Showing {Title} menu");
    }

    public override void OnHide()
    {
        Main.MenuFramework.LogWarning($"Hiding {Title} menu");
    }

    protected internal override void CreateUI(Transform ui)
    {
        // Create ui
        //UIModder.Create(new RectCreationOptions()
        //{
        //    Name = "test",
        //    Parent = ui,
        //    XRange = new Vector2(0, 1),
        //    YRange = new Vector2(0, 1),
        //    Size = Vector2.one,
        //}).AddImage(new ImageCreationOptions()
        //{
        //    Color = new Color(1, 1, 1, 0.5f)
        //});

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
            ArrowSize = 55,
            TextSize = 55,
            ElementSpacing = 180,
            TextColorAlt = Color.cyan
        };

        string[] options = [ "Option 1", "Option 2", "Option 3" ];
        var arrow1 = defaultArrow.CreateOption("test2", ui, new Vector2(0, 0), "Test arrow", options);
        var arrow2 = specialArrow.CreateOption("test22", ui, new Vector2(0, 200), "Test arrow", options);

        // Texts

        var textCreator = new TextCreator(this);
        var text = textCreator.CreateOption("test3", ui, new Vector2(500, 0), 36, Color.white, 300, "Test text", false, true, 16);
    }
}
