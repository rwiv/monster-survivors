using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBullet : MonoBehaviour
{
    public float damage;
    public float offsetX;
    public float offsetY;

    public Vector3 origin;

    void Awake()
    {
        origin = transform.localScale;
    }

    public void Init(float damage, float coef)
    {
        this.damage = damage;
        transform.localScale = new Vector3(coef, coef, 1);
    }

    private void FixedUpdate()
    {
        Vector3 pos = GameManager.instance.player.transform.position;
        pos.x += offsetX;
        pos.y += offsetY;
        transform.position = pos;
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.isHeat = true;
            
            enemy.health -= damage;
    
            if (enemy.health <= 0)
            {
                enemy.Dead();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.isHeat = false;
        }
    }
}
