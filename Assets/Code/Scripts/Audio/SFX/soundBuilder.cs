using UnityEngine;
using AudioSystem;
/// Original Author: Victor
/// All Contributors Since Creation: Victor, Michael
/// Last Modified By: Michael
/// 
/// <summary>
/// helper object that builds out the sound and player for soundEffectManager to use properly 
/// </summary>

public class soundBuilder 
{
    SoundEffectManager soundManager;
    soundEffectPlayer currentSoundPlayer;
    soundEffect soundForBuild;
    Vector3 soundPosition = Vector3.zero;

    public void Initialize(SoundEffectManager soundManager)
    {
        this.soundManager = soundManager;
        currentSoundPlayer = soundManager.GetPlayer();
    }

    public soundEffectPlayer GetSoundPlayer()
    {
        return currentSoundPlayer;
    }

    public soundBuilder setSound(soundEffect sound)
    {
        soundForBuild = sound;
        return this;
    }

    public soundBuilder setSoundPosition(Vector3 position)
    {
        soundPosition = position;
        return this;
    }

    public void ValidateAndPlaySound()
    {
        if (!SoundEffectManager.Instance.CanPlaySound(soundForBuild)) return;
        currentSoundPlayer = soundManager.GetPlayer();
        currentSoundPlayer.setupSoundEffect(soundForBuild);
        currentSoundPlayer.transform.position = soundPosition;
        currentSoundPlayer.transform.parent = soundManager.transform;

        if (soundForBuild.isFrequent) soundManager.RegisterFrequentPlayer(currentSoundPlayer);

        currentSoundPlayer.Play();
    }
}
