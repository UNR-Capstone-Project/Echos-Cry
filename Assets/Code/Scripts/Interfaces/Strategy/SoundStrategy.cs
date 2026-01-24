using AudioSystem;
using UnityEngine;

public abstract class SoundStrategy: ScriptableObject
{
    public abstract void Execute(soundEffect sfx, Transform origin);
}
