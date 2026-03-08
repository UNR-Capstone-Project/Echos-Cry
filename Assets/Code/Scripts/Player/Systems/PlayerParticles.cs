using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    //Particles
    [SerializeField] private ParticleSystem _experienceParticles;

    //Channels
    [SerializeField] private IntEventChannel _levelUpChannel;

    private void OnEnable()
    {
        _levelUpChannel.Channel += StartExperienceParticles;
    }
    private void OnDisable()
    {
        _levelUpChannel.Channel -= StartExperienceParticles;
    }

    private void StartExperienceParticles(int level)
    {
        _experienceParticles.Play();
    }
}
