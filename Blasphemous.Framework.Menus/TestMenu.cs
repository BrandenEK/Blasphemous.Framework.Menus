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

        //RectTransform click = UIModder.Create(new RectCreationOptions()
        //{
        //    Name = "Clickable",
        //    Parent = ui,
        //});

        //click.AddImage(new ImageCreationOptions() { Color = Color.blue });
        //AddClickable(click, () => Main.MenuFramework.Log("Clicked image"));

        var toggle = OptionCreator.CreateToggleOption("test1", ui, new Vector2(0, 0), 36, Color.black, "Test toggle");
        var arrow = OptionCreator.CreateArrowOption("test2", ui, new Vector2(0, -100), 36, Color.white, "Test arrow",
        [
            "Option 1", "Option 2", "Option 3"
        ]);
    }
}
