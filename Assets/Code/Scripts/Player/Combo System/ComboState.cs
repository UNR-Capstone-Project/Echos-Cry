using System;
using UnityEngine;

public class ComboState 
{
    public ComboState(ComboStateMachine.StateName stateName)
    {
        StateName = stateName;
    }
    public ComboState()
    {
        NextLightAttack = null;
        NextHeavyAttack = null;
    }
    //Where to act on Attack data
    public void InitiateComboState()
    {
        //Subtract by one since any AttackData array will be 1 less than ComboState array
        OnInitiateComboEvent?.Invoke(((int) StateName) - 1);  
    }
    public ComboState NextLightAttack;
    public ComboState NextHeavyAttack;
    public ComboStateMachine.StateName StateName;
    public static event Action<int> OnInitiateComboEvent;
}
