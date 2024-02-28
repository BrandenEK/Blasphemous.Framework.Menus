using BepInEx;

namespace Blasphemous.Framework.Menus;

[BepInPlugin(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_VERSION)]
[BepInDependency("Blasphemous.ModdingAPI", "2.1.1")]
[BepInDependency("Blasphemous.Framework.UI", "0.1.0")]
public class Main : BaseUnityPlugin
{
    public static MenuFramework MenuFramework { get; private set; }

    private void Start()
    {
        MenuFramework = new MenuFramework();
    }
}
