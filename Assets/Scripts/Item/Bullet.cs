using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //프리펩 친구들은 변수 초기화를 하는게 좋다
    public float damage;
    public int per;

    Rigidbody2D rigid;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
        rigid.gravityScale = 0;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    public void Init(float daamage,int per, Vector3 dir)
    {
        this.damage = daamage;
        this.per = per; 

        if(per >= 0)
        {
            rigid.velocity = dir * 15f;
        }
    }
    
    //관통
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 근거리 무기일 때
        if(!collision.CompareTag("Enemy") || per == -100)
            return;
        
        per--;
        if (per < 0)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    // 총알이 맵 밖으로 벗어나면 삭제
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;
        
        gameObject.SetActive(false);
    }
}
