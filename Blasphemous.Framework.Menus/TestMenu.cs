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

        var defaultToggle = new ToggleCreator(this);
        var specialToggle = new ToggleCreator(this)
        {
            BoxSize = 70,
            TextSize = 70
        };
        var arrowCreator = new ArrowCreator(this);
        var textCreator = new TextCreator(this);

        var toggle1 = defaultToggle.CreateOption("test1", ui, new Vector2(0, 0), "Test toggle");
        var toggle2 = specialToggle.CreateOption("test11", ui, new Vector2(0, 100), "Test toggle");


        var arrow = arrowCreator.CreateOption("test2", ui, new Vector2(0, -200), 36, Color.white, "Test arrow",
        [
            "Option 1", "Option 2", "Option 3"
        ]);
        var text = textCreator.CreateOption("test3", ui, new Vector2(0, -300), 36, Color.white, 300, "Test text", false, true, 16);
    }
}
