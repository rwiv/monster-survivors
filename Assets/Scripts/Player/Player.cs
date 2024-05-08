using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
		inputVec.x = Input.GetAxisRaw("Horizontal");
		inputVec.y = Input.GetAxisRaw("Vertical");
	}

	void FixedUpdate()
	{
		Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
		rigid.MovePosition(rigid.position + nextVec);
	}

	void LateUpdate()
	{
		anim.SetFloat("Speed", inputVec.magnitude);

		if (inputVec.x != 0) {
			sprite.flipX = inputVec.x < 0;
		}
	}
}