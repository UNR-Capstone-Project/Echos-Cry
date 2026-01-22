using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon _currentlyEquippedWeapon;
    public Weapon CurrentlyEquippedWeapon
    {
        get => _currentlyEquippedWeapon;
        set => _currentlyEquippedWeapon = value; 
    }

    public void PrimaryAction()
    {
        if (_currentlyEquippedWeapon == null) return;
        if(_currentlyEquippedWeapon is IPrimaryAction action)
        {
            action.PrimaryAction();
        }
    }
    public void SecondaryAction() 
    {
        if (_currentlyEquippedWeapon == null) return;
        if (_currentlyEquippedWeapon is ISecondaryAction action)
        {
            action.SecondaryAction();
        }
    }
}
