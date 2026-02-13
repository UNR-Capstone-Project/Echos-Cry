using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _selectImage;

    private void Start()
    {
        _selectImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selectImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selectImage.SetActive(false);
    }
}
