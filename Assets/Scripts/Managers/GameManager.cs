using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("Player Info")]
    public float health;
    public float maxHealth;
    public int level;
    public int kill;
    public float exp;
    public float[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("Game Objects")]
    public PoolManager pool;
    public Player player;
    
    void Awake()
    {
        instance = this;
        
        maxHealth = 100;
        health = maxHealth;
    }
    
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
    
    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
