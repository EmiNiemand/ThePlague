using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class APScythe : AttackPattern
{
	private Collider2D collider;
	private SpriteShapeRenderer renderer;

	[Range(0, 1.0f)]
	[SerializeField] private float attackDuration;

    private void Start()
	{
		Setup("Scythe");

		GetComponent<WeaponHitDetect>().pattern = this;

		collider = GetComponent<PolygonCollider2D>();
		renderer = GetComponent<SpriteShapeRenderer>();

		collider.enabled = false;
		renderer.enabled = false;
	}

	public override IEnumerator StartPattern()
	{
		if(isOnCooldown) yield break;

		StartCoroutine(Cooldown());

		collider.enabled = true;
		renderer.enabled = true;

		yield return new WaitForSeconds(attackDuration);

		collider.enabled = false;
		renderer.enabled = false;
	}

	protected override IEnumerator Cooldown()
	{
		isOnCooldown = true;
		yield return new WaitForSeconds(attackDuration + cooldownDuration);
		isOnCooldown = false;
	}
}
