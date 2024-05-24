using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;
    public int stage;
    public int lastStage;

    [Header("Player Info")]
    public float health;
    public float defaultSpeed;
    public float speed;
    public float defaultWeaponSpeedRate;
    public float weaponSpeedRate;
    public float maxHealth;
    public int level;
    public int kill;
    public float exp;
    public float[] nextExp;
    public float[] takeExp;
    public float[] spawnTimes;

    [Header("Game Objects")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public SpawnData[] spawnData;

    public enum Prefab
    {
        Enemy,
        Gem,
    }
    
    void Awake()
    {
        instance = this;
        
        health = maxHealth;
        speed = defaultSpeed;
        weaponSpeedRate = defaultWeaponSpeedRate;
        
        // 최초 무기
        StartCoroutine(OnStart());
    }
    
    IEnumerator OnStart()
    {
		yield return new WaitForSeconds(0.1f);
        // uiLevelUp.Select(1);
        
        AudioManager.instance.PlayBgm(true);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Start);
    }

    void NextStage()
    {
        if (stage == lastStage)
        {
            GameClear();
        }
        else
        {
            stage++;
            gameTime = 0;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.NextStage);
        }
    }

    void GameClear()
    {
        // AudioManager.instance.PlayBgm(false);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
        SceneManager.LoadScene("GameClear");
    }
    
    public void GameOver()
    {
        // AudioManager.instance.PlayBgm(false);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
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
            NextStage();
        }
    }
    
    public void GetExp(float expNum)
    {
        exp += expNum;
        if (exp >= nextExp[Mathf.Min(level,nextExp.Length-1)])
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
    public int level;
    public int[] stages;
    public int health;
    public float speed;
    public bool isFlip;
}
