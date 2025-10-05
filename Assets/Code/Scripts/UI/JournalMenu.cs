using System.Data;
using UnityEngine;

public enum journalTabs
{
    MAP, INVENTORY, UPGRADES, BESTIARY
}

public class JournalMenu : MonoBehaviour
{
    [SerializeField] private Canvas journalCanvas;
    [SerializeField] private journalTabs currentJournalTab = journalTabs.MAP;

    void Start()
    {
        if (journalCanvas != null)
        {
            journalCanvas.enabled = false;
            InputTranslator.OnMapEvent += showJournal;
            InputTranslator.OnExitMapEvent += hideJournal;
        }
        else
        {
            Debug.Log("Journal Canvas is null!");
        }

    }

    void OnDestroy()
    {
        InputTranslator.OnMapEvent -= showJournal;
        InputTranslator.OnExitMapEvent -= hideJournal;
    }

    private void showJournal()
    {
        journalCanvas.enabled = true;
    }

    private void hideJournal()
    {
        journalCanvas.enabled = false;
    }
}
