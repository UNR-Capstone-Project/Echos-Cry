using UnityEngine;
using AudioSystem;

[CreateAssetMenu(fileName = "soundBuilder", menuName = "Scriptable Objects/soundBuilder")]
public class soundBuilder : ScriptableObject
{
    soundEffectManager soundManager;
    soundEffect soundForBuild;
    Vector3 soundPosition = Vector3.zero;

    public void Initialize(soundEffectManager soundManager)
    {
        this.soundManager = soundManager;
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

        soundEffectPlayer player = soundManager.getPlayer();
        player.setupSoundEffect(soundForBuild);
        player.transform.position = soundPosition;
        player.transform.parent = soundManager.transform;
    }
}
