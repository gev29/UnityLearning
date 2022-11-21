using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private GameObject healthContainer;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text healthPercentText;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private GameObject enemyCounterContainer;
    [SerializeField] private Text enemyCounter;

    private Action onClick;
    private APlayer player;

    private void Awake()
    {
        nextLevelButton.onClick.AddListener(NextLevelClicked);
    }

    private void NextLevelClicked()
    {
        onClick?.Invoke();
    }

    public void Init(APlayer player, UnityAction onReloadClick)
    {
        this.player = player;
        reloadButton.onClick.RemoveAllListeners();
        reloadButton.onClick.AddListener(onReloadClick);

        UpdatePlayerHealth();

        HideNextLevelView();
        HideEnemyCounter();
    }

    public void ShowEnemyCounter(int killed, int total)
    {
        enemyCounterContainer.SetActive(true);
        enemyCounter.text = $"{killed}/{total}";
    }

    public void HideEnemyCounter()
    {
        enemyCounterContainer.SetActive(false);
    }

    public void ShowNextLevelView(Action onClick)
    {
        this.onClick = onClick;
        nextLevelButton.gameObject.SetActive(true);
    }

    public void HideNextLevelView()
    {
        nextLevelButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdatePlayerHealth();
    }

    private void UpdatePlayerHealth()
    {
        if (player != null)
        {
            float playerNormalizedHealth = player.Health / APlayer.MaxHP;
            healthSlider.value = playerNormalizedHealth;
            healthPercentText.text = $"{Mathf.CeilToInt(playerNormalizedHealth * 100)}%";
        }
        else if (healthContainer.activeSelf)
        {
            healthContainer.SetActive(false);
        }
    }
}
