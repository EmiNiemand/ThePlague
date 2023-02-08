using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class APScytheBoomerangFusion : AttackPattern
{
	private Collider2D weaponCollider;
	private SpriteShapeRenderer weaponRenderer;

	[Range(0, 1.0f)]
	[SerializeField] private float attackDuration;

    private void Start()
	{
		Setup();

		GetComponent<WeaponHitDetect>().pattern = this;

		weaponCollider = GetComponent<PolygonCollider2D>();
		weaponRenderer = GetComponent<SpriteShapeRenderer>();

		weaponCollider.enabled = false;
		weaponRenderer.enabled = false;
	}

	public override IEnumerator StartPattern()
	{
		if(isOnCooldown) yield break;

		StartCoroutine(Cooldown());

		weaponCollider.enabled = true;
		weaponRenderer.enabled = true;

		yield return new WaitForSeconds(attackDuration - upgrades.attackSpeed);

		weaponCollider.enabled = false;
		weaponRenderer.enabled = false;
	}

	protected override IEnumerator Cooldown()
	{
		isOnCooldown = true;
		yield return new WaitForSeconds(attackDuration + cooldownDuration - upgrades.attackSpeed);
		isOnCooldown = false;
	}
}
