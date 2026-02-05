using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon _currentlyEquippedWeapon;
    public Weapon CurrentlyEquippedWeapon
    {
        get => _currentlyEquippedWeapon;
        set => _currentlyEquippedWeapon = value; 
    }
    public bool HasWeapon => _currentlyEquippedWeapon != null;

    public void PrimaryAction()
    {
        if (_currentlyEquippedWeapon == null) return;
        _currentlyEquippedWeapon.PrimaryAction();
    }
    public void SecondaryAction() 
    {
        if (_currentlyEquippedWeapon == null) return;
        _currentlyEquippedWeapon.SecondaryAction();
    }

    public bool IsActionEnded()
    {
        return _currentlyEquippedWeapon.IsAttackEnded;
    }
}
