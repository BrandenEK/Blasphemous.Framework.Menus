using Gameplay.UI.Others.MenuLogic;
using HarmonyLib;

namespace Blasphemous.Framework.Menus;

/// <summary>
/// Shows the menu, or starts game if no menus or accepted
/// </summary>
[HarmonyPatch(typeof(NewMainMenu), "InternalPlay")]
class Menu_Play_Patch
{
    public static bool Prefix(bool ___isContinue)
    {
        return StartGameFlag || Main.MenuFramework.TryStartGame(___isContinue);
    }

    public static bool StartGameFlag { get; set; }
}
