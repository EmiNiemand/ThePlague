using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
	// bool needed to check collisions
	bool isAttacking = false;
	SpriteShapeRenderer spriteShapeRenderer;
	// Start is called before the first frame update
	void Start()
	{
		spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();
	}

	public void Attack()
	{
		StartCoroutine(AttackCoroutine());
	}

	IEnumerator AttackCoroutine()
	{
		isAttacking = true;
		spriteShapeRenderer.enabled = true;

		yield return new WaitForSeconds(1.0f);

		/*Gamepad.current.SetMotorSpeeds(0, 0);*/
		spriteShapeRenderer.enabled = false;
		isAttacking = false;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(isAttacking && other.transform.tag == "Enemy")
		{
			Debug.Log("siema");
			/*Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);*/
			// other.transform.GetComponent<Enemy>().Damage;
		}
	}
}
