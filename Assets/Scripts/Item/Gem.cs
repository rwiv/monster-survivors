using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Sprite[] sprites;
    
    float exp;
	SpriteRenderer spriter;

    private void Awake()
    {
        exp = 0;
        spriter = GetComponent<SpriteRenderer>();
    }

    public void Init(int level)
    {
        exp = GameManager.instance.takeExp[level];
        spriter.sprite = sprites[level];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GetExp(exp);
            gameObject.SetActive(false);
            
            AudioManager.instance.PlaySfx(AudioManager.Sfx.TakeGem);
        }
    }
}
