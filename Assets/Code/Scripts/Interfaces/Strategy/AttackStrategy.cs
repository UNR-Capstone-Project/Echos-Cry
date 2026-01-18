
using System;
using UnityEngine;

[Serializable]
public abstract class AttackStrategy : ScriptableObject
{
    //Return true if hit player, else return false
    public abstract bool Execute(float damage, Vector3 direction, Transform origin);
}