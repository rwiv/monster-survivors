using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    // public SpawnData[] spawnData;
    // public float levelTime;
    public float spawnTime;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
		if (!GameManager.instance.isLive)
			return;
		
        timer += Time.deltaTime;
        
        // level = Mathf.Min(
        //     Mathf.FloorToInt(GameManager.instance.gameTime / levelTime),
        //     spawnData.Length - 1
        // );
        //
        // if (timer > spawnData[level].spawnTime)
        // {
        //     timer = 0;
        //     Spawn();
        // }

        if (timer > spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        int enemyPrefabIdx = 0;
        GameObject enemy = GameManager.instance.pool.Get(enemyPrefabIdx);    //SpawnData 인스펙터창
        enemy.transform.position = spawnPoint[Random.Range(1,spawnPoint.Length)].position;

        SpawnData[] spawnData = GameManager.instance.spawnData;
        int randIdx = Random.Range(0, spawnData.Length);
        enemy.GetComponent<Enemy>().Init(spawnData[randIdx]);
    }
}

// [System.Serializable]
// public class SpawnData
// {
//     public int spriteType;
//     // public float spawnTime;
//     public int health;
//     public float speed;
//     public bool isFlip;
// }
