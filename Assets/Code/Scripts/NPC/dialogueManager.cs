using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class dialogueManager : MonoBehaviour
{
    public static dialogueManager DialogueManagerInstance;
    public static event Action onDialogueStarted;
    public static event Action onDialogueEnded;

    public static event Action broadcastSlapjackStarting;


    [Header("Dialogue Box UI")]
    //[SerializeField] private string speaker;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueCanvas;

    [Header("Dialogue Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool isDialoguePlaying { get; private set; }
    private bool canContinueToNextLine;
    private bool interruptTextDisplayer = false;
    [SerializeField] private float typingSpeed = 0.10f;
    
    private InputAction clickAction;
    private Coroutine displayLineCoroutine;
    private Coroutine triggerCardGame;
    private bool switchToGameScene = false;
    private EventSystem currentEventSystem;

    void Awake()
    {
        //if (DialogueManagerInstance != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        DialogueManagerInstance = this;
        //DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        if (dialogueText == null) throw new Exception("Dialogue Text is null.");        
        if (dialogueCanvas == null) throw new Exception("Dialogue Canvas is null.");
        if (choices.Length == 0) throw new Exception("Choices List for UI Buttons is null.");
        if (continueIcon == null) throw new Exception("ContinueIcon UI is null.");

        //Debug.Log("Currently In Start Function no Exception thrown for choices GameObject array");

        interruptTextDisplayer = false;
        isDialoguePlaying = false;
        dialogueCanvas.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        currentEventSystem = EventSystem.current;

        clickAction = InputController.Instance.GetAction("Click");
        if (clickAction != null)
        {
            clickAction.performed += clickCheck; 
        }
        
    }

    public void EnableGameSceneSwitch()
    {
        switchToGameScene = true;
    }

    public void DisableGameSceneSwitch()
    {
        switchToGameScene = false;
    }

    private void OnEnable()
    {
        dialogueTrigger.broadcastBattleBool += EnableGameSceneSwitch;
    }

    private void OnDisable()
    {
        dialogueTrigger.broadcastBattleBool -= EnableGameSceneSwitch;
    }

    public void clickCheck(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        //interruptTextDisplayer = true;

        if (canContinueToNextLine && currentStory.currentChoices.Count == 0)
        {
            ContinueIfPossible();
        }
        
    }

    public void enterDialogueMode(TextAsset inkJSON)
    {
        //Debug.Log($"Current action map before switch: {InputController.Instance.playerInput.currentActionMap.name}");        
        currentStory = new Story(inkJSON.text);
        isDialoguePlaying = true;
        onDialogueStarted?.Invoke(); 
        InputController.Instance.PushActionMap("UI");
        //Debug.Log($"Current action map after switch: {InputController.Instance.playerInput.currentActionMap.name}");
        dialogueCanvas.SetActive(true);
        continueStory();
        
    }

    public IEnumerator exitDialogueMode()
    {
        yield return new WaitForSeconds(0.1f);
        isDialoguePlaying = false;
        dialogueCanvas.SetActive(false);
        dialogueText.text = "";
        onDialogueEnded?.Invoke();
        InputController.Instance.PopActionMap();

        if (switchToGameScene)
        {
            switchToGameScene = false;
            //InputController.Instance.PushActionMap("PlayerSlapjack");
            GameManager.Instance.loadSlapjackGameplay();
        }
    }

    public void ContinueIfPossible()
    {
        // not ready yet → do nothing
        if (!canContinueToNextLine) return;

        // choice on screen → let the choice-buttons handle it
        if (currentStory.currentChoices.Count > 0) return;

        // safe to continue the story
        continueStory();
    }

    private void continueStory()
    {
        if (currentStory.canContinue) 
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(exitDialogueMode());
            } 
            // otherwise, handle the normal case for continuing the story
            else 
            {
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else 
        {
            StartCoroutine(exitDialogueMode());
        }
    }

    private void hideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void displayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        //Debug.Log($"Current choices list length is {currentChoices.Count}");
        //Debug.Log($"The length of choices variable is: {choices.Length}");

        if (currentChoices.Count == 0)
        {
            hideChoices();
            return;
        }

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI could support: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            //Debug.Log($"Current index: {index}");
            //Debug.Log($"Active state in hierarchy: {choices[index].gameObject.activeInHierarchy}");
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(selectFirstChoice());
    }

     private IEnumerator DisplayLine(string line) 
    {
        interruptTextDisplayer = false;
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        continueIcon.SetActive(false);
        hideChoices();
        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away

            
            if (interruptTextDisplayer) 
            {
                //Debug.Log("Interrupt to finish letter displayer.");
                dialogueText.maxVisibleCharacters = line.Length;
                interruptTextDisplayer = false;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag) 
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else 
            {
                //play dialogue sound here
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        displayChoices();

        canContinueToNextLine = true;
    }

    private IEnumerator selectFirstChoice()
    {
        if (currentEventSystem == null)
            yield break;

        if (choices.Length == 0 || !choices[0].activeInHierarchy)
            yield break;

        currentEventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        currentEventSystem.SetSelectedGameObject(choices[0]);
    }

    public void makeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            continueStory();
        }
    }

}