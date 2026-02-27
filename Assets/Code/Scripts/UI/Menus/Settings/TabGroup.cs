using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    private List<TabButton> _tabButtons = new List<TabButton>();
    [SerializeField] Color tabIdle;
    [SerializeField] Color tabHover;
    [SerializeField] Color tabActive;
    [SerializeField] private List<GameObject> _objectsToSwap;
    private TabButton _selectedTab;

    public void Subscribe(TabButton tabButton)
    {
        if (!_tabButtons.Contains(tabButton))
            _tabButtons.Add(tabButton);
    }

    private void Start()
    {
        if (_tabButtons.Count > 0)
        {
            OnTabSelected(_tabButtons[0]);
        }
    }

    private void OnEnable()
    {
        if (_tabButtons.Count > 0)
        {
            OnTabSelected(_tabButtons[0]);
        }
    }

    public void OnTabEnter(TabButton tabButton)
    {
        ResetTabs();
        if (_selectedTab == null || tabButton != _selectedTab)
        {
            tabButton.TabImage.color = tabHover;
        }
    }

    public void OnTabExit(TabButton tabButton)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton tabButton)
    {
        _selectedTab = tabButton;
        ResetTabs();
        tabButton.TabImage.color = tabActive;
        tabButton.TabText.fontStyle |= FontStyles.Underline;

        int index = tabButton.transform.GetSiblingIndex(); //Order of gameobjects in list is same as index of the tabs
        for (int i = 0; i < _objectsToSwap.Count; i++)
        {
            if (i == index)
                _objectsToSwap[i].gameObject.SetActive(true);
            else
                _objectsToSwap[i].gameObject.SetActive(false);
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in _tabButtons)
        {
            if (_selectedTab != null && button == _selectedTab) { continue; }
            button.TabImage.color = tabIdle;
            button.TabText.fontStyle &= ~FontStyles.Underline;
        }
    }
}
