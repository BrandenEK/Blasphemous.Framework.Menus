using Gameplay.UI.Others.MenuLogic;
using Gameplay.UI.Widgets;
using HarmonyLib;
using System.Collections.Generic;

namespace Blasphemous.Framework.Menus;

/// <summary>
/// Shows the menu, or starts game if no menus or accepted
/// </summary>
[HarmonyPatch(typeof(SelectSaveSlots), nameof(SelectSaveSlots.OnAcceptSlots))]
class Menu_Play_Patch
{
    public static bool Prefix(int idxSlot, List<SaveSlot> ___slots)
    {
        return StartGameFlag || Main.MenuFramework.TryStartGame(idxSlot, !___slots[idxSlot].IsEmpty);
    }

    public static bool StartGameFlag { get; set; }
}
