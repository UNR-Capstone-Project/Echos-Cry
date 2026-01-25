using UnityEngine;
using AudioSystem;
/// Original Author: Victor
/// All Contributors Since Creation: Victor, Michael
/// Last Modified By: Michael
/// 
/// <summary>
/// helper object that builds out the sound and player for soundEffectManager to use properly 
/// </summary>

public class SoundBuilder 
{
    SoundEffectManager soundManager;
    SoundEffectPlayer currentSoundPlayer;
    soundEffect soundForBuild;
    Vector3 soundPosition = Vector3.zero;
    float soundDelay = 0;

    public void Initialize(SoundEffectManager soundManager)
    {
        this.soundManager = soundManager;
        currentSoundPlayer = soundManager.GetPlayer();
    }

    public SoundEffectPlayer GetSoundPlayer()
    {
        return currentSoundPlayer;
    }

    public SoundBuilder SetSound(soundEffect sound)
    {
        soundForBuild = sound;
        return this;
    }

    public SoundBuilder SetSoundPosition(Vector3 position)
    {
        soundPosition = position;
        return this;
    }

    public SoundBuilder SetDelay(float delay)
    {
        soundDelay = delay;
        return this;
    }

    public void ValidateAndPlaySound()
    {
        if (!SoundEffectManager.Instance.CanPlaySound(soundForBuild)) return;
        currentSoundPlayer = soundManager.GetPlayer();
        currentSoundPlayer.SetupSoundEffect(soundForBuild);
        currentSoundPlayer.transform.position = soundPosition;
        currentSoundPlayer.transform.parent = soundManager.transform;

        if (soundForBuild.isFrequent) soundManager.RegisterFrequentPlayer(currentSoundPlayer);

        if(soundDelay > 0) currentSoundPlayer.DelayedPlay(soundDelay);
        else currentSoundPlayer.Play();
    }
}
