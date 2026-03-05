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
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    var binding = action.bindings[i];

                    if (binding.isComposite)
                        continue;

                    GameObject instance = Instantiate(_remapOptionPrefab, _controlsContainer.transform);

                    instance.GetComponent<RemapOptionHandler>().SetupAction(
                        action.name,
                        action.GetBindingDisplayString(i),
                        action,
                        i
                    );
                }
            }
        }
    }
}
