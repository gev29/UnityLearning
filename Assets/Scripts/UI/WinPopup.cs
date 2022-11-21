using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupContainer;
    [SerializeField] private EventTrigger darkBackgroundEventTrigger;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button confirmBtn;

    private Action onClick;
    private Action onHide;

    private void Awake()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => Hide());
        darkBackgroundEventTrigger.triggers.Add(entry);

        closeBtn.onClick.AddListener(Hide);
        confirmBtn.onClick.AddListener(ConfirmBtnClicked);
    }

    private void ConfirmBtnClicked()
    {
        this.onClick?.Invoke();
    }

    public void Show(Action onClick, Action onHide)
    {
        this.onClick = onClick;
        this.onHide = onHide;
        popupContainer.SetActive(true);
    }

    public void Hide()
    {
        popupContainer.SetActive(false);
        var hideAction = this.onHide;
        this.onHide = null;
        hideAction?.Invoke();
    }

    public void Reset()
    {
        this.onHide = null;
        Hide();
    }
}
