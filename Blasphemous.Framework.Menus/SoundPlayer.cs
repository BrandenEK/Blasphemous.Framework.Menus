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
        /// <summary> SFX used on input submit </summary>
        EquipItem = 0,
        /// <summary> SFX used on input cancel </summary>
        UnequipItem = 1,
        /// <summary> SFX used on selection change </summary>
        ChangeSelection = 2,
        /// <summary> SFX used on scene change </summary>
        FadeToWhite = 3,
    }
}
