using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
	GamepadHaptics haptics;
	private AttackPattern pattern;
	// bool needed to check collisions
	bool isAttacking = false;
	private SpriteShapeRenderer spriteShapeRenderer;
    [SerializeField] private float damage;

    [SerializeField] private float cooldown;
    private bool isOnCooldown = false;
    [SerializeField] private Sprite weaponSprite;

	// Start is called before the first frame update
	void Start()
	{
		haptics = GetComponentInParent<GamepadHaptics>();
		spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();
		pattern = GetComponentInChildren<AttackPattern>();
	}

	public void Attack()
	{
		if (!isOnCooldown)
		{
			StartCoroutine(AttackCoroutine());
		}
	}

	IEnumerator AttackCoroutine()
	{
		StartCoroutine(Cooldown());

		isAttacking = true;
		spriteShapeRenderer.enabled = true;
		
		yield return StartCoroutine(pattern.StartPattern());

		spriteShapeRenderer.enabled = false;
		isAttacking = false;
	}

	IEnumerator Cooldown()
	{
		isOnCooldown = true;
		
		yield return new WaitForSeconds(cooldown);
		
		isOnCooldown = false;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(isAttacking && other.CompareTag("Enemy"))
		{
			StartCoroutine(haptics.Attack());
			// other.transform.GetComponent<Enemy>().Damage;
		}
	}
}
