using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Vector2 inputVec;
	public float speed;
	public Scanner scanner;
	public SpriteRenderer sprite;

	Animator anim;
	Rigidbody2D rigid;

	void Awake()
	{
		scanner = GetComponent<Scanner>();
		sprite = GetComponent<SpriteRenderer>();
		
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();

		rigid.gravityScale = 0;
		rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void Update()
	{
		if (!GameManager.instance.isLive)
			return;
		
		inputVec.x = Input.GetAxisRaw("Horizontal");
		inputVec.y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
		if (!GameManager.instance.isLive)
			return;
		
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
			// GameManager.instance.GameOver();
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		sprite.color = Color.white;
	}
}