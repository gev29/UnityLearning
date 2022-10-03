using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform player;
    [SerializeField] private Transform targetObj;
    [SerializeField] private float distanceToWin;

    public bool GameFinished { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        Vector3 difference = targetObj.position - player.position;

        if (!GameFinished && difference.sqrMagnitude < distanceToWin * distanceToWin)
        {
            Debug.Log("Level 1 passed");
            GameFinished = true;
            SceneManager.LoadScene(1);
        }
    }
}
