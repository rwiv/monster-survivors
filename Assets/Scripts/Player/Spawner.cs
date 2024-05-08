using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        // 최대 시간에 몬스터 데이터 크기로 나누어 자동으로 구간 시간 계산
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }

    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(
            Mathf.FloorToInt(GameManager.instance.gameTime / levelTime),
            spawnData.Length - 1
        );

        if (timer > (spawnData[level].spawnTime))
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);    //SpawnData 인스펙터창
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
    public bool isFlip;
}
