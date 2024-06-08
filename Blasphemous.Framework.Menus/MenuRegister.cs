using Blasphemous.ModdingAPI;
using System.Collections.Generic;

namespace Blasphemous.Framework.Menus;

/// <summary> Registers a new menu </summary>
public static class MenuRegister
{
    private static readonly List<ModMenu> _newMenus = new();
    private static readonly List<ModMenu> _loadMenus = new();

    internal static IEnumerable<ModMenu> NewGameMenus => _newMenus;
    internal static IEnumerable<ModMenu> LoadGameMenus => _loadMenus;

    /// <summary> Registers a menu to appear before starting a new game </summary>
    public static void RegisterNewGameMenu(this ModServiceProvider provider, ModMenu menu)
    {
        if (provider == null)
            return;

        Main.MenuFramework.Log($"Registered new game menu: {menu.GetType().Name}");
        menu.OwnerMod = provider.RegisteringMod;
        _newMenus.Add(menu);
    }

    /// <summary> Registers a menu to appear before loading an existing game </summary>
    public static void RegisterLoadGameMenu(this ModServiceProvider provider, ModMenu menu)
    {
        if (provider == null)
            return;

        Main.MenuFramework.Log($"Registered load game menu: {menu.GetType().Name}");
        menu.OwnerMod = provider.RegisteringMod;
        _loadMenus.Add(menu);
    }
}
