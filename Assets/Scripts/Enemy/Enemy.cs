using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	public Rigidbody2D target;
	
    public float health;
	public float maxHealth;
	public RuntimeAnimatorController[] animCon;

	bool isLive;
	private bool isFilp;

	Rigidbody2D rigid;
	private Animator anim;
	SpriteRenderer spriter;

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriter = GetComponent<SpriteRenderer>();
		
		rigid.gravityScale = 0;
		rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void FixedUpdate()
	{
		if (!isLive)
			return;

		Vector2 dirVec = target.position - rigid.position;
		Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + nextVec);
		rigid.velocity = Vector2.zero;
	}

	void LateUpdate()
	{
		if (!isLive)
			return;

		bool filpX = target.position.x < rigid.position.x;
		spriter.flipX = isFilp ? !filpX : filpX;
	}

	public void Init(SpawnData data)
	{
		anim.runtimeAnimatorController = animCon[data.spriteType];
		speed = data.speed;
		maxHealth = data.health;
		health = data.health;
		isFilp = data.isFlip;
	}

	private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Bullet"))
			return;
		health -= collision.GetComponent<Bullet>().damage;
    
		if (health > 0)
		{
			// .. Live, Hit Action
		}
		else
		{
			// Dead
			Dead();
		}
	}

	void Dead()
	{
		gameObject.SetActive(false);
	}
}
