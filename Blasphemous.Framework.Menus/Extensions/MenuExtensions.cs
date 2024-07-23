using UnityEngine;

namespace Blasphemous.Framework.Menus.Extensions;

internal static class MenuExtensions
{
    public static bool OverlapsPoint(this RectTransform rect, Vector2 point)
    {
        float xScale = (float)Screen.width / 1920;
        var scaling = new Vector3(xScale, xScale, (Screen.height - 1080 * xScale) * 0.5f);

        //Camera cam = Object.FindObjectsOfType<Camera>().First(x => x.name == "UICamera");

        var position = new Vector2(rect.position.x * scaling.x, rect.position.y * scaling.y + scaling.z);
        var size = new Vector2(rect.rect.width * scaling.x, rect.rect.height * scaling.y);

        float leftBound = position.x + size.x * -rect.pivot.x;
        float rightBound = position.x + size.x * (1 - rect.pivot.x);
        float lowerBound = position.y + size.y * -rect.pivot.y;
        float upperBound = position.y + size.y * (1 - rect.pivot.y);

        point = new Vector2(point.x * scaling.x, point.y * scaling.y);
        return point.x >= leftBound && point.x <= rightBound && point.y >= lowerBound && point.y <= upperBound;
    }
}
