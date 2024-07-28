using System.Collections.Generic;
using Blasphemous.Framework.Menus.Extensions;
using UnityEngine;

namespace Blasphemous.Framework.Menus;

internal class MenuComponent : MonoBehaviour
{
    private Clickable _clickedSetting = null;

    private readonly List<Clickable> _clickables = new();
    private ICursorController _cursorController;

    private void OnEnable()
    {
        _cursorController ??= _clickables.Count > 0
            ? new RealCursor(transform)
            : new FakeCursor();

        _cursorController.UpdatePosition(Input.mousePosition);
    }

    private void OnDisable()
    {
        _clickedSetting?.OnUnclick();
        _clickedSetting = null;
    }

    private void Update()
    {
        _cursorController.UpdatePosition(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // If clicked setting is not null and allows tab, find the next one and unclick/click
        }
    }

    private void HandleClick()
    {
        _clickedSetting?.OnUnclick();
        _clickedSetting = null;

        foreach (var click in _clickables)
        {
            if (click.Rect.OverlapsPoint(Input.mousePosition))
            {
                _clickedSetting = click;
                click.OnClick();
                break;
            }
        }
    }

    public void AddClickable(RectTransform rect, bool allowTab, System.Action onClick, System.Action onUnclick)
    {
        _clickables.Add(new Clickable(rect, allowTab, onClick, onUnclick));
    }

    class Clickable(RectTransform rect, bool allowTab, System.Action onClick, System.Action onUnclick)
    {
        private readonly System.Action _onClick = onClick;
        private readonly System.Action _onUnclick = onUnclick;

        public RectTransform Rect { get; } = rect;
        public bool AllowTab { get; } = allowTab;

        internal void OnClick() => _onClick?.Invoke();

        internal void OnUnclick() => _onUnclick?.Invoke();
    }
}
