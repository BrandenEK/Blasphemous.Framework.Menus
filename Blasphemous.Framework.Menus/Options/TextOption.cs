using UnityEngine;
using UnityEngine.UI;

namespace Blasphemous.Framework.Menus.Options;

/// <summary>
/// An option composed of text
/// </summary>
public class TextOption : MonoBehaviour
{
    private ModMenu _menu;
    private Image _underline;
    private Text _text;

    private bool _numeric;
    private bool _allowZero;
    private int _maxLength;

    private string _currentValue = string.Empty;
    private bool _selected;

    /// <summary>
    /// The current string value of the option
    /// </summary>
    public string CurrentValue
    {
        get => _currentValue.Length > 0 ? _currentValue : string.Empty;
        set
        {
            _currentValue = value;
            UpdateStatus();
        }
    }

    /// <summary>
    /// The current int value of the option
    /// </summary>
    public int CurrentNumericValue => int.TryParse(CurrentValue, out int value) ? value : 0;

    /// <summary>
    /// Updates the selected status
    /// </summary>
    public void SetSelected(bool selected)
    {
        _selected = selected;
        UpdateStatus();
    }

    private void Update()
    {
        if (!_selected)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TabToNextText();
            return;
        }

        foreach (char c in Input.inputString)
        {
            ProcessCharacter(c);
        }
    }

    private void TabToNextText()
    {
        Main.MenuFramework.LogWarning("Attempting to tab to next text option");
        var texts = FindObjectsOfType<TextOption>();

        // If only one text option, do nothing
        if (texts.Length == 0)
            return;

        for (int i = texts.Length - 1; i >= 1; i--)
        {
            // If this is a different text option, keep going
            if (texts[i] != this)
                continue;

            // Deselect this one and select the next one
            SetSelected(false);
            texts[i - 1].SetSelected(true);
            Main.MenuFramework.Log($"Tabbed to text {texts[i - 1].name}");
        }
    }

    /// <summary>
    /// Initializes the text option
    /// </summary>
    public void Initialize(ModMenu menu, Image underline, Text text, bool numeric, bool allowZero, int maxLength)
    {
        _menu = menu;
        _underline = underline;
        _text = text;

        _numeric = numeric;
        _allowZero = allowZero;
        _maxLength = maxLength;

        _currentValue = string.Empty;
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        _underline.sprite = _selected
            ? Main.MenuFramework.IconLoader.TextOn
            : Main.MenuFramework.IconLoader.TextOff;
        _text.text = _currentValue.Length > 0
            ? _currentValue
            : _selected ? string.Empty : "---";
    }

    private void ProcessCharacter(char c)
    {
        if (c == '\r' || c == '\n')
            return;

        if (c == '\b')
        {
            HandleBackspace();
            return;
        }

        if (_currentValue.Length >= _maxLength)
            return;

        if (char.IsWhiteSpace(c))
        {
            HandleWhitespace(c);
        }
        else if (!char.IsNumber(c))
        {
            HandleNonNumeric(c);
        }
        else if (c == '0')
        {
            HandleZero();
        }
        else
        {
            HandleNumber(c);
        }
    }

    void HandleBackspace()
    {
        if (_currentValue.Length == 0) // Skip if value is empty
            return;

        CurrentValue = _currentValue.Substring(0, _currentValue.Length - 1);
        _menu.OnOptionsChanged();
    }

    void HandleWhitespace(char c)
    {
        if (_currentValue.Length == 0 || _numeric) // Skip if value is empty or only numbers allowed
            return;

        CurrentValue += c;
        _menu.OnOptionsChanged();
    }

    void HandleNonNumeric(char c)
    {
        if (_numeric) // Skip if only numbers allowed
            return;

        CurrentValue += c;
        _menu.OnOptionsChanged();
    }

    void HandleZero()
    {
        if (!_allowZero && _currentValue.Length == 0) // Skip if value is empty and cant start with zero
            return;

        CurrentValue += '0';
        _menu.OnOptionsChanged();
    }

    void HandleNumber(char c)
    {
        CurrentValue += c;
        _menu.OnOptionsChanged();
    }
}
