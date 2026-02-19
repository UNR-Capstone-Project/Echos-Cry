using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;
using Unity.Cinemachine;
public class dialogueTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    private InputAction interactAction;
    public static event Action onInteractWithDialogue;
    public static event Action broadcastBattleBool;
    [SerializeField] private GameObject visualCue;
    //[SerializeField] private GameObject visualCueText;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] public bool triggerBattleAtEnd = false;

    private UnityEvent enterCameraLook;
    private UnityEvent exitCameraLook;
    [Header("Cameras")]
    [SerializeField] private CinemachineCamera dialogueCamera;
    [SerializeField] private CinemachineCamera defaultCamera;
    
    [SerializeField] private InputTranslator _inputTranslator;

    //max characters on player choice buttons is 40 characters long

    void Awake()
    {
        visualCue.SetActive(false);
        //visualCueText.SetActive(false);
        if (enterCameraLook == null) {
            enterCameraLook = new UnityEvent();
        }

        if (exitCameraLook == null)
        {    
            exitCameraLook = new UnityEvent();
        }
    }

    void Start()
    {
        //interactAction = InputController.Instance.GetAction("Interact");
        
        _inputTranslator.OnInteractEvent += interactCheck;
        
        
        enterCameraLook.AddListener(SwitchToDialogueCamera);
        exitCameraLook.AddListener(SwitchToDefaultCamera);
    }

    private void OnEnable()
    {
        dialogueManager.onDialogueEnded += cameraPassThroughEvent;
    }

    private void OnDisable()
    {
        dialogueManager.onDialogueEnded -= cameraPassThroughEvent;
    }

    void cameraPassThroughEvent()
    {
        exitCameraLook?.Invoke();
    }

    void Update()
    {
        if(playerInRange){
            visualCue.SetActive(true);
            /*if(_inputTranslator.OnInteract){
                Debug.Log(inkJSON.text);
            }*/
        }else{
            visualCue.SetActive(false);
        }
        if (dialogueManager.DialogueManagerInstance.isDialoguePlaying)
        {
            visualCue.SetActive(false);
            //visualCueText.SetActive(false);
        }
    }

    private void interactCheck()
    {
        onInteractWithDialogue?.Invoke();
        if (triggerBattleAtEnd) broadcastBattleBool?.Invoke();

        if (!dialogueManager.DialogueManagerInstance.isDialoguePlaying)
        {
            enterCameraLook?.Invoke();
            dialogueManager.DialogueManagerInstance.enterDialogueMode(inkJSON);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if (alreadyEntered) return;
        

        if (other.gameObject.CompareTag("Player"))
        {
            
            playerInRange = true;
            if (!dialogueManager.DialogueManagerInstance.isDialoguePlaying)
            {
                visualCue.SetActive(true);
                //visualCueText.SetActive(true);
            } 
            //if (oneShot) alreadyEntered = true;
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        //if (alreadyExited) return;


        if (other.gameObject.CompareTag("Player"))
        {
            //onTriggerExit.Invoke();
            playerInRange = false;   
            visualCue.SetActive(false);
            //visualCueText.SetActive(false);
            //exitCameraLook?.Invoke();
            //if (oneShot) alreadyExited = true;
        }

        
    }

    private void SwitchToDialogueCamera()
    {
        //if (dialogueCamera != null)
            //CameraSwitcher.SwitchCamera(dialogueCamera);
    }

    private void SwitchToDefaultCamera()
    {
        //if (defaultCamera != null)
            //CameraSwitcher.SwitchCamera(defaultCamera);
    }
}