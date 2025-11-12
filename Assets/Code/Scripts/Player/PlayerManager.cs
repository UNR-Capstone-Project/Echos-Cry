using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    void Start()
    {
        playerStats.Initialize(); //Resets stats to default values
    }
}
