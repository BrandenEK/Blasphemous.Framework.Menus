using Blasphemous.Framework.Menus.Options;
using System;
using UnityEngine;

namespace Blasphemous.Framework.Menus;

/// <summary>
/// The abstract form of an in-game menu
/// </summary>
public abstract class ModMenu
{
    internal string Title { get; }
    internal int Priority { get; }

    /// <summary>
    /// Creates a new menu with a title and priority
    /// </summary>
    public ModMenu(string title, int priority)
    {
        Title = title;
        Priority = priority;

        OptionCreator = new OptionCreator(this);
    }

    /// <summary>
    /// Handles clickable settings and menu functionality
    /// </summary>
    internal MenuComponent UI { get; private set; }
    /// <summary>
    /// Handles creating and registering options
    /// </summary>
    protected OptionCreator OptionCreator { get; }

    /// <summary>
    /// Called when the menus are first opened
    /// </summary>
    public virtual void OnStart() { }

    /// <summary>
    /// Called when the menus are closed by starting the game
    /// </summary>
    public virtual void OnFinish() { }

    /// <summary>
    /// Called when the menus are closed by returning to the title screen
    /// </summary>
    public virtual void OnCancel() { }

    /// <summary>
    /// Called when this specific menu is opened
    /// </summary>
    public virtual void OnShow() { }

    /// <summary>
    /// Called when this specific menu is closed
    /// </summary>
    public virtual void OnHide() { }

    /// <summary>
    /// Creates the specific UI for this menu if it doesnt already exist
    /// </summary>
    internal void CreateUI(bool isFirst, bool isLast)
    {
        if (UI != null)
            return;

        UI = Main.MenuFramework.CreateBaseMenu(Title, isFirst, isLast);
        CreateUI(UI.transform.Find("Main Section"));
    }

    /// <summary>
    /// Adds all menu-specific UI to its base UI object
    /// </summary>
    protected internal abstract void CreateUI(Transform ui);

    /// <summary>
    /// Adds an event to occur whenever this object is clicked on
    /// </summary>
    protected internal void AddClickable(RectTransform rect, Action onClick) => UI.AddClickable(rect, onClick, null);

    /// <summary>
    /// Adds an event to occur whenever this object is clicked on or clicked off
    /// </summary>
    protected internal void AddClickable(RectTransform rect, Action onClick, Action onUnclick) => UI.AddClickable(rect, onClick, onUnclick);
}
