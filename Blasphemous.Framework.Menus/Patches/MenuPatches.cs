using Blasphemous.ModdingAPI;
using Gameplay.UI.Others.MenuLogic;
using Gameplay.UI.Widgets;
using HarmonyLib;
using System.Collections.Generic;

namespace Blasphemous.Framework.Menus.Patches;

/// <summary>
/// Shows the menu, or starts game if no menus or accepted
/// </summary>
[HarmonyPatch(typeof(SelectSaveSlots), nameof(SelectSaveSlots.OnAcceptSlots))]
class SelectSaveSlots_OnAcceptSlots_Patch
{
    public static bool Prefix(int idxSlot, List<SaveSlot> ___slots, bool __runOriginal)
    {
        if (!__runOriginal)
        {
            ModLog.Info("Something else skipped game start, so skip opening menus");
            return false;
        }

        return StartGameFlag || Main.MenuFramework.TryStartGame(idxSlot, !___slots[idxSlot].IsEmpty);
    }

    public static bool StartGameFlag { get; set; }
}
