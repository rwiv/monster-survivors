using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Vector2 inputVec;
	public Scanner scanner;
	public SpriteRenderer sprite;

	Animator anim;
	Rigidbody2D rigid;
	Collider2D coll;

	float prevHealth;

	void Awake()
	{
		scanner = GetComponent<Scanner>();
		sprite = GetComponent<SpriteRenderer>();
		
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();

		rigid.gravityScale = 0;
		rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void Update()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Roll"))
		{
			coll.isTrigger = true;
		}
		else
		{
			coll.isTrigger = false;
		}
		
		if (!GameManager.instance.isLive)
			return;
	       
		inputVec.x = Input.GetAxisRaw("Horizontal");
		inputVec.y = Input.GetAxisRaw("Vertical");

		if (Input.GetKeyDown("space"))
		{
			// GameManager.instance.spawner.SpawnTriggerEnemy(3);
			StartCoroutine(Roll());
		}
	}

	IEnumerator Roll()
	{
		anim.SetBool("Roll", true);
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("Roll", false);
	}
	
	void FixedUpdate()
	{
		float curHealth = GameManager.instance.health;
		if (prevHealth == curHealth)
		{
			sprite.color = Color.white;
		}
		prevHealth = curHealth;
		
		if (!GameManager.instance.isLive)
			return;

		float speed = GameManager.instance.speed;
		Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + nextVec);
	}

	void LateUpdate()
	{
		if (!GameManager.instance.isLive)
			return;
		
		anim.SetFloat("Speed", inputVec.magnitude);

		if (inputVec.x != 0) {
			sprite.flipX = inputVec.x < 0;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (!GameManager.instance.isLive)
			return;
		
		sprite.color = Color.red;
		GameManager.instance.health -= Time.deltaTime * 10;
    
		if (GameManager.instance.health < 0)
		{
			for(int index = 2; index < transform.childCount; index++)
			{
				transform.GetChild(index).gameObject.SetActive(false);
			}
    
			// anim.SetTrigger("Dead");
			GameManager.instance.GameOver();
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		sprite.color = Color.white;
	}
}