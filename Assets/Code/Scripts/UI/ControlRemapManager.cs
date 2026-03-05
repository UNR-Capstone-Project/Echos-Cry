using UnityEngine;
using UnityEngine.InputSystem;

public class ControlRemapManager : MonoBehaviour
{
    [SerializeField] private GameObject _controlsContainer;
    [SerializeField] private GameObject _remapOptionPrefab;
    [SerializeField] private InputTranslator _inputTranslator;

    private void Start()
    {
        foreach (var actionMap in _inputTranslator.PlayerInputs.asset.actionMaps)
        {
            foreach (var action in actionMap.actions)
            {
                if (action.bindings.Count == 0) continue;

                foreach (var binding in action.bindings)
                {
                    GameObject newOptionInstsance = Instantiate(_remapOptionPrefab, _controlsContainer.transform);
                    newOptionInstsance.GetComponent<RemapOptionHandler>().SetupAction(action.name, binding.ToDisplayString());
                }
            }
        }
    }
}
