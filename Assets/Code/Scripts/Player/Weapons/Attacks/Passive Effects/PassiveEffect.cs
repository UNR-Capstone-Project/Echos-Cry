using System.Collections;
using UnityEngine;

public abstract class PassiveEffect : ScriptableObject
{
    public bool isEffectOneTime = false;
    public float effectUseInterval;
    public float effectDuration;
}