using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
	public float speed;
	public Rigidbody2D target;
	public RuntimeAnimatorController[] animCon;
	
    public float health;
	public float maxHealth;
	public int knockBackPower = 5;

	bool isLive;
	bool isFilp;
	bool isKnockBack = false;

	Rigidbody2D rigid;
	Collider2D coll;
	Animator anim;
	SpriteRenderer spriter;

	void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
		spriter = GetComponent<SpriteRenderer>();
		
		rigid.gravityScale = 0;
		rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void FixedUpdate()
	{
		if (!isLive || isKnockBack)
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
		coll.enabled = true;
		rigid.simulated = true;
		spriter.sortingOrder = 2;
        health = maxHealth;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!isLive)
			return;

		if (collision.CompareTag("Bullet"))
		{
			onHitBullet(collision);
		}
		else if (collision.CompareTag("Player"))
		{
			onHitPlayer();
		}
	}
	
	IEnumerator KnockBack()
	{
		isKnockBack = true;
        
		//yield return null;  // 1프레임 쉬기
		//yield return new WaitForSeconds(2f);    // 2초 쉬기
		yield return new WaitForFixedUpdate();//하나의 물리 프레임을 딜레이 주기
		
		Vector3 playerPos = GameManager.instance.player.transform.position;
		Vector3 dirVec = transform.position - playerPos;
		rigid.AddForce(dirVec.normalized * knockBackPower, ForceMode2D.Impulse);
		
		spriter.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		spriter.color = Color.white;
		
		isKnockBack = false;
	}

	IEnumerator SetPlayerHitColor()
	{
		GameManager.instance.player.sprite.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		GameManager.instance.player.sprite.color = Color.white;
		yield return new WaitForSeconds(0.2f);
		GameManager.instance.player.sprite.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		GameManager.instance.player.sprite.color = Color.white;
	}

	void onHitBullet(Collider2D collision)
	{
		health -= collision.GetComponent<Bullet>().damage;
		StartCoroutine(KnockBack());
    
		if (health > 0) // on hit
		{
			// ...
		}
		else // on dead
		{
			isLive = false;
			coll.enabled = false;
			rigid.simulated = false;
			spriter.sortingOrder = 1;
			GameManager.instance.kill++;
			GameManager.instance.GetExp();
			Dead();
		}
	}

	void onHitPlayer()
	{
		GameManager.instance.health--;
		StartCoroutine(SetPlayerHitColor());
	}

	void Dead()
	{
		gameObject.SetActive(false);
	}
}
