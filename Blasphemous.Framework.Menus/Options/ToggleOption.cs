using UnityEngine;
using UnityEngine.UI;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// An option that can either be on or off
/// </summary>
public class ToggleOption : MonoBehaviour
{
    private Image _toggleBox;

    private bool _toggled;

    /// <summary>
    /// Whether or not the option is toggled on
    /// </summary>
    public bool Toggled
    {
        get => _toggled;
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
        Toggled = !Toggled;
        //Main.Randomizer.AudioHandler.PlayEffectUI(UISFX.ChangeSelection);
    }

    /// <summary>
    /// Initializes the toggle option
    /// </summary>
    public void Initialize(Image toggleBox)
    {
        _toggleBox = toggleBox;
    }

    private void UpdateStatus()
    {
        _toggleBox.sprite = _toggled
            ? Main.MenuFramework.IconLoader.ToggleOn
            : Main.MenuFramework.IconLoader.ToggleOff;
    }
}
