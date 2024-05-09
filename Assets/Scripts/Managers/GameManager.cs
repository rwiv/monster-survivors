using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    void Awake()
    {
        instance = this;
        
        maxHealth = 100;
        health = maxHealth;
        
        // 최초 무기
        uiLevelUp.Select(1);
    }
    
    void Update()
    {
        if (!isLive)
            return;
        
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
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
