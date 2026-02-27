using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private string _returnSceneName;

    public void BackButton()
    {
        SceneManager.LoadScene(_returnSceneName);
    }
}
