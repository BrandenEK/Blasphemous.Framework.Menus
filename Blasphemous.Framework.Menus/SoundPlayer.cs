using Framework.Managers;

namespace Blasphemous.Framework.Menus;

/// <summary>
/// Plays sound effects
/// </summary>
public class SoundPlayer
{
    /// <summary>
    /// Plays the specified sound effect
    /// </summary>
    public void Play(SfxType sfx)
    {
        Core.Audio.PlayOneShot($"event:/SFX/UI/{sfx}");
    }

    /// <summary>
    /// The type of sound effect to play
    /// </summary>
    public enum SfxType
    {
        EquipItem = 0,
        UnequipItem = 1,
        ChangeSelection = 2,
        FadeToWhite = 3,
    }
}
