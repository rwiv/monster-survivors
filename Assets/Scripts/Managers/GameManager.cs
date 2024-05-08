using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public PoolManager pool;
    public Player player;
    
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    public float health;
    public float maxHealth;
    public int level;
    public int kill;
    public float exp;
    public float[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

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
}
