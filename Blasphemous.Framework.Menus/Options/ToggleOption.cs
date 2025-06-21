using UnityEngine;
using UnityEngine.UI;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// An option that can either be on or off
/// </summary>
public class ToggleOption : MonoBehaviour
{
    private ModMenu _menu;
    private Image _toggleBox;

    private bool _enabled = true;
    private bool _toggled = false;

    /// <summary>
    /// Whether or not the option is able to be selected
    /// </summary>
    public bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            UpdateStatus();
        }
    }

    /// <summary>
    /// Whether or not the option is toggled on
    /// </summary>
    public bool Toggled
    {
        get => _toggled && _enabled;
        set
        {
            _toggled = value;
            UpdateStatus();
        }
    }

    /// <summary>
    /// Changes the toggled status
    /// </summary>
    public void Toggle()
    {
        if (!Enabled)
            return;

        Toggled = !Toggled;

        _menu.OnOptionsChanged(name);
    }

    /// <summary>
    /// Initializes the toggle option
    /// </summary>
    public void Initialize(ModMenu menu, Image toggleBox)
    {
        _menu = menu;
        _toggleBox = toggleBox;

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        _toggleBox.sprite = _enabled
            ? _toggled
                ? Main.MenuFramework.IconLoader.ToggleOn
                : Main.MenuFramework.IconLoader.ToggleOff
            : Main.MenuFramework.IconLoader.ToggleNo;
    }
}
