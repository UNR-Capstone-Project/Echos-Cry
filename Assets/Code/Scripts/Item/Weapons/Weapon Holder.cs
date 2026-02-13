using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] _weaponInventory;
    [SerializeField] private InputTranslator _inputTranslator;

    private Weapon _currentlyEquippedWeapon;
    public Weapon CurrentlyEquippedWeapon
    {
        get => _currentlyEquippedWeapon;
        set => _currentlyEquippedWeapon = value; 
    }
    public bool HasWeapon => _currentlyEquippedWeapon != null;

    private void Awake()
    {
        if (_weaponInventory.Length > 0)
        {
            _currentlyEquippedWeapon = _weaponInventory[0].GetComponent<Weapon>();
            ActivateWeapon();
        }
    }

    private void OnEnable()
    {
        _inputTranslator.OnWeaponNextEvent += NextWeapon;
    }

    private void OnDisable()
    {
        _inputTranslator.OnWeaponNextEvent -= NextWeapon;
    }

    private void ActivateWeapon()
    {
        foreach (var weapon in _weaponInventory)
        {
            weapon.SetActive(weapon.GetComponent<Weapon>() == _currentlyEquippedWeapon);
        }
    }

    private void NextWeapon()
    {
        if (_weaponInventory.Length == 0) return;

        if (!_currentlyEquippedWeapon.IsAttackEnded) return;

        int currentIndex = System.Array.IndexOf(_weaponInventory, _currentlyEquippedWeapon.gameObject);
        int nextIndex = (currentIndex + 1) % _weaponInventory.Length;
        _currentlyEquippedWeapon = _weaponInventory[nextIndex].GetComponent<Weapon>();
        ActivateWeapon();
    }

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
