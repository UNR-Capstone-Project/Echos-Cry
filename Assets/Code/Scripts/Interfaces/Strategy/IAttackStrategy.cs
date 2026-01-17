
using UnityEngine;

public interface IAttackStrategy 
{
    //Return true if hit player, else return false
    public bool Execute(float damage, Vector3 direction, float distance, Transform origin);
}