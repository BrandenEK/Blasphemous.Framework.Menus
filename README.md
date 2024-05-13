# Blasphemous Menu Framework

<img src="https://img.shields.io/github/downloads/BrandenEK/Blasphemous.Framework.Menus/total?color=6495ED&style=for-the-badge">

---

## Usage
- Allows other mods to declare interactable menus when starting a new game or loading an existing one

## Development
Example menu:
```cs
internal class TestMenu(string title, int priority) : ModMenu(title, priority)
{
    public override void OnShow()
    {
        Main.MenuFramework.LogWarning($"Showing {Title} menu");
    }

    public override void OnHide()
    {
        Main.MenuFramework.LogWarning($"Hiding {Title} menu");
    }

    protected internal override void CreateUI(Transform ui)
    {
        // Create main ui
        UIModder.Create(new RectCreationOptions()
        {
            Name = "test",
            Parent = ui,
            XRange = new Vector2(0, 1),
            YRange = new Vector2(0, 1),
            Size = Vector2.one,
        }).AddImage(new ImageCreationOptions()
        {
            Color = new Color(1, 1, 1, 0.5f)
        });

        // Create clickable ui
        RectTransform click = UIModder.Create(new RectCreationOptions()
        {
            Name = "Clickable",
            Parent = ui,
        });

        click.AddImage(new ImageCreationOptions() { Color = Color.blue });
        AddClickable(click, () => Main.MenuFramework.Log("Clicked image"));
    }
}
```
