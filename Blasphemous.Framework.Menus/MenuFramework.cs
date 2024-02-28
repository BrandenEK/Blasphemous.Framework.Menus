using Blasphemous.ModdingAPI;

namespace Blasphemous.Framework.Menus;

public class MenuFramework : BlasMod
{
    public MenuFramework() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

#if DEBUG
    protected override void OnRegisterServices(ModServiceProvider provider)
    {
        provider.RegisterNewGameMenu(new TestMenu("Testing number 1", 10));
        provider.RegisterNewGameMenu(new TestMenu("Testing number 2", 21));

        provider.RegisterLoadGameMenu(new TestMenu("Loading game...", 50));
    }
#endif
}
