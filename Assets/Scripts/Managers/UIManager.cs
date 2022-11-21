using System;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private MainScreen mainScreen;
    [SerializeField] private WinPopup winPopup;

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Init(APlayer player, UnityAction onReloadClick)
    {
        mainScreen.Init(player, onReloadClick);
        winPopup.Reset();
    }

    public void UpdateEnemyCounter(int killed, int total)
    {
        mainScreen.ShowEnemyCounter(killed, total);
    }

    private void HideEnemyCounter()
    {
        mainScreen.HideEnemyCounter();
    }

    public void ShowNextLevelView(Action onClick)
    {
        mainScreen.ShowNextLevelView(onClick);
    }

    public void HideNextLevelView()
    {
        mainScreen.HideNextLevelView();
    }

    public void ShowWinPopup(Action onConfirmBtnClicked, Action onHide)
    {
        winPopup.Show(onConfirmBtnClicked, onHide);
    }

    public void HideWinPopup()
    {
        winPopup.Hide();
    }
}
