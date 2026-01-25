using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Strategies/Attack/AOE")]

public class AOEAttackStrategy : AttackStrategy
{
    [SerializeField] GameObject _aoeObject;
    public override bool Execute(float damage, Vector3 direction, Transform origin)
    {
        Instantiate(_aoeObject).transform.position = origin.position;
        return true;
    }
}
