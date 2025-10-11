using UnityEngine;

public abstract class ComboState
{
    public ComboState(ComboStateCache comboCache)
    {
        _comboCache = comboCache;

    }
    ~ComboState()
    {

    }
    public abstract void InitComboState();
    public abstract void ComboStateStart();
    public abstract void ComboStateExit();
    public abstract ComboState HandleLightAttackTransition();
    public abstract ComboState HandleHeavyAttackTransition();

    private ComboStateCache _comboCache;

    public Attack ComboAttack;
}
