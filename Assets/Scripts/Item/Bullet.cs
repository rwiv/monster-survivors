using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    Rigidbody2D rigid;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
        rigid.gravityScale = 0;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    public void Init(float daamage, int per, Vector3 dir, float velocity)
    {
        this.damage = daamage;
        this.per = per; 

        if(per >= 0)
        {
            rigid.velocity = dir * velocity;
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
