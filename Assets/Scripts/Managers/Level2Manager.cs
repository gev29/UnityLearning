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
    [SerializeField] private float enemiesCount;

    [Header("Level 2 Prefabs")]
    [SerializeField] private GameObject enemyPrefab;

    private float curTime;

    protected override void Awake()
    {
        base.Awake();
        curTime = addEnemyDelay;
    }

    protected override void Update()
    {
        base.Update();
        if (!GameFinished)
        {
            UpdateEnemyTimer();
        }
    }

    private void UpdateEnemyTimer()
    {
        if(curTime > 0)
        {
            curTime = Mathf.Max(curTime - Time.deltaTime, 0);

            if (curTime == 0)
            {
                Vector3 randomPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                Vector3 direction = player.position - randomPoint;
                Enemy enemy = Instantiate(enemyPrefab, randomPoint, Quaternion.LookRotation(Vector3.forward, direction)).GetComponent<Enemy>();
                enemy.transform.SetParent(enemiesParent);
                enemy.Init(player);

                if (--enemiesCount != 0)
                {
                    curTime = addEnemyDelay;
                }
            }
        }
    }
}
