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

    private dialogueManager dialogueManagerInstance;
    //max characters on player choice buttons is 40 characters long

    void Awake()
    {
        //dialogueManagerInstance = dialogueManager.Instance;
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

    void Start(){
    dialogueManagerInstance = dialogueManager.Instance;

    if (dialogueManagerInstance == null)
    {
        dialogueManagerInstance = FindObjectOfType<dialogueManager>();
    }

    if (dialogueManagerInstance == null)
    {
        Debug.LogError("DialogueManager instance not found in scene!");
        return;
    }

    if (_inputTranslator != null)
    {
        _inputTranslator.OnInteractEvent += interactCheck;
    }

    enterCameraLook.AddListener(SwitchToDialogueCamera);
    exitCameraLook.AddListener(SwitchToDefaultCamera);
}

    private void OnEnable()
    {
        //dialogueManager.onDialogueEnded += cameraPassThroughEvent;
    }

    private void OnDisable()
    {
        //dialogueManager.onDialogueEnded -= cameraPassThroughEvent;
    }

    void cameraPassThroughEvent()
    {
        exitCameraLook?.Invoke();
    }

    void Update()
    {
        if(dialogueManagerInstance == null){
            return;
        }
        if(playerInRange && !dialogueManagerInstance.isDialoguePlaying){
            visualCue.SetActive(true);
            /*if(_inputTranslator.OnInteract){
                Debug.Log(inkJSON.text);
            }*/
            //dialogueManagerInstance.enterDialogueMode(inkJSON);
        }else{
            visualCue.SetActive(false);
            //dialogueManagerInstance.exitDialogueMode();
        }
    }

    private void interactCheck()
    {
        if (dialogueManagerInstance == null){
            return;
        }
        if (playerInRange && !dialogueManagerInstance.isDialoguePlaying)
        {
            //enterCameraLook?.Invoke();
            dialogueManagerInstance.enterDialogueMode(inkJSON);
            onInteractWithDialogue?.Invoke();
            /*if (triggerBattleAtEnd)
                broadcastBattleBool?.Invoke();*/
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            playerInRange = true;
            if (!dialogueManagerInstance.isDialoguePlaying)
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
            if (dialogueManagerInstance.isDialoguePlaying)
            {
                StartCoroutine(dialogueManagerInstance.exitDialogueMode());
            }
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