using UnityEngine;
using AudioSystem;

[CreateAssetMenu(fileName = "soundBuilder", menuName = "Scriptable Objects/soundBuilder")]
public class soundBuilder : ScriptableObject
{
    soundEffectManager soundManager;
    soundEffectPlayer currentSoundPlayer;
    soundEffect soundForBuild;
    Vector3 soundPosition = Vector3.zero;

    public void Initialize(soundEffectManager soundManager)
    {
        this.soundManager = soundManager;
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
        if (!soundEffectManager.Instance.canPlaySound(soundForBuild)) return;
        currentSoundPlayer = soundManager.getPlayer();
        currentSoundPlayer.setupSoundEffect(soundForBuild);
        currentSoundPlayer.transform.position = soundPosition;
        currentSoundPlayer.transform.parent = soundManager.transform;

        if (soundForBuild.isFrequent) soundManager.registerFrequentPlayer(currentSoundPlayer);

        currentSoundPlayer.Play();
    }
}
