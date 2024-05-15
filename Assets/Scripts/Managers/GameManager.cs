using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("Player Info")]
    public float health;
    public float maxHealth;
    public int level;
    public int kill;
    public float exp;
    public float[] nextExp;

    [Header("Game Objects")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public SpawnData[] spawnData;
    
    void Awake()
    {
        instance = this;
        
        health = maxHealth;
        
        // 최초 무기
        StartCoroutine(OnStart());
    }
    
    IEnumerator OnStart()
    {
		yield return new WaitForSeconds(0.1f);
        uiLevelUp.Select(1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    
    void Update()
    {
        if (!isLive)
            return;
        
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            SceneManager.LoadScene("GameClear");
        }
    }
    
    public void GetExp()
    {
        exp++;
        if (exp == nextExp[Mathf.Min(level,nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }
    
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

[System.Serializable]
public class SpawnData
{
    public RuntimeAnimatorController animCon;
    // public int spriteType;
    // public float spawnTime;
    public int health;
    public float speed;
    public bool isFlip;
}
