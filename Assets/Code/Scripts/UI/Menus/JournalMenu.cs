using System.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum journalTabs
{
    MAP, ITEMS, UPGRADES, BESTIARY
}

public class JournalMenu : MonoBehaviour
{
    [SerializeField] private Canvas journalCanvas;
    [SerializeField] private journalTabs currentJournalTab = journalTabs.MAP;
    [SerializeField] private Image selectedJournalTab;
    [SerializeField] private TextMeshProUGUI mapText;
    [SerializeField] private TextMeshProUGUI itemsText;
    [SerializeField] private TextMeshProUGUI upgradesText;
    [SerializeField] private TextMeshProUGUI beastiaryText;
    private Vector2 currentPos;
    private Vector2 originPos;

    void Start()
    {
        if (journalCanvas != null)
        {
            journalCanvas.enabled = false;
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
            originPos = currentPos;
            InputTranslator.OnMapEvent += showJournal;
            InputTranslator.OnExitMapEvent += hideJournal;
            InputTranslator.OnJournalLeftInput += switchTabLeft;
            InputTranslator.OnJournalRightInput += switchTabRight;
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
        InputTranslator.OnJournalLeftInput -= switchTabLeft;
        InputTranslator.OnJournalRightInput -= switchTabRight;
    }

    private void showJournal()
    {
        journalCanvas.enabled = true;
    }

    private void hideJournal()
    {
        journalCanvas.enabled = false;
    }

    private void switchTabLeft()
    {
        if (currentJournalTab == journalTabs.MAP)
        {
            currentJournalTab = journalTabs.BESTIARY;
            selectedJournalTab.rectTransform.anchoredPosition = new Vector2(currentPos.x + 990, originPos.y);
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
        }
        else if (currentJournalTab == journalTabs.ITEMS)
        {
            currentJournalTab = journalTabs.MAP;
            selectedJournalTab.rectTransform.anchoredPosition = new Vector2(currentPos.x - 330, currentPos.y);
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
        }
        else if (currentJournalTab == journalTabs.UPGRADES)
        {
            currentJournalTab = journalTabs.ITEMS;
            selectedJournalTab.rectTransform.anchoredPosition = new Vector2(currentPos.x - 330, currentPos.y);
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
        }
        else if (currentJournalTab == journalTabs.BESTIARY)
        {
            currentJournalTab = journalTabs.UPGRADES;
            selectedJournalTab.rectTransform.anchoredPosition = new Vector2(currentPos.x - 330, currentPos.y);
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
        }
    }

    private void switchTabRight() {
        if (currentJournalTab == journalTabs.MAP)
        {
            currentJournalTab = journalTabs.ITEMS;
            selectedJournalTab.rectTransform.anchoredPosition = new Vector2(currentPos.x + 330, currentPos.y);
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
        }
        else if (currentJournalTab == journalTabs.ITEMS)
        {
            currentJournalTab = journalTabs.UPGRADES;
            selectedJournalTab.rectTransform.anchoredPosition = new Vector2(currentPos.x + 330, currentPos.y);
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
        }
        else if (currentJournalTab == journalTabs.UPGRADES)
        {
            currentJournalTab = journalTabs.BESTIARY;
            selectedJournalTab.rectTransform.anchoredPosition = new Vector2(currentPos.x + 330, currentPos.y);
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
        }
        else if (currentJournalTab == journalTabs.BESTIARY)
        {
            currentJournalTab = journalTabs.MAP;
            selectedJournalTab.rectTransform.anchoredPosition = new Vector2(originPos.x, originPos.y);
            currentPos = selectedJournalTab.rectTransform.anchoredPosition;
        }
    }
}
