
namespace Blasphemous.Framework.Menus;

public class TestMenu(string title, int priority) : ModMenu(title, priority)
{
    public override void OnShow()
    {
        Main.MenuFramework.LogWarning($"Showing {Title} menu");
    }

    public override void OnHide()
    {
        Main.MenuFramework.LogWarning($"Hiding {Title} menu");
    }
}
