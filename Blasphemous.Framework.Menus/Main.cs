using BepInEx;

namespace Blasphemous.Framework.Menus;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "3.0.0")]
[BepInDependency("Blasphemous.Framework.UI", "0.2.0")]
internal class Main : BaseUnityPlugin
{
    public static MenuFramework MenuFramework { get; private set; }

    private void Start()
    {
        MenuFramework = new MenuFramework();
    }
}
