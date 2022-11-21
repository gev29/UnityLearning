using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : GameManager
{
    [Header("Level 2 Fields")]
    [SerializeField] private float addEnemyDelay;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform enemiesParent;
    [SerializeField] private int enemiesCount;
    [SerializeField] private Transform gate;
    [SerializeField] private float gateOpenSpeed;

    [Header("Level 2 Prefabs")]
    [SerializeField] private GameObject enemyPrefab;

    private int leftEnemiesCount;
    private int killedEnemiesCount;

    //private float curTime;

    protected override void Awake()
    {
        base.Awake();
        //curTime = addEnemyDelay;
        leftEnemiesCount = enemiesCount;
        StartCoroutine(UpdateEnemyTimer());
    }

    protected override void Start()
    {
        base.Start();
        UpdateEnemyCounter();
    }

    //private void UpdateEnemyTimer()
    //{
    //    if(curTime > 0)
    //    {
    //        curTime = Mathf.Max(curTime - Time.deltaTime, 0);

    //        if (curTime == 0)
    //        {
    //            Vector3 randomPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
    //            Vector3 direction = player.position - randomPoint;
    //            Enemy enemy = Instantiate(enemyPrefab, randomPoint, Quaternion.LookRotation(Vector3.forward, direction)).GetComponent<Enemy>();
    //            enemy.transform.SetParent(enemiesParent);
    //            enemy.Init(player);

    //            if (--enemiesCount != 0)
    //            {
    //                curTime = addEnemyDelay;
    //            }
    //        }
    //    }
    //}

    private IEnumerator UpdateEnemyTimer()
    {
        while(!GameFinished && leftEnemiesCount > 0)
        {
            yield return new WaitForSeconds(addEnemyDelay);

            Vector3 randomPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            Vector3 direction = player.transform.position - randomPoint;
            Enemy enemy = Instantiate(enemyPrefab, randomPoint, Quaternion.LookRotation(Vector3.forward, direction)).GetComponent<Enemy>();
            enemy.transform.SetParent(enemiesParent);
            enemy.Init(player.transform, EnemyDestroyed);
            leftEnemiesCount--;
        }

        if (!GameFinished)
        {
            StartCoroutine(OpenGate());
        }
    }

    private void EnemyDestroyed(bool killed)
    {
        if (killed)
        {
            killedEnemiesCount++;
            UpdateEnemyCounter();
        }
    }

    private void UpdateEnemyCounter()
    {
        UIManager.Instance.UpdateEnemyCounter(killedEnemiesCount, enemiesCount);
    }

    private IEnumerator OpenGate()
    {
        Vector3 curPosition = gate.localPosition;
        SpriteRenderer sr = gate.GetComponent<SpriteRenderer>();
        Vector3 targetPosition = curPosition + Vector3.up * sr.sprite.texture.height / sr.sprite.pixelsPerUnit * gate.localScale.y;

        while(curPosition != targetPosition)
        {
            yield return null;

            curPosition = Vector3.MoveTowards(curPosition, targetPosition, Time.deltaTime * gateOpenSpeed);
            gate.localPosition = curPosition;
        }
    }
}
