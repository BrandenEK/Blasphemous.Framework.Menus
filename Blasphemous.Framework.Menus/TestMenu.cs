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
        UIModder.Create(new RectCreationOptions()
        {
            Name = "test",
            Parent = ui,
            XRange = new Vector2(0, 1),
            YRange = new Vector2(0, 1),
            Size = Vector2.one,
        }).AddImage(new ImageCreationOptions()
        {
            Color = new Color(1, 1, 1, 0.5f)
        });

        if (!hasClickable)
            return;

        var creator = new OptionCreator(this);

        var toggle = creator.CreateToggleOption("test1", ui, new Vector2(0, 0), 36, Color.black, "Test toggle");
        var arrow = creator.CreateArrowOption("test2", ui, new Vector2(0, -100), 36, Color.white, "Test arrow",
        [
            "Option 1", "Option 2", "Option 3"
        ]);
        var text = creator.CreateTextOption("test3", ui, new Vector2(0, 100), 36, Color.white, 300, "Test text", false, true, 16);
    }
}
