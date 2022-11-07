using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] protected Transform player;
    [SerializeField] protected Transform targetObj;
    [SerializeField] protected float distanceToWin;

    public bool GameFinished { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    protected virtual void Update()
    {
        if (player != null && targetObj != null)
        {
            Vector3 difference = targetObj.position - player.position;
            if (!GameFinished && difference.sqrMagnitude < distanceToWin * distanceToWin)
            {
                GameFinished = true;
                LoadNextLevel();
            }
        }
        if (GameFinished && Input.GetKeyUp(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log($"<color=green>Level {nextSceneIndex} passed</color>");
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void GameOver()
    {
        GameFinished = true;
    }
}
