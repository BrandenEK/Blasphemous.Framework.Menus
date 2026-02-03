using Blasphemous.Framework.Menus.Patches;
using Blasphemous.Framework.UI;
using Blasphemous.ModdingAPI;
using Framework.Managers;
using Gameplay.UI.Others;
using Gameplay.UI.Others.MenuLogic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

namespace Blasphemous.Framework.Menus;

/// <summary>
/// Handles adding and displaying in-game menus
/// </summary>
public class MenuFramework : BlasMod
{
    internal MenuFramework() : base(ModInfo.MOD_ID, ModInfo.MOD_NAME, ModInfo.MOD_AUTHOR, ModInfo.MOD_VERSION) { }

    /// <summary>
    /// Loads and stores UI icons
    /// </summary>
    public IconLoader IconLoader { get; private set; }
    /// <summary>
    /// Plays sound effects
    /// </summary>
    public SoundPlayer SoundPlayer { get; private set; }

    private MenuCollection _newGameMenus;
    private MenuCollection _loadGameMenus;
    private MenuCollection CurrentMenuCollection => _isContinue ? _loadGameMenus : _newGameMenus;
    private bool IsMenuActive => CurrentMenuCollection.IsActive;

    private bool _enterNextFrame = false;
    private bool _isContinue = false;
    private int _currentSlot = 0;

    /// <summary>
    /// Load and setup ui
    /// </summary>
    protected override void OnInitialize()
    {
        IconLoader = new IconLoader(FileHandler);
        SoundPlayer = new SoundPlayer();

        LocalizationHandler.RegisterDefaultLanguage("en");
    }

    /// <summary>
    /// Initialize the menu collections with all registered menus
    /// </summary>
    protected override void OnAllInitialized()
    {
        _newGameMenus = new MenuCollection(MenuRegister.NewGameMenus, OnFinishMenu, OnCancelMenu);
        _loadGameMenus = new MenuCollection(MenuRegister.LoadGameMenus, OnFinishMenu, OnCancelMenu);
    }

    /// <summary>
    /// Force close the menus whenever unloading the main menu
    /// </summary>
    protected override void OnLevelUnloaded(string oldLevel, string newLevel)
    {
        if (oldLevel == "MainMenu")
            CurrentMenuCollection.ForceClose();
    }

    /// <summary>
    /// Finish all new game menus once data has been reset
    /// </summary>
    protected override void OnNewGame() => _newGameMenus.DelayedFinish();

    /// <summary>
    /// Finish all load game menus once data has been reset
    /// </summary>
    protected override void OnLoadGame() => _loadGameMenus.DelayedFinish();

    /// <summary>
    /// Opens the next menu in the queue, or starts the game
    /// </summary>
    public void ShowNextMenu()
    {
        _enterNextFrame = true;
    }

    /// <summary>
    /// Opens the previous menu in the queue, or returns to the main menu
    /// </summary>
    public void ShowPreviousMenu()
    {
        CurrentMenuCollection.ShowPreviousMenu();
    }

    /// <summary>
    /// Updates the current menu
    /// </summary>
    protected override void OnUpdate()
    {
        if (!IsMenuActive)
            return;

        if (_enterNextFrame)
        {
            _enterNextFrame = false;
            CurrentMenuCollection.ShowNextMenu();
        }

        CurrentMenuCollection.CurrentMenu.OnUpdate();
    }

    /// <summary>
    /// If there are menus, start the menu process, otherwise, continue normally
    /// </summary>
    internal bool TryStartGame(int slot, bool isContinue)
    {
        _currentSlot = slot;
        _isContinue = isContinue;

        if (CurrentMenuCollection.IsEmpty)
            return true;

        if (_isContinue)
            Core.Persistence.LoadGameWithOutRespawn(slot);

        StartMenu();
        return false;
    }

    /// <summary>
    /// Whenever new game or continue is pressed, open the menus
    /// </summary>
    private void StartMenu()
    {
        SlotsMenu.gameObject.SetActive(false);
        CurrentMenuCollection.StartMenu();
    }

    /// <summary>
    /// Whenever 'A' is pressed on the final menu, actually start the game
    /// </summary>
    private void OnFinishMenu()
    {
        SelectSaveSlots_OnAcceptSlots_Patch.StartGameFlag = true;
        SlotsMenu.gameObject.SetActive(true);
        SlotsMenu.OnAcceptSlots(_currentSlot);
        SelectSaveSlots_OnAcceptSlots_Patch.StartGameFlag = false;
    }

    /// <summary>
    /// Whenever 'B' is pressed on the first menu, return to slots screen
    /// </summary>
    private void OnCancelMenu()
    {
        SlotsMenu.gameObject.SetActive(true);
    }

    /// <summary>
    /// Creates a new empty menu UI
    /// </summary>
    internal MenuComponent CreateBaseMenu(string title, bool isFirst, bool isLast)
    {
        // Duplicate slot menu
        GameObject settingsMenu = Object.Instantiate(SlotsMenu.gameObject, UIModder.Parents.CanvasHighRes);
        settingsMenu.name = $"Menu {title}";
        RescaleUIElements(settingsMenu);

        // Remove slot menu stuff
        Object.Destroy(settingsMenu.GetComponent<SelectSaveSlots>());
        Object.Destroy(settingsMenu.GetComponent<KeepFocus>());
        Object.Destroy(settingsMenu.GetComponent<CanvasGroup>());
        int childrenCount = settingsMenu.transform.childCount;
        for (int i = 0; i < childrenCount; i++)
        {
            if (i != 0 && i != 1 && i != 3)
                Object.Destroy(settingsMenu.transform.GetChild(i).gameObject);
        }

        // Set header text
        Text headerText = settingsMenu.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        headerText.text = title;

        // Set 'A' button text
        Text aButtonText = settingsMenu.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();
        aButtonText.text = LocalizationHandler.Localize(isLast ? (_isContinue ? "btncnt" : "btnbgn") : "btnnxt");

        // Set 'B' button text
        Text bButtonText = settingsMenu.transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>();
        bButtonText.text = LocalizationHandler.Localize(isFirst ? "btncnc" : "btnprv");

        // Destroy all localize components in children
        foreach (Localize loc in settingsMenu.GetComponentsInChildren<Localize>())
            Object.Destroy(loc);

        // Create holder for options and all settings
        RectTransform main = UIModder.Create(new RectCreationOptions()
        {
            Name = "Main Section",
            Parent = settingsMenu.transform,
            Pivot = new Vector2(0.5f, 0.5f),
            XRange = new Vector2(0, 1),
            YRange = new Vector2(0, 1),
            Size = new Vector2(1, 1)
        });
        main.offsetMin = new Vector2(200, 150);
        main.offsetMax = new Vector2(-200, -250);

        return settingsMenu.AddComponent<MenuComponent>();
    }

    /// <summary>
    /// When moving to the HighRes canvas, everything needs to be scaled up by 3
    /// </summary>
    private void RescaleUIElements(GameObject parent)
    {
        foreach (RectTransform rect in parent.GetComponentsInChildren<RectTransform>())
        {
            rect.sizeDelta *= 3;
            rect.anchoredPosition *= 3;

            Text t = rect.GetComponent<Text>();
            if (t != null)
                t.fontSize *= 3;
        }
    }

    /// <summary>
    /// Register test menus
    /// </summary>
    protected override void OnRegisterServices(ModServiceProvider provider)
    {
        //provider.RegisterNewGameMenu(new TestMenu("Testing number 1", 10, true));
        //provider.RegisterNewGameMenu(new TestMenu("Testing number 2", 21, true));
        //provider.RegisterLoadGameMenu(new TestMenu("Loading game...", 50, false));
    }

    private SelectSaveSlots x_slotsMenu;
    private SelectSaveSlots SlotsMenu
    {
        get
        {
            if (x_slotsMenu == null)
                x_slotsMenu = Object.FindObjectOfType<SelectSaveSlots>();
            return x_slotsMenu;
        }
    }
}
