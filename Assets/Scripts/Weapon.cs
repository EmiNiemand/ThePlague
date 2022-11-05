using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
	GamepadHaptics haptics;

	// bool needed to check collisions
	bool isAttacking = false;
	// Start is called before the first frame update
	void Start()
	{
		haptics = GetComponentInParent<GamepadHaptics>();
	}

	public void Attack()
	{
		StartCoroutine(AttackCoroutine());
	}

	IEnumerator AttackCoroutine()
	{
		isAttacking = true;
		GetComponent<SpriteShapeRenderer>().enabled = true;

		yield return new WaitForSeconds(1.0f);

		GetComponent<SpriteShapeRenderer>().enabled = false;
		isAttacking = false;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(isAttacking && other.transform.tag == "Enemy")
		{
			StartCoroutine(haptics.Attack());
			// other.transform.GetComponent<Enemy>().Damage;
		}
	}
}
