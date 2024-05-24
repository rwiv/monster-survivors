using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

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

        if (timer > GameManager.instance.spawnTimes[GameManager.instance.stage])
        {
            timer = 0;
            if (Random.Range(1, 100) < GameManager.instance.specialRate)
            {
                SpawnTriggerEnemy(3);
            }
            else
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
		int prefabIdx = (int)GameManager.Prefab.Enemy;
        GameObject enemy = GameManager.instance.pool.Get(prefabIdx);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        List<SpawnData> targets = new List<SpawnData>();
        foreach (var spawnData in GameManager.instance.spawnData)
        {
            if (Contains(spawnData.stages, GameManager.instance.stage))
            {
                targets.Add(spawnData);
            }
        }
        int idx = Random.Range(0, targets.Count);
        enemy.GetComponent<Enemy>().Init(targets[idx]);
    }

    private bool Contains(int[] ints, int target)
    {
        foreach (var i in ints)
        {
            if (i == target)
            {
                return true;
            }
        }
        return false;
    }
    
	public void SpawnTriggerEnemy(int idx)
	{
		int prefabIdx = (int)GameManager.Prefab.Enemy;
        GameObject enemy = GameManager.instance.pool.Get(prefabIdx);

        Spawner spawner = GameManager.instance.spawner;
        enemy.transform.position = spawner.spawnPoint[Random.Range(1, spawner.spawnPoint.Length)].position;
        Vector3 orgScale = enemy.transform.localScale;
        float coef = 0.5f;
        enemy.transform.localScale = new Vector3(orgScale.x * coef, orgScale.y * coef, orgScale.z);

        SpawnData target = GameManager.instance.spawnData[idx];
        enemy.GetComponent<Enemy>().Init(target);
	}

    public void SpawnTarget(int idx)
    {
		int prefabIdx = (int)GameManager.Prefab.Enemy;
        GameObject enemy = GameManager.instance.pool.Get(prefabIdx);
        
        Spawner spawner = GameManager.instance.spawner;
        enemy.transform.position = spawner.spawnPoint[Random.Range(1, spawner.spawnPoint.Length)].position;

        SpawnData target = GameManager.instance.spawnData[idx];
        enemy.GetComponent<Enemy>().Init(target);
    }
}
