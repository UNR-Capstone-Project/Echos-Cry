using UnityEngine;
using AudioSystem;

[CreateAssetMenu(fileName = "soundBuilder", menuName = "Scriptable Objects/soundBuilder")]
public class soundBuilder : ScriptableObject
{
    soundEffectManager soundManager;
    soundEffect soundForBuild;
    Vector3 soundPosition = Vector3.zero;

    public soundBuilder(soundEffectManager soundManager)
    {
        this.soundManager = soundManager;
    }

    public soundBuilder setSound(soundEffect sound)
    {
        this.soundForBuild = sound;
        return this;
    }

    public soundBuilder setSoundPosition(Vector3 position)
    {
        this.soundPosition = position;
        return this;
    }
    
    public void ValidateAndPlaySound()
    {
        soundEffectPlayer player = soundManager.getPlayer();
        player.setupSoundEffect(soundForBuild);
        player.transform.position = soundPosition;
        player.transform.parent = soundManager.transform;
    }
}
